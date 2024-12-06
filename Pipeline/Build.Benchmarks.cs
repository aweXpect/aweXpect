using System.Collections.Generic;
using System.IO;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using Octokit;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

[GitHubActions(
	"Benchmarks",
	GitHubActionsImage.UbuntuLatest,
	AutoGenerate = false,
	ImportSecrets = new[] { nameof(GithubToken) }
)]
partial class Build
{
	[Parameter("Github Token")] readonly string GithubToken;

	Target Benchmarks => _ => _
		.Executes(async () =>
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

			int? prId = GitHubActions.PullRequestNumber;
			if (prId != null)
			{
				string fileContent = await File.ReadAllTextAsync(ArtifactsDirectory / "Benchmarks" / "results" /
				                                                 "aweXpect.Benchmarks.HappyCaseBenchmarks-report-github.md");

				GitHubClient gitHubClient = new GitHubClient(new ProductHeaderValue("Nuke"));
				Credentials tokenAuth = new Credentials(GithubToken);
				gitHubClient.Credentials = tokenAuth;
				IReadOnlyList<IssueComment> comments =
					await gitHubClient.Issue.Comment.GetAllForIssue("aweXpect", "aweXpect", prId.Value);
				long? commentId = null;
				foreach (IssueComment comment in comments)
				{
					if (comment.Body.Contains("BenchmarkDotNet v"))
					{
						Log.Information($"Found comment: {comment.Body}");
						commentId = comment.Id;
					}
				}

				if (commentId == null)
				{
					Log.Information($"Create comment {fileContent}");
					await gitHubClient.Issue.Comment.Create("aweXpect", "aweXpect", prId.Value, fileContent);
				}
				else
				{
					Log.Information($"Update comment {fileContent}");
					await gitHubClient.Issue.Comment.Update("aweXpect", "aweXpect", commentId.Value, fileContent);
				}
			}
		});
}
