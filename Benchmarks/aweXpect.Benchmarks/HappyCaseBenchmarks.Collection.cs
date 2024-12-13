using BenchmarkDotNet.Attributes;
using FluentAssertions;
using FluentAssertions.Collections;

namespace aweXpect.Benchmarks;

public partial class HappyCaseBenchmarks
{
	private readonly int _enumerableCount = 1000;
	private readonly IEnumerable<int> _enumerableSubject = Enumerable.Range(1, 1000);

	[Benchmark]
	public async Task ItemsCount_AtLeast_aweXpect()
		=> await Expect.That(_enumerableSubject).Should().HaveAtLeast(_enumerableCount).Items();

	[Benchmark]
	public AndConstraint<GenericCollectionAssertions<int>> ItemsCount_AtLeast_FluentAssertions()
		=> _enumerableSubject.Should().HaveCountGreaterThanOrEqualTo(_enumerableCount);

	[Benchmark]
	public async Task ItemsCount_AtLeast_TUnit()
		=> await Assert.That(_enumerableSubject).HasCount().GreaterThanOrEqualTo(_enumerableCount);
}
