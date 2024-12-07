using BenchmarkDotNet.Attributes;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace aweXpect.Benchmarks;

public partial class HappyCaseBenchmarks
{
	private readonly string _stringExpectation = "foo";
	private readonly string _stringSubject = "foo";

	[Benchmark]
	public async Task<string?> String_aweXpect()
		=> await Expect.That(_stringSubject).Should().Be(_stringExpectation);

	[Benchmark]
	public AndConstraint<StringAssertions> String_FluentAssertions()
		=> _stringSubject.Should().Be(_stringExpectation);

	[Benchmark]
	public async Task<string?> String_TUnit()
		=> await Assert.That(_stringSubject).IsEqualTo(_stringExpectation);
}
