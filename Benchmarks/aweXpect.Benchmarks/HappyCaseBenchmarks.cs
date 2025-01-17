using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace aweXpect.Benchmarks;

[MarkdownExporterAttribute.GitHub]
[MemoryDiagnoser]
public partial class HappyCaseBenchmarks
{
	private readonly Consumer _consumer = new();
}
