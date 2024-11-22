using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.SonarScanner;
using System.Linq;
using static Nuke.Common.Tools.Coverlet.CoverletTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	[Parameter("The key to push to sonarcloud")]
	[Secret]
	readonly string SonarToken;

	Target CodeAnalysisBegin => _ => _
		.Unlisted()
		.Before(Compile)
		.Before(CodeCoverage)
		.Executes(() =>
		{
			SonarScannerTasks.SonarScannerBegin(s => s
				.SetOrganization("awexpect")
				.SetProjectKey("aweXpect_aweXpect")
				.AddVSTestReports(TestResultsDirectory / "*.trx")
				.AddOpenCoverPaths(TestResultsDirectory / "reports" / "OpenCover.xml")
				.SetBranchName(GitVersion.BranchName)
				.SetVersion(GitVersion.SemVer)
				.SetToken(SonarToken));
		});

	Target CodeAnalysisEnd => _ => _
		.Unlisted()
		.DependsOn(Compile)
		.DependsOn(CodeCoverage)
		//.OnlyWhenDynamic(() => IsServerBuild)
		.Executes(() =>
		{
			SonarScannerTasks.SonarScannerEnd(s => s
				.SetToken(SonarToken));
		});

	Target CodeAnalysis => _ => _
		.DependsOn(CodeAnalysisBegin)
		.DependsOn(CodeAnalysisEnd);
}
