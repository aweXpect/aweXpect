using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Xunit;
using static Nuke.Common.Tools.Xunit.XunitTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable UnusedMember.Local
// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	Target UnitTests => _ => _
		.DependsOn(DotNetFrameworkUnitTests)
		.DependsOn(DotNetUnitTests);

	Target DotNetFrameworkUnitTests => _ => _
		.Unlisted()
		.DependsOn(Compile)
		.OnlyWhenDynamic(() => EnvironmentInfo.IsWin)
		.Executes(() =>
		{
			string[] testAssemblies = UnitTestProjects(BuildScope)
				.SelectMany(project =>
					project.Directory.GlobFiles(
						$"bin/{(Configuration == Configuration.Debug || BuildScope == BuildScope.CoreOnly ? "Debug" : "Release")}/net48/*.Tests.dll"))
				.Select(p => p.ToString())
				.ToArray();

			Assert.NotEmpty(testAssemblies.ToList());

			Xunit2(s => s
				.SetFramework("net48")
				.AddTargetAssemblies(testAssemblies)
			);
		});

	Target DotNetUnitTests => _ => _
		.Unlisted()
		.DependsOn(Compile)
		.Executes(() =>
		{
			string net48 = "net48";
			DotNetTest(s => s
					.SetConfiguration(BuildScope == BuildScope.CoreOnly ? Configuration.Debug : Configuration)
					.SetProcessEnvironmentVariable("DOTNET_CLI_UI_LANGUAGE", "en-US")
					.EnableNoBuild()
					.SetDataCollector("XPlat Code Coverage")
					.SetResultsDirectory(TestResultsDirectory)
					.CombineWith(
						UnitTestProjects(BuildScope),
						(settings, project) => settings
							.SetProjectFile(project)
							.CombineWith(
								project.GetTargetFrameworks()?.Except([net48]),
								(frameworkSettings, framework) => frameworkSettings
									.SetFramework(framework)
									.AddLoggers($"trx;LogFileName={project.Name}_{framework}.trx")
							)
					), completeOnFailure: true
			);
		});

	Project[] UnitTestProjects(BuildScope buildScope)
		=> buildScope switch
		{
			BuildScope.CoreOnly => [Solution.Tests.aweXpect_Core_Tests],
			BuildScope.MainOnly =>
			[
				Solution.Tests.aweXpect_Analyzers_Tests,
				Solution.Tests.aweXpect_Tests,
				Solution.Tests.aweXpect_Internal_Tests
			],
			_ =>
			[
				Solution.Tests.aweXpect_Core_Tests,
				Solution.Tests.aweXpect_Analyzers_Tests,
				Solution.Tests.aweXpect_Tests,
				Solution.Tests.aweXpect_Internal_Tests
			]
		};
}
