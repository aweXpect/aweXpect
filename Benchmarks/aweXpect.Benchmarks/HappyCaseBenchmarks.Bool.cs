using BenchmarkDotNet.Attributes;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace aweXpect.Benchmarks;

public partial class HappyCaseBenchmarks
{
	private readonly bool _boolSubject = true;

	[Benchmark]
	public async Task<bool> Bool_aweXpect()
		=> await Expect.That(_boolSubject).IsTrue();

	[Benchmark]
	public AndConstraint<BooleanAssertions> Bool_FluentAssertions()
		=> _boolSubject.Should().BeTrue();

	[Benchmark]
	public async Task<bool> Bool_TUnit()
		=> await Assert.That(_boolSubject).IsTrue();
}
