using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using Nuke.Components;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	Target Pack => _ => _
		.DependsOn(Compile)
		.Executes(() =>
		{
			ReportSummary(s => s
				.WhenNotNull(SemVer, (c, semVer) => c
					.AddPair("Packed version", semVer)));

			foreach (Project project in new[]
			         {
				         Solution.aweXpect, Solution.aweXpect_Core, Solution.aweXpect_Discovery
			         })
			{
				DotNetPack(s => s
					.SetProject(project)
					.SetOutputDirectory(ArtifactsDirectory)
					.SetConfiguration(Configuration == Configuration.Debug ? "Debug" : "Release")
					.EnableNoLogo()
					.EnableNoRestore()
					.EnableContinuousIntegrationBuild() // Necessary for deterministic builds
					.SetVersion(SemVer));
			}
		});
}
