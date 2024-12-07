using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace aweXpect.Benchmarks;

[MemoryDiagnoser]
public partial class HappyCaseBenchmarks
{
	private readonly Consumer _consumer = new();
	
}
