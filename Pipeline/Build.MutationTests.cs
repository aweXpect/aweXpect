using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	Target MutationTests => _ => _
		.DependsOn(Compile)
		.Executes(() =>
		{
			AbsolutePath toolPath = TestResultsDirectory / "dotnet-stryker";
			AbsolutePath configFile = toolPath / "Stryker.Config.json";
			toolPath.CreateOrCleanDirectory();

			DotNetToolInstall(_ => _
				.SetPackageName("dotnet-stryker")
				.SetToolInstallationPath(toolPath));

			Dictionary<Project, Project[]> projects = new()
			{
				{ Solution.aweXpect, UnitTestProjects },
				{ Solution.aweXpect_Core, [Solution.Tests.aweXpect_Core_Tests] },
				{ Solution.aweXpect_Discovery, [Solution.Tests.aweXpect_Discovery_Tests] }
			};

			foreach (KeyValuePair<Project, Project[]> project in projects)
			{
				string configText = $$"""
				                      {
				                      	"stryker-config": {
				                      		"project-info": {
				                      			"name": "github.com/aweXpect/aweXpect",
				                      			"module": "{{project.Key.Name}}",
				                      			"version": "{{GitVersion.BranchName}}"
				                      		},
				                      		"test-projects": [
				                      			{{string.Join(",\n\t\t\t", project.Value.Select(PathForJson))}}
				                      		],
				                      		"project": {{PathForJson(project.Key)}},
				                      		"target-framework": "net8.0",
				                      		"since": {
				                      			"target": "main",
				                      			"enabled": false,
				                      			"ignore-changes-in": [
				                      				"**/.github/**/*.*"
				                      			]
				                      		},
				                      		"reporters": [
				                      			"html",
				                      			"progress",
				                      			"cleartext"
				                      		],
				                      		"mutation-level": "Advanced"
				                      	}
				                      }
				                      """;
				File.WriteAllText(configFile, configText);
				Log.Debug($"Created '{configFile}':{Environment.NewLine}{configText}");

				string arguments = IsServerBuild
					? $"-f \"{configFile}\" -v \"{GitVersion.BranchName}\" -r \"Dashboard\" -r \"cleartext\""
					: $"-f \"{configFile}\" -v \"{GitVersion.BranchName}\" -r \"cleartext\"";

				string executable = EnvironmentInfo.IsWin ? "dotnet-stryker.exe" : "dotnet-stryker";
				IProcess process = ProcessTasks.StartProcess(
						Path.Combine(toolPath, executable),
						arguments,
						Solution.Directory)
					.AssertWaitForExit();
				if (process.ExitCode != 0)
				{
					Assert.Fail(
						$"Stryker did not execute successfully for {project.Key.Name}: (exit code {process.ExitCode}).");
				}
			}
		});

	static string PathForJson(Project project) => $"\"{project.Path.ToString().Replace(@"\", @"\\")}\"";
}
