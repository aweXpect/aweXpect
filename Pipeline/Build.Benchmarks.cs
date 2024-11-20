using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable AllUnderscoreLocalParameterName

namespace Build;

partial class Build
{
	Target Benchmarks => _ => _
		.Executes(() =>
		{
			AbsolutePath benchmarkDirectory = ArtifactsDirectory / "Benchmarks";
			benchmarkDirectory.CreateOrCleanDirectory();

			DotNetBuild(s => s
				.SetProjectFile(Solution.Benchmarks.aweXpect_Benchmarks)
				.SetConfiguration(Configuration.Release)
				.EnableNoLogo());

			DotNet(
				$"{Solution.Benchmarks.aweXpect_Benchmarks.Name}.dll --exporters json --filter * --artifacts \"{benchmarkDirectory}\"",
				Solution.Benchmarks.aweXpect_Benchmarks.Directory / "bin" / "Release");
		});
}
