using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;

namespace Build;

[GitHubActions(
	"Build",
	GitHubActionsImage.UbuntuLatest,
	AutoGenerate = false,
	ImportSecrets = [nameof(GithubToken)]
)]
partial class Build : NukeBuild
{
	[Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
	Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

	[Parameter("Github Token")] readonly string GithubToken;

	[Solution(GenerateProjects = true)] readonly Solution Solution;

	AbsolutePath ArtifactsDirectory => RootDirectory / "Artifacts";
	AbsolutePath TestResultsDirectory => RootDirectory / "TestResults";
	GitHubActions GitHubActions => GitHubActions.Instance;

	public static int Main() => Execute<Build>(x => x.Pack, x => x.ApiChecks, x => x.Benchmarks, x => x.CodeAnalysis);
}
