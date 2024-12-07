using System.Collections.Generic;
using System.IO;
using System.Text;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using Octokit;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{

	Target BenchmarkDotNet => _ => _
		.Executes(() =>
		{
			AbsolutePath benchmarkDirectory = ArtifactsDirectory / "Benchmarks";
			benchmarkDirectory.CreateOrCleanDirectory();

			DotNetBuild(s => s
				.SetProjectFile(Solution.Benchmarks.aweXpect_Benchmarks)
				.SetConfiguration(Configuration.Release)
				.EnableNoLogo());

			DotNet(
				$"{Solution.Benchmarks.aweXpect_Benchmarks.Name}.dll --exporters json --filter * --artifacts \"{benchmarkDirectory}\"",
				Solution.Benchmarks.aweXpect_Benchmarks.Directory / "bin" / "Release");
		});

	Target BenchmarkResult => _ => _
		.After(BenchmarkDotNet)
		.Executes(async () =>
		{
			string fileContent = await File.ReadAllTextAsync(ArtifactsDirectory / "Benchmarks" / "results" /
			                                                 "aweXpect.Benchmarks.HappyCaseBenchmarks-report-github.md");
			Log.Information("Report:\n {FileContent}", fileContent);
		});

	Target BenchmarkComment => _ => _
		.After(BenchmarkDotNet)
		.OnlyWhenDynamic(() => GitHubActions.IsPullRequest)
		.Executes(async () =>
		{
			string body = CreateCommentBody();
			int? prId = GitHubActions.PullRequestNumber;
			Log.Debug("Pull request number: {PullRequestId}", prId);
			if (prId != null)
			{
				GitHubClient gitHubClient = new(new ProductHeaderValue("Nuke"));
				Credentials tokenAuth = new(GithubToken);
				gitHubClient.Credentials = tokenAuth;
				IReadOnlyList<IssueComment> comments =
					await gitHubClient.Issue.Comment.GetAllForIssue("aweXpect", "aweXpect", prId.Value);
				long? commentId = null;
				Log.Information($"Found {comments.Count} comments");
				foreach (IssueComment comment in comments)
				{
					if (comment.Body.Contains("## :rocket: Benchmark Results"))
					{
						Log.Information($"Found comment: {comment.Body}");
						commentId = comment.Id;
					}
				}

				if (commentId == null)
				{
					Log.Information($"Create comment:\n{body}");
					await gitHubClient.Issue.Comment.Create("aweXpect", "aweXpect", prId.Value, body);
				}
				else
				{
					Log.Information($"Update comment:\n{body}");
					await gitHubClient.Issue.Comment.Update("aweXpect", "aweXpect", commentId.Value, body);
				}
			}
		});

	Target Benchmarks => _ => _
		.DependsOn(BenchmarkDotNet)
		.DependsOn(BenchmarkResult)
		.DependsOn(BenchmarkComment);

	string CreateCommentBody()
	{
		string[] fileContent = File.ReadAllLines(ArtifactsDirectory / "Benchmarks" / "results" /
		                                         "aweXpect.Benchmarks.HappyCaseBenchmarks-report-github.md");
		StringBuilder sb = new();
		sb.AppendLine("## :rocket: Benchmark Results");
		sb.AppendLine("<details>");
		sb.AppendLine("<summary>Details</summary>");
		int count = 0;
		foreach (string line in fileContent)
		{
			if (line.StartsWith("```"))
			{
				count++;
				if (count == 1)
				{
					sb.AppendLine("<pre>");
				}
				else if (count == 2)
				{
					sb.AppendLine("</pre>");
					sb.AppendLine("</details>");
					sb.AppendLine();
				}

				continue;
			}
			sb.AppendLine(line);
		}

		string body = sb.ToString();
		return body;
	}
}
