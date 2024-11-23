using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.GitVersion;

namespace Build;

partial class Build : NukeBuild
{
	[Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
	readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

	[Required] [GitVersion(Framework = "net8.0", NoCache = true, NoFetch = true)] readonly GitVersion GitVersion;

	[Solution(GenerateProjects = true)] readonly Solution Solution;

	AbsolutePath ArtifactsDirectory => RootDirectory / "Artifacts";
	AbsolutePath TestResultsDirectory => RootDirectory / "TestResults";
	GitHubActions GitHubActions => GitHubActions.Instance;

	public static int Main() => Execute<Build>([
		x => x.ApiChecks,
		x => x.Benchmarks,
		x => x.CodeAnalysis,
		]);
}
