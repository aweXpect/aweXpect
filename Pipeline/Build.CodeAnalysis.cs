using Nuke.Common;
using Nuke.Common.Tools.SonarScanner;

// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	[Parameter("The key to push to sonarcloud")] [Secret] readonly string SonarToken;

	Target CodeAnalysisBegin => _ => _
		.Unlisted()
		.Before(Compile)
		.Executes(() =>
		{
			SonarScannerTasks.SonarScannerBegin(s => s
				.SetOrganization("awexpect")
				.SetProjectKey("aweXpect_aweXpect")
				.AddVSTestReports(TestResultsDirectory / "*.trx")
				.AddOpenCoverPaths(TestResultsDirectory / "Coverage" / "*opencover.xml")
				.SetBranchName(GitVersion.BranchName)
				.SetVersion(GitVersion.SemVer)
				.SetToken(SonarToken));
		});

	Target CodeAnalysisEnd => _ => _
		.Unlisted()
		.DependsOn(Compile)
		.DependsOn(CodeCoverage)
		.Executes(() =>
		{
			SonarScannerTasks.SonarScannerEnd(s => s
				.SetToken(SonarToken));
		});

	Target CodeAnalysis => _ => _
		.DependsOn(CodeAnalysisBegin)
		.DependsOn(Compile)
		.DependsOn(CodeAnalysisEnd);
}
