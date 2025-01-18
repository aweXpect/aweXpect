using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable UnusedMember.Local
// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	Target ApiChecks => _ => _
		.DependsOn(Compile)
		.Executes(() =>
		{
			Project[] projects =
			[
				Solution.Tests.aweXpect_Api_Tests,
				Solution.Tests.aweXpect_Core_Api_Tests
			];

			DotNetTest(s => s
				.SetConfiguration(Configuration == Configuration.Debug ? "Debug" : "Release")
				.SetProcessEnvironmentVariable("DOTNET_CLI_UI_LANGUAGE", "en-US")
				.EnableNoBuild()
				.SetResultsDirectory(TestResultsDirectory)
				.CombineWith(
					projects,
					(settings, project) => settings
						.SetProjectFile(project)
						.AddLoggers($"trx;LogFileName={project.Name}.trx")), completeOnFailure: true);
		});
}
