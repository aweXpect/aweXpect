using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Utilities.Collections;
using Nuke.Components;
using System.IO;
using static Serilog.Log;

// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	Target UpdateReadme => _ => _
		.DependsOn(Clean)
		.Before(Compile)
		.Executes(() =>
		{
			string content = File.ReadAllText(Solution.Directory / "README.md");
			content += "- UPDATED";
			File.WriteAllText(ArtifactsDirectory / "README.md", content);
		});

	Target Pack => _ => _
		.DependsOn(UpdateReadme)
		.DependsOn(Compile)
		.Executes(() =>
		{
			ReportSummary(s => s
				.WhenNotNull(SemVer, (c, semVer) => c
					.AddPair("Packed version", semVer)));

			AbsolutePath packagesDirectory = ArtifactsDirectory / "Packages";
			packagesDirectory.CreateOrCleanDirectory();

			foreach (Project project in new[]
			         {
				         Solution.aweXpect, Solution.aweXpect_Core, Solution.aweXpect_Discovery
			         })
			{
				foreach (string package in
				         Directory.EnumerateFiles(project.Directory / "bin", "*.nupkg", SearchOption.AllDirectories))
				{
					File.Move(package, packagesDirectory / Path.GetFileName(package));
					Debug("Found nuget package: {PackagePath}", package);
				}

				foreach (string symbolPackage in
				         Directory.EnumerateFiles(project.Directory / "bin", "*.snupkg", SearchOption.AllDirectories))
				{
					File.Move(symbolPackage, packagesDirectory / Path.GetFileName(symbolPackage));
					Debug("Found symbol package: {PackagePath}", symbolPackage);
				}
			}
		});
}
