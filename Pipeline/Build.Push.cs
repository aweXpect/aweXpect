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
	readonly string NuGetApiKey;

	Target Push => _ => _
		.DependsOn(Pack)
		.OnlyWhenDynamic(() => IsTag && IsServerBuild)
		.ProceedAfterFailure()
		.Executes(() =>
		{
			IReadOnlyCollection<AbsolutePath> packages = ArtifactsDirectory.GlobFiles("*.nupkg");

			Assert.NotEmpty(packages);

			DotNetNuGetPush(s => s
				.SetApiKey(NuGetApiKey)
				.EnableSkipDuplicate()
				.SetSource("https://api.nuget.org/v3/index.json")
				.EnableNoSymbols()
				.CombineWith(packages,
					(v, path) => v.SetTargetPath(path)));
		});

	string BranchSpec => GitHubActions?.Ref;

	bool IsTag => BranchSpec != null && BranchSpec.Contains("refs/tags", StringComparison.OrdinalIgnoreCase);
}
