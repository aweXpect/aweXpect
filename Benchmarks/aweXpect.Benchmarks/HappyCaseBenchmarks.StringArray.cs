using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using FluentAssertions;
using FluentAssertions.Collections;
using TUnit.Assertions.Enums;

namespace aweXpect.Benchmarks;

public partial class 
	HappyCaseBenchmarks
{
	private readonly string[] _stringArrayExpectation = ["foo", "bar", "baz"];
	private readonly string[] _stringArrayOtherOrderExpectation = ["foo", "baz", "bar"];
	private readonly string[] _stringArraySubject = ["foo", "bar", "baz"];

	[Benchmark]
	public async Task StringArray_aweXpect()
		=> (await Expect.That(_stringArraySubject).Should().Be(_stringArrayExpectation)).Consume(_consumer);

	[Benchmark]
	public AndConstraint<StringCollectionAssertions<IEnumerable<string>>> StringArray_FluentAssertions()
		=> _stringArraySubject.Should().Equal(_stringArrayExpectation);

	[Benchmark]
	public async Task StringArray_TUnit()
		=> (await Assert.That(_stringArraySubject).IsEquivalentTo(_stringArrayExpectation))?.Consume(_consumer);

	[Benchmark]
	public async Task StringArrayInAnyOrder_aweXpect()
		=> (await Expect.That(_stringArraySubject).Should().Be(_stringArrayOtherOrderExpectation).InAnyOrder())
			.Consume(_consumer);

	[Benchmark]
	public AndConstraint<StringCollectionAssertions<IEnumerable<string>>> StringArrayInAnyOrder_FluentAssertions()
		=> _stringArraySubject.Should().BeEquivalentTo(_stringArrayOtherOrderExpectation);

	[Benchmark]
	public async Task StringArrayInAnyOrder_TUnit()
		=> (await Assert.That(_stringArraySubject)
			.IsEquivalentTo(_stringArrayOtherOrderExpectation, CollectionOrdering.Any))?.Consume(_consumer);
}
