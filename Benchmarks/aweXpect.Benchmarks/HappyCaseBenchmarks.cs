using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Primitives;
using TUnit.Assertions.Enums;

namespace aweXpect.Benchmarks;

[MemoryDiagnoser]
public class HappyCaseBenchmarks
{
	private readonly bool _boolSubject = true;
	private readonly string _stringExpectation = "foo";
	private readonly string _stringSubject = "foo";
	private readonly string[] _stringArraySubject = ["foo", "bar", "baz"];
	private readonly string[] _stringArrayExpectation = ["foo", "bar", "baz"];
	private readonly string[] _stringArrayOtherOrderExpectation = ["foo", "baz", "bar"];
	private readonly Consumer _consumer = new();
	

	[Benchmark]
	public AndConstraint<BooleanAssertions> Bool_FluentAssertions()
		=> _boolSubject.Should().BeTrue();
	
	[Benchmark]
	public async Task<bool> Bool_aweXpect()
		=> await Expect.That(_boolSubject).Should().BeTrue();
	
	[Benchmark]
	public async Task<bool> Bool_TUnit()
		=> await Assert.That(_boolSubject).IsTrue();
	
	[Benchmark]
	public AndConstraint<StringAssertions> String_FluentAssertions()
		=> _stringSubject.Should().Be(_stringExpectation);
	
	[Benchmark]
	public async Task<string?> String_aweXpect()
		=> await Expect.That(_stringSubject).Should().Be(_stringExpectation);
	
	[Benchmark]
	public async Task<string?> String_TUnit()
		=> await Assert.That(_stringSubject).IsEqualTo(_stringExpectation);
	
	[Benchmark]
	public AndConstraint<StringCollectionAssertions<IEnumerable<string>>> StringArray_FluentAssertions()
		=> _stringArraySubject.Should().Equal(_stringArrayExpectation);
	
	[Benchmark]
	public async Task StringArray_aweXpect()
		=> (await Expect.That(_stringArraySubject).Should().Be(_stringArrayExpectation)).Consume(_consumer);
	
	[Benchmark]
	public async Task StringArray_TUnit()
		=> (await Assert.That(_stringArraySubject).IsEquivalentTo(_stringArrayExpectation))?.Consume(_consumer);
	
	[Benchmark]
	public AndConstraint<StringCollectionAssertions<IEnumerable<string>>> StringArrayInAnyOrder_FluentAssertions()
		=> _stringArraySubject.Should().BeEquivalentTo(_stringArrayOtherOrderExpectation);
	
	[Benchmark]
	public async Task StringArrayInAnyOrder_aweXpect()
		=> (await Expect.That(_stringArraySubject).Should().Be(_stringArrayOtherOrderExpectation).InAnyOrder()).Consume(_consumer);
	
	[Benchmark]
	public async Task StringArrayInAnyOrder_TUnit()
		=> (await Assert.That(_stringArraySubject).IsEquivalentTo(_stringArrayOtherOrderExpectation, CollectionOrdering.Any))?.Consume(_consumer);
}
