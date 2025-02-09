using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Git;
using Octokit;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using ProductHeaderValue = Octokit.ProductHeaderValue;

// ReSharper disable UnusedMember.Local
// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	private const string BenchmarkBranch = "benchmarks";

	Target BenchmarkDotNet => _ => _
		.OnlyWhenDynamic(() => !OnlyCore)
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
		.OnlyWhenDynamic(() => !OnlyCore)
		.Executes(async () =>
		{
			string fileContent = await File.ReadAllTextAsync(ArtifactsDirectory / "Benchmarks" / "results" /
			                                                 "aweXpect.Benchmarks.HappyCaseBenchmarks-report-github.md");
			Log.Information("Report:\n {FileContent}", fileContent);
		});

	Target BenchmarkComment => _ => _
		.After(BenchmarkDotNet)
		.OnlyWhenDynamic(() => !OnlyCore && GitHubActions?.IsPullRequest == true)
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
		.After(BenchmarkDotNet)
		.OnlyWhenDynamic(() => !OnlyCore && GitHubActions?.IsPullRequest == false)
		.Executes(async () =>
		{
			BenchmarkFile currentFile = await DownloadBenchmarkFile();
			List<string> benchmarkReports = new();
			foreach (string file in Directory.GetFiles(ArtifactsDirectory / "Benchmarks" / "results",
				         "*full-compressed.json"))
			{
				benchmarkReports.Add(await File.ReadAllTextAsync(file));
			}

			Output[] lines = GitTasks.Git("log -1").ToArray();
			string commitId = null, author = null, date = null, message = null;
			foreach (string line in lines.Select(x => x.Text))
			{
				if (commitId == null && line.StartsWith("commit "))
				{
					commitId = line.Substring("commit ".Length, 40);
					continue;
				}

				if (author == null && line.StartsWith("Author: "))
				{
					author = line.Substring("Author: ".Length);
					int index = author.IndexOf(" <", StringComparison.Ordinal);
					if (index > 0)
					{
						author = author.Substring(0, index);
					}

					continue;
				}

				if (date == null && line.StartsWith("Date:   "))
				{
					date = line.Substring("Date:   ".Length);
					continue;
				}

				if (commitId != null && author != null && date != null && !string.IsNullOrWhiteSpace(line))
				{
					message = line.Trim();
					break;
				}
			}

			PageBenchmarkReportGenerator.CommitInfo commitInfo = new(commitId, author, date, message);
			string updatedFileContent =
				PageBenchmarkReportGenerator.Append(commitInfo, currentFile.Content, benchmarkReports);

			if (!string.IsNullOrWhiteSpace(updatedFileContent))
			{
				await UploadBenchmarkFile(commitInfo, currentFile, updatedFileContent);
			}
		});

	Target Benchmarks => _ => _
		.DependsOn(BenchmarkDotNet)
		.DependsOn(BenchmarkResult)
		.DependsOn(BenchmarkComment)
		.DependsOn(BenchmarkReport);

	async Task UploadBenchmarkFile(PageBenchmarkReportGenerator.CommitInfo commitInfo, BenchmarkFile currentFile,
		string updatedFileContent)
	{
		using HttpClient client = new();
		client.DefaultRequestHeaders.UserAgent.ParseAdd("aweXpect");
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GithubToken);
		GithubUpdateFile content = new(
			$"Update benchmark for {commitInfo.Sha.Substring(0, 8)}: {commitInfo.Message} by {commitInfo.Author}",
			Base64Encode(updatedFileContent),
			currentFile.Sha,
			BenchmarkBranch);
		HttpResponseMessage response = await client.PutAsync(
			"https://api.github.com/repos/aweXpect/aweXpect/contents/Docs/pages/static/js/data.js",
			new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json"));
		if (response.IsSuccessStatusCode)
		{
			Log.Information("Successfully updated the benchmark data...");
		}
		else
		{
			string responseContent = await response.Content.ReadAsStringAsync();
			Log.Error($"Could not update the benchmark data: {responseContent}");
		}
	}

	async Task<BenchmarkFile> DownloadBenchmarkFile()
	{
		using HttpClient client = new();
		client.DefaultRequestHeaders.UserAgent.ParseAdd("aweXpect");
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GithubToken);
		HttpResponseMessage response = await client.GetAsync(
			$"https://api.github.com/repos/aweXpect/aweXpect/contents/Docs/pages/static/js/data.js?ref={BenchmarkBranch}");
		string responseContent = await response.Content.ReadAsStringAsync();
		if (!response.IsSuccessStatusCode)
		{
			throw new InvalidOperationException(
				$"Could not find 'Docs/pages/static/js/data.js' in branch '{BenchmarkBranch}': {responseContent}");
		}

		using JsonDocument document = JsonDocument.Parse(responseContent);
		string content = Base64Decode(document.RootElement.GetProperty("content").GetString());
		string sha = document.RootElement.GetProperty("sha").GetString();
		return new BenchmarkFile(content, sha);
	}

	static string Base64Encode(string plainText)
	{
		byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
		return Convert.ToBase64String(plainTextBytes);
	}

	static string Base64Decode(string base64EncodedData)
	{
		byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
		return Encoding.UTF8.GetString(base64EncodedBytes);
	}

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

	// ReSharper disable InconsistentNaming
	// ReSharper disable NotAccessedPositionalProperty.Local
	private record GithubUpdateFile(string message, string content, string sha, string branch);
	// ReSharper restore NotAccessedPositionalProperty.Local
	// ReSharper restore InconsistentNaming

	private record BenchmarkFile(string Content, string Sha);
}
