using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Octokit;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using ProductHeaderValue = Octokit.ProductHeaderValue;
using Project = Nuke.Common.ProjectModel.Project;

// ReSharper disable UnusedMember.Local
// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	AbsolutePath StrykerOutputDirectory => ArtifactsDirectory / "Stryker";
	AbsolutePath StrykerToolPath => TestResultsDirectory / "dotnet-stryker";

	Target MutationTestsCore => _ => _
		.DependsOn(Compile)
		.OnlyWhenDynamic(() => BuildScope == BuildScope.Default)
		.Executes(() =>
		{
			ExecuteMutationTest(Solution.aweXpect_Core, [..FrameworkUnitTestProjects, Solution.Tests.aweXpect_Core_Tests,]);
		});

	Target MutationTestsMain => _ => _
		.DependsOn(Compile)
		.OnlyWhenDynamic(() => BuildScope == BuildScope.Default)
		.Executes(() =>
		{
			ExecuteMutationTest(Solution.aweXpect, [Solution.Tests.aweXpect_Tests, Solution.Tests.aweXpect_Internal_Tests,]);
		});
	Target MutationTestsComment => _ => _
		.After(MutationTestsMain)
		.After(MutationTestsCore)
		.OnlyWhenDynamic(() => GitHubActions.IsPullRequest)
		.Executes(async () =>
		{
			int? prId = GitHubActions.PullRequestNumber;
			Log.Debug("Pull request number: {PullRequestId}", prId);
			var mutationCommentBodies = new List<string>();
			foreach (var file in ArtifactsDirectory.GetFiles("MutationTest_*.md"))
			{
				var body = await File.ReadAllTextAsync(file);
				mutationCommentBodies.Add(body);
			}

			if (mutationCommentBodies.Count == 0)
			{
				return;
			}

			if (prId != null)
			{
				GitHubClient gitHubClient = new(new ProductHeaderValue("Nuke"));
				Credentials tokenAuth = new(GithubToken);
				gitHubClient.Credentials = tokenAuth;
				IReadOnlyList<IssueComment> comments =
					await gitHubClient.Issue.Comment.GetAllForIssue("aweXpect",
						"aweXpect", prId.Value);
				IssueComment existingComment = null;
				Log.Information($"Found {comments.Count} comments");
				foreach (IssueComment comment in comments)
				{
					if (comment.Body.Contains("## :alien: Mutation Results"))
					{
						Log.Information($"Found comment: {comment.Body}");
						existingComment = comment;
					}
				}

				string body = "## :alien: Mutation Results"
				              + Environment.NewLine
				              + $"[![Mutation testing badge](https://img.shields.io/endpoint?style=flat&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2FaweXpect%2FaweXpect%2Fpull/{prId}/merge)](https://dashboard.stryker-mutator.io/reports/github.com/aweXpect/aweXpect/pull/{prId}/merge)"
				              + Environment.NewLine
				              + string.Join(Environment.NewLine, mutationCommentBodies);
				if (existingComment == null)
				{
					Log.Information($"Create comment:\n{body}");
					await gitHubClient.Issue.Comment.Create("aweXpect", "aweXpect",
						prId.Value, body);
				}
				else
				{
					Log.Information($"Update comment:\n{body}");
					await gitHubClient.Issue.Comment.Update("aweXpect", "aweXpect",
						existingComment.Id, body);
				}
			}
		});

	Target MutationTestsDashboard => _ => _
		.After(MutationTestsMain)
		.After(MutationTestsCore)
		.OnlyWhenDynamic(() => BuildScope == BuildScope.Default)
		.Executes(async () =>
		{
			Dictionary<Project, Project[]> projects = new()
			{
				{
					Solution.aweXpect, [Solution.Tests.aweXpect_Tests, Solution.Tests.aweXpect_Internal_Tests,]
				},
				{
					Solution.aweXpect_Core, [..FrameworkUnitTestProjects, Solution.Tests.aweXpect_Core_Tests,]
				},
			};
			string apiKey = Environment.GetEnvironmentVariable("STRYKER_DASHBOARD_API_KEY");
			string branchName = File.ReadAllText(ArtifactsDirectory / "BranchName.txt");
			foreach (KeyValuePair<Project, Project[]> project in projects)
			{
				string reportComment =
					File.ReadAllText(ArtifactsDirectory / "Stryker" / "reports" / "mutation-report.json");
				using HttpClient client = new();
				client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
				// https://stryker-mutator.io/docs/General/dashboard/#send-a-report-via-curl
				await client.PutAsync(
					$"https://dashboard.stryker-mutator.io/api/reports/github.com/aweXpect/aweXpect/{branchName}?module={project.Key.Name}",
					new StringContent(reportComment, new MediaTypeHeaderValue("application/json")));
			}
		});

	private void ExecuteMutationTest(Project project, Project[] testProjects)
	{
		AbsolutePath toolPath = TestResultsDirectory / "dotnet-stryker";
		AbsolutePath configFile = toolPath / "Stryker.Config.json";
		AbsolutePath strykerOutputDirectory = ArtifactsDirectory / "Stryker";
		strykerOutputDirectory.CreateOrCleanDirectory();
		toolPath.CreateOrCleanDirectory();

		DotNetToolInstall(_ => _
			.SetPackageName("dotnet-stryker")
			.SetVersion("4.7.0")
			.SetToolInstallationPath(toolPath));

		string branchName = BranchName;
		if (GitHubActions?.Ref.StartsWith("refs/tags/", StringComparison.OrdinalIgnoreCase) == true)
		{
			string version = GitHubActions.Ref.Substring("refs/tags/".Length);
			branchName = "release/" + version;
			Log.Information("Use release branch analysis for '{BranchName}'", branchName);
		}

		File.WriteAllText(ArtifactsDirectory / "BranchName.txt", branchName);

		string configText = $$"""
		                      {
		                      	"stryker-config": {
		                      		"project-info": {
		                      			"name": "github.com/aweXpect/aweXpect",
		                      			"module": "{{project.Name}}",
		                      			"version": "{{branchName}}"
		                      		},
		                      		"test-projects": [
		                      			{{string.Join(",\n\t\t\t", testProjects.Select(PathForJson))}}
		                      		],
		                      		"project": {{PathForJson(project)}},
		                      		"target-framework": "net8.0",
		                      		"since": {
		                      			"target": "main",
		                      			"enabled": {{(BranchName != "main").ToString().ToLowerInvariant()}},
		                      			"ignore-changes-in": [
		                      				"**/.github/**/*.*"
		                      			]
		                      		},
		                      		"mutation-level": "Advanced"
		                      	}
		                      }
		                      """;
		File.WriteAllText(configFile, configText);
		Log.Debug($"Created '{configFile}':{Environment.NewLine}{configText}");

		string arguments =
			$"-f \"{configFile}\" -O \"{strykerOutputDirectory}\" -r \"Markdown\" -r \"cleartext\" -r \"json\"";

		string executable = EnvironmentInfo.IsWin ? "dotnet-stryker.exe" : "dotnet-stryker";
		IProcess process = ProcessTasks.StartProcess(
				Path.Combine(toolPath, executable),
				arguments,
				Solution.Directory)
			.AssertWaitForExit();
		if (process.ExitCode != 0)
		{
			Assert.Fail(
				$"Stryker did not execute successfully for {project.Name}: (exit code {process.ExitCode}).");
		}

		File.WriteAllText(ArtifactsDirectory / $"MutationTest_{project.Name}.md", CreateMutationCommentBody(project.Name));
	}

	string CreateMutationCommentBody(string projectName)
	{
		string[] fileContent =
			File.ReadAllLines(ArtifactsDirectory / "Stryker" / "reports" / "mutation-report.md");
		StringBuilder sb = new();
		sb.AppendLine($"<!-- START {projectName} -->");
		sb.AppendLine($"### {projectName}");
		sb.AppendLine("<details>");
		sb.AppendLine("<summary>Details</summary>");
		sb.AppendLine();
		int count = 0;
		foreach (string line in fileContent.Skip(1))
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				continue;
			}

			if (line.StartsWith("#"))
			{
				if (++count == 1)
				{
					sb.AppendLine();
					sb.AppendLine("</details>");
					sb.AppendLine();
				}

				sb.AppendLine("##" + line);
				continue;
			}

			if (count == 0 &&
			    line.StartsWith("|") &&
			    line.Contains("| N\\/A"))
			{
				continue;
			}

			sb.AppendLine(line);
		}

		sb.AppendLine($"<!-- END {projectName} -->");
		string body = sb.ToString();
		return body;
	}

	static string PathForJson(Project project)
		=> $"\"{project.Path.ToString().Replace(@"\", @"\\")}\"";
}
