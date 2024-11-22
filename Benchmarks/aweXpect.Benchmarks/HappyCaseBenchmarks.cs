using BenchmarkDotNet.Attributes;
using FluentAssertions;
using FluentAssertions.Primitives;
using aweXpect;

namespace aweXpect.Benchmarks;

[MemoryDiagnoser]
public class HappyCaseBenchmarks
{
	private readonly bool _boolSubject = true;
	private readonly string _stringExpectation = "foo";
	private readonly string _stringSubject = "foo";

	[Benchmark]
	public AndConstraint<BooleanAssertions> Bool_FluentAssertions()
		=> _boolSubject.Should().BeTrue();

	[Benchmark]
	public async Task<bool> Bool_aweXpectExpectations()
		=> await Expect.That(_boolSubject).Should().BeTrue();

	[Benchmark]
	public async Task<bool> Bool_TUnit()
		=> await Assert.That(_boolSubject).IsTrue();

	[Benchmark]
	public AndConstraint<StringAssertions> String_FluentAssertions()
		=> _stringSubject.Should().Be(_stringExpectation);

	[Benchmark]
	public async Task<string?> String_aweXpectExpectations()
		=> await Expect.That(_stringSubject).Should().Be(_stringExpectation);

	[Benchmark]
	public async Task<string?> String_TUnit()
		=> await Assert.That(_stringSubject).IsEqualTo(_stringExpectation);
}
