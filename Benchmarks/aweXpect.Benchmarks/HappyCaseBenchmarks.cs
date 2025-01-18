using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

namespace aweXpect.Benchmarks;

[Config(typeof(Config))]
[MarkdownExporterAttribute.GitHub]
[MemoryDiagnoser]
public partial class HappyCaseBenchmarks
{
	private readonly Consumer _consumer = new();

	private class Config : ManualConfig
	{
		public Config()
		{
			AddJob(Job.MediumRun
				.WithLaunchCount(1)
				.WithToolchain(InProcessEmitToolchain.Instance)
				.WithId("InProcess"));
		}
	}
}
