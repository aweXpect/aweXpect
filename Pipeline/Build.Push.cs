using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using System;
using System.Collections.Generic;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	[Parameter("The key to push to Nuget")]
	[Secret]
	readonly string NugetApiKey;

	Target Push => _ => _
		.DependsOn(Pack)
		.OnlyWhenDynamic(() => IsTag && IsServerBuild)
		.ProceedAfterFailure()
		.Executes(() =>
		{
			AbsolutePath packagesDirectory = ArtifactsDirectory / "Packages";
			IReadOnlyCollection<AbsolutePath> packages = packagesDirectory.GlobFiles("*.nupkg");

			Assert.NotEmpty(packages);

			DotNetNuGetPush(s => s
				.SetApiKey(NugetApiKey)
				.EnableSkipDuplicate()
				.SetSource("https://api.nuget.org/v3/index.json")
				.CombineWith(packages, (v, path) => v.SetTargetPath(path)));
		});

	string BranchSpec => GitHubActions?.Ref;

	bool IsTag => BranchSpec != null && BranchSpec.StartsWith("refs/tags/", StringComparison.OrdinalIgnoreCase);
}
