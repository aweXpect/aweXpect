using BenchmarkDotNet.Attributes;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace aweXpect.Benchmarks;

[MemoryDiagnoser]
public class HappyCaseBenchmarks
{
	private readonly bool _boolSubject = true;

	[Benchmark]
	public AndConstraint<BooleanAssertions> Bool_FluentAssertions()
		=> _boolSubject.Should().BeTrue();
}
