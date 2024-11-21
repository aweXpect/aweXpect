using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.Xunit;
using System.Linq;
using static Nuke.Common.Tools.Xunit.XunitTasks;
using static Nuke.Common.Tools.Coverlet.CoverletTasks;

// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	Target UnitTests => _ => _
		.DependsOn(DotNetFrameworkUnitTests)
		.DependsOn(DotNetUnitTests);

	Project[] UnitTestProjects =>
	[
		Solution.Tests.aweXpect_Core_Tests,
		Solution.Tests.aweXpect_Discovery_Tests,
		Solution.Tests.aweXpect_Tests
	];

	Target DotNetFrameworkUnitTests => _ => _
		.Unlisted()
		.DependsOn(Compile)
		.Executes(() =>
		{
			string[] testAssemblies = UnitTestProjects
				.SelectMany(project =>
					project.Directory.GlobFiles(
						$"bin/{(Configuration == Configuration.Debug ? "Debug" : "Release")}/net48/*.Tests.dll"))
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
			var coverageDirectory = TestResultsDirectory / "Coverage";
			coverageDirectory.CreateOrCleanDirectory();
			
			const string net48 = "net48";
			foreach (Project project in UnitTestProjects)
			{
				foreach (string framework in project.GetTargetFrameworks()?.Except([net48]) ?? [])
				{
					AbsolutePath binPath = project.Path.Parent / "bin" / "Release" / framework / project.Name + ".dll";
					Coverlet(s => s
						.SetTarget("dotnet")
						.SetProcessWorkingDirectory(project.Path.Parent)
						.SetTargetArgs("test --no-build --no-restore")
						.SetAssembly(binPath)
						.SetOutput(coverageDirectory / (project.Name + "_" + framework + "_opencover.xml"))
						.SetFormat(CoverletOutputFormat.opencover));
				}
			}
			//DotNetTest(s => s
			//		.SetConfiguration(Configuration)
			//		.SetProcessEnvironmentVariable("DOTNET_CLI_UI_LANGUAGE", "en-US")
			//		.EnableNoBuild()
			//		.SetDataCollector("XPlat Code Coverage")
			//		.SetResultsDirectory(TestResultsDirectory)
			//		.CombineWith(
			//			UnitTestProjects,
			//			(settings, project) => settings
			//				.SetProjectFile(project)
			//				.CombineWith(
			//					project.GetTargetFrameworks()?.Except([net48]),
			//					(frameworkSettings, framework) => frameworkSettings
			//						.SetFramework(framework)
			//						.AddLoggers($"trx;LogFileName={project.Name}_{framework}.trx")
			//				)
			//		), completeOnFailure: true
			//);
		});
}
