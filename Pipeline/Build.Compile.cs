using System;
using System.IO;
using System.Linq;
using System.Text;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable UnusedMember.Local
// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	string BranchName;
	AssemblyVersion CoreVersion;
	AssemblyVersion MainVersion;
	string SemVer;

	Target CalculateNugetVersion => _ => _
		.Unlisted()
		.Executes(() =>
		{
			CoreVersion = AssemblyVersion.FromGitVersion(GitVersionTasks.GitVersion(s => s
					.SetFramework("net8.0")
					.SetNoFetch(true)
					.SetNoCache(true)
					.DisableProcessOutputLogging()
					.SetUpdateAssemblyInfo(false)
					.AddProcessAdditionalArguments("/overrideconfig", "tag-prefix=core/v"))
				.Result);

			GitVersion gitVersion = GitVersionTasks.GitVersion(s => s
					.SetFramework("net8.0")
					.SetNoFetch(true)
					.SetNoCache(true)
					.DisableProcessOutputLogging()
					.SetUpdateAssemblyInfo(false))
				.Result;

			MainVersion = AssemblyVersion.FromGitVersion(gitVersion);
			SemVer = gitVersion.SemVer;
			BranchName = gitVersion.BranchName;

			if (GitHubActions?.IsPullRequest == true)
			{
				string buildNumber = GitHubActions.RunNumber.ToString();
				Console.WriteLine(
					$"Branch spec is a pull request. Adding build number {buildNumber}");

				SemVer = string.Join('.', gitVersion.SemVer.Split('.').Take(3).Union([buildNumber]));
			}

			Console.WriteLine($"SemVer = {SemVer}");
		});

	Target Clean => _ => _
		.Unlisted()
		.Before(Restore)
		.Executes(() =>
		{
			ArtifactsDirectory.CreateOrCleanDirectory();
			Log.Information("Cleaned {path}...", ArtifactsDirectory);

			TestResultsDirectory.CreateOrCleanDirectory();
			Log.Information("Cleaned {path}...", TestResultsDirectory);
		});

	Target Restore => _ => _
		.Unlisted()
		.DependsOn(Clean)
		.Executes(() =>
		{
			DotNetRestore(s => s
				.SetProjectFile(Solution)
				.EnableNoCache()
				.SetConfigFile(RootDirectory / "nuget.config"));
		});

	Target Compile => _ => _
		.DependsOn(Restore)
		.DependsOn(CalculateNugetVersion)
		.Executes(() =>
		{
			ReportSummary(s => s
				.WhenNotNull(MainVersion, (summary, version) => summary
					.AddPair("Version", version.FileVersion))
				.WhenNotNull(CoreVersion, (summary, version) => summary
					.AddPair("Core", version.FileVersion)));

			ClearNugetPackages(Solution.aweXpect.Directory / "bin");
			UpdateReadme(MainVersion.FileVersion);

			DotNetBuild(s => s
				.SetProjectFile(Solution)
				.SetConfiguration(Configuration)
				.EnableNoLogo()
				.SetVersion(MainVersion.FileVersion)
				.SetAssemblyVersion(MainVersion.FileVersion)
				.SetFileVersion(MainVersion.FileVersion)
				.SetInformationalVersion(MainVersion.InformationalVersion));

			ClearNugetPackages(Solution.aweXpect_Core.Directory / "bin");
			UpdateReadme(CoreVersion.FileVersion);

			DotNetBuild(s => s
				.SetProjectFile(Solution.aweXpect_Core)
				.SetConfiguration(Configuration)
				.EnableNoLogo()
				.SetVersion(CoreVersion.FileVersion)
				.SetProcessAdditionalArguments($"/p:SolutionDir={RootDirectory}")
				.SetAssemblyVersion(CoreVersion.FileVersion)
				.SetFileVersion(CoreVersion.FileVersion)
				.SetInformationalVersion(CoreVersion.InformationalVersion));
		});

	Target CompileDebug => _ => _
		.DependsOn(Restore)
		.DependsOn(CalculateNugetVersion)
		.Executes(() =>
		{
			ClearNugetPackages(Solution.aweXpect.Directory / "bin");

			DotNetBuild(s => s
				.SetProjectFile(Solution)
				.SetConfiguration(Configuration.Debug)
				.EnableNoLogo());
		});

	private void UpdateReadme(string fileVersion)
	{
		string version = string.Join('.', fileVersion.Split('.').Take(3));
		if (version.IndexOf('-') != -1)
		{
			version = version.Substring(0, version.IndexOf('-'));
		}

		StringBuilder sb = new();
		string[] lines = File.ReadAllLines(Solution.Directory / "README.md");
		sb.AppendLine(lines.First());
		sb.AppendLine(
			$"[![Changelog](https://img.shields.io/badge/Changelog-v{version}-blue)](https://github.com/aweXpect/aweXpect/releases/tag/v{version})");
		foreach (string line in lines.Skip(1))
		{
			if (line.StartsWith("[![Build](https://github.com/aweXpect/aweXpect/actions/workflows/build.yml") ||
			    line.StartsWith("[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure"))
			{
				continue;
			}

			if (line.StartsWith("[![Coverage](https://sonarcloud.io/api/project_badges/measure"))
			{
				sb.AppendLine(line
					.Replace(")", $"&branch=release/v{version})"));
				continue;
			}

			if (line.StartsWith("[![Mutation testing badge](https://img.shields.io/endpoint"))
			{
				sb.AppendLine(line
					.Replace("%2Fmain)", $"%2Frelease%2Fv{version})")
					.Replace("/main)", $"/release/v{version})"));
				continue;
			}

			sb.AppendLine(line);
		}

		File.WriteAllText(ArtifactsDirectory / "README.md", sb.ToString());
	}

	private static void ClearNugetPackages(string binPath)
	{
		if (Directory.Exists(binPath))
		{
			foreach (string package in Directory.EnumerateFiles(binPath, "*nupkg", SearchOption.AllDirectories))
			{
				File.Delete(package);
			}
		}
	}

	public record AssemblyVersion(string FileVersion, string InformationalVersion)
	{
		public static AssemblyVersion FromGitVersion(GitVersion gitVersion)
		{
			if (gitVersion is null)
			{
				return null;
			}

			return new AssemblyVersion(gitVersion.AssemblySemVer, gitVersion.InformationalVersion);
		}
	}
}
