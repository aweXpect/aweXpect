using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NJsonSchema;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Git;
using Nuke.Common.Tools.GitVersion;
using Octokit;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable UnusedMember.Local
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
		.OnlyWhenDynamic(() => GitHubActions?.IsPullRequest == true)
		.Executes(async () =>
		{
			string body = CreateBenchmarkCommentBody();
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

	Target BenchmarkReport => _ => _
		//.After(BenchmarkDotNet) // TODO: Change to `is main branch`
		.OnlyWhenDynamic(() => GitHubActions?.IsPullRequest != true)
		.Executes(async () =>
		{
			string currentFileContent = await DownloadBenchmarkFile();
			var benchmarkReports = new List<string>();
			foreach (var file in Directory.GetFiles(ArtifactsDirectory / "Benchmarks" / "results",
				         "*full-compressed.json"))
			{
				benchmarkReports.Add(await File.ReadAllTextAsync(file));
			}

			var cli = GitTasks.Git("log -1").ToArray();
			var commitId = cli[0].Text.Substring("commit ".Length, 40);
			var author = cli[1].Text.Substring("Author: ".Length);
			var date = cli[2].Text.Substring("Date:   ".Length);
			var message = cli[4].Text.Substring("    ".Length);
			var commitInfo = new PageBenchmarkReportGenerator.CommitInfo(commitId, author, date, message);
			string updatedFileContent = PageBenchmarkReportGenerator.Append(commitInfo, currentFileContent, benchmarkReports);
			await UploadBenchmarkFile(updatedFileContent);
		});

	Task UploadBenchmarkFile(string updatedFileContent)
	{
		//TODO replace with download from Github
		return File.WriteAllTextAsync(@"C:\Work\src\aweXpect\Docs\pages\static\js\data.js", updatedFileContent);
	}

	Task<string> DownloadBenchmarkFile()
	{
		//TODO replace with download from Github
		return File.ReadAllTextAsync(@"C:\Work\src\aweXpect\Docs\pages\static\js\data.js");
	}

	Target Benchmarks => _ => _
		.DependsOn(BenchmarkDotNet)
		.DependsOn(BenchmarkResult)
		.DependsOn(BenchmarkComment)
		.DependsOn(BenchmarkReport);

	string CreateBenchmarkCommentBody()
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

			if (line.StartsWith('|') && line.Contains("_aweXpect") && line.EndsWith('|'))
			{
				MakeLineBold(sb, line);
				continue;
			}

			sb.AppendLine(line);
		}

		string body = sb.ToString();
		return body;
	}

	static void MakeLineBold(StringBuilder sb, string line)
	{
		string[] tokens = line.Split("|", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
		sb.Append('|');
		foreach (string token in tokens)
		{
			sb.Append(" **");
			sb.Append(token);
			sb.Append("** |");
		}

		sb.AppendLine();
	}
}
