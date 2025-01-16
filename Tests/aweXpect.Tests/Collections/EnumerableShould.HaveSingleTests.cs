using System.Collections.Generic;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class EnumerableShould
{
	public sealed class HaveSingle
	{
		public sealed class Tests
		{
			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

				async Task Act()
					=> await That(subject).Should().HaveSingle();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have a single item,
					             but it contained more than one item
					             """);
			}

			[Fact]
			public async Task ShouldReturnSingleItem()
			{
				IEnumerable<int> subject = ToEnumerable([42]);

				int result = await That(subject).Should().HaveSingle();

				await That(result).Should().Be(42);
			}

			[Fact]
			public async Task WhenEnumerableContainsMoreThanOneElement_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().HaveSingle();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have a single item,
					             but it contained more than one item
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsSingleElement_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1]);

				int result = await That(subject).Should().HaveSingle();

				await That(result).Should().Be(1);
			}

			[Fact]
			public async Task WhenEnumerableIsEmpty_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).Should().HaveSingle();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have a single item,
					             but it was empty
					             """);
			}
		}

		public sealed class WhichTests
		{
			[Fact]
			public async Task ShouldReturnSingleItem()
			{
				IEnumerable<int> subject = ToEnumerable([42]);

				int result = await That(subject).Should().HaveSingle().Which.Should().BeGreaterThan(41).And
					.BeLessThan(43);

				await That(result).Should().Be(42);
			}

			[Fact]
			public async Task WhenEnumerableContainsMoreThanOneElement_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().HaveSingle().Which.Should().BeGreaterThan(4);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have a single item which should be greater than 4,
					             but it contained more than one item
					             """);
			}

			[Fact]
			public async Task WhenEnumerableIsEmpty_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).Should().HaveSingle().Which.Should().BeGreaterThan(4);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have a single item which should be greater than 4,
					             but it was empty
					             """);
			}

			[Fact]
			public async Task WhenSingleItemDoesNotSatisfyExpectation_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([3]);

				async Task Act()
					=> await That(subject).Should().HaveSingle().Which.Should().BeGreaterThan(4);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have a single item which should be greater than 4,
					             but it was 3
					             """);
			}

			[Fact]
			public async Task WhenSingleItemSatisfiesExpectation_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([3]);

				async Task Act()
					=> await That(subject).Should().HaveSingle().Which.Should().BeGreaterThan(2);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
