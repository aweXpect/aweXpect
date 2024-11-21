using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.SonarScanner;
using System.Linq;
using static Nuke.Common.Tools.Coverlet.CoverletTasks;

// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	[Parameter("The key to push to sonarcloud")] [Secret] readonly string SonarToken;

	Target CodeAnalysisBegin => _ => _
		.Unlisted()
		.Before(Cover)
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

	Target Cover => _ => _
		.Unlisted()
		.Executes(() =>
		{
			AbsolutePath coverageDirectory = TestResultsDirectory / "Coverage";
			coverageDirectory.CreateOrCleanDirectory();

			const string net48 = "net48";
			foreach (Project project in UnitTestProjects)
			{
				foreach (string framework in project.GetTargetFrameworks()?.Except([net48]) ?? [])
				{
					AbsolutePath binPath = (project.Path.Parent / "bin" / (IsLocalBuild ? "Debug" : "Release") /
					                        framework / project.Name) + ".dll";
					Coverlet(s => s
						.SetTarget("dotnet")
						.SetProcessWorkingDirectory(EnvironmentInfo.IsWin ? project.Path.Parent : binPath.Parent)
						.SetTargetArgs("test")
						.SetAssembly(binPath)
						.SetOutput(coverageDirectory / (project.Name + "_" + framework + "_opencover.xml"))
						.SetFormat(CoverletOutputFormat.opencover));
				}
			}
		});

	Target CodeAnalysisEnd => _ => _
		.Unlisted()
		.DependsOn(Cover)
		.Executes(() =>
		{
			SonarScannerTasks.SonarScannerEnd(s => s
				.SetToken(SonarToken));
		});

	Target CodeAnalysis => _ => _
		.DependsOn(CodeAnalysisBegin)
		.DependsOn(Cover)
		.DependsOn(CodeAnalysisEnd);
}
