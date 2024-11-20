using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using System.Linq;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	Target TestFrameworks => _ => _
		.DependsOn(VsTestFrameworks)
		.DependsOn(TestingPlatformFrameworks);

	Target VsTestFrameworks => _ => _
		.Unlisted()
		.DependsOn(Compile)
		.Executes(() =>
		{
			Project[] projects =
			[
				Solution.Tests.Frameworks.aweXpect_Frameworks_NUnit3_Tests,
				Solution.Tests.Frameworks.aweXpect_Frameworks_NUnit4_Tests,
				Solution.Tests.Frameworks.aweXpect_Frameworks_XUnit2_Tests
			];

			var testCombinations =
				from project in projects
				let frameworks = project.GetTargetFrameworks()
				let supportedFrameworks = EnvironmentInfo.IsWin ? frameworks : frameworks.Except(["net47"])
				from framework in supportedFrameworks
				select new { project, framework };

			DotNetTest(s => s
				.SetConfiguration(Configuration)
				.SetProcessEnvironmentVariable("DOTNET_CLI_UI_LANGUAGE", "en-US")
				.EnableNoBuild()
				.SetDataCollector("XPlat Code Coverage")
				.SetResultsDirectory(TestResultsDirectory)
				.AddRunSetting(
					"DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.DoesNotReturnAttribute",
					"DoesNotReturnAttribute")
				.CombineWith(
					testCombinations,
					(settings, v) => settings
						.SetProjectFile(v.project)
						.SetFramework(v.framework)
						.AddLoggers($"trx;LogFileName={v.project.Name}_{v.framework}.trx")), completeOnFailure: true);
		});

	Target TestingPlatformFrameworks => _ => _
		.Unlisted()
		.DependsOn(Compile)
		.Executes(() =>
		{
			Project[] projects =
			[
				Solution.Tests.Frameworks.aweXpect_Frameworks_TUnit_Tests
			];

			var testCombinations =
				from project in projects
				let frameworks = project.GetTargetFrameworks()
				from framework in frameworks
				select new { project, framework };

			DotNetTest(s => s
				.SetConfiguration(Configuration)
				.SetProcessEnvironmentVariable("DOTNET_CLI_UI_LANGUAGE", "en-US")
				.EnableNoBuild()
				.CombineWith(
					testCombinations,
					(settings, v) => settings
						.SetProjectFile(v.project)
						.SetFramework(v.framework)
						.SetProcessArgumentConfigurator(args => args
							.Add("--")
							.Add("--coverage")
							.Add("--report-trx")
							.Add($"--report-trx-filename {v.project.Name}_{v.framework}.trx")
							.Add($"--results-directory {TestResultsDirectory}")
						)
				)
			);
		});
}
