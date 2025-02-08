using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed class HasSingle
	{
		public sealed class Tests
		{
			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

				async Task Act()
					=> await That(subject).HasSingle();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has a single item,
					             but it contained more than one item
					             """);
			}

			[Fact]
			public async Task ShouldReturnSingleItem()
			{
				IEnumerable<int> subject = ToEnumerable([42]);

				int result = await That(subject).HasSingle();

				await That(result).IsEqualTo(42);
			}

			[Fact]
			public async Task WhenEnumerableContainsMoreThanOneElement_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).HasSingle();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has a single item,
					             but it contained more than one item
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsSingleElement_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1]);

				int result = await That(subject).HasSingle();

				await That(result).IsEqualTo(1);
			}

			[Fact]
			public async Task WhenEnumerableIsEmpty_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).HasSingle();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has a single item,
					             but it was empty
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject).HasSingle();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has a single item,
					             but it was <null>
					             """);
			}
		}

		public sealed class WhichTests
		{
			[Fact]
			public async Task ShouldReturnSingleItem()
			{
				IEnumerable<int> subject = ToEnumerable([42]);

				int result = await That(subject).HasSingle().Which.IsGreaterThan(41).And
					.IsLessThan(43);

				await That(result).IsEqualTo(42);
			}

			[Fact]
			public async Task WhenEnumerableContainsMoreThanOneElement_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).HasSingle().Which.IsGreaterThan(4);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has a single item which is greater than 4,
					             but it contained more than one item
					             """);
			}

			[Fact]
			public async Task WhenEnumerableIsEmpty_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).HasSingle().Which.IsGreaterThan(4);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has a single item which is greater than 4,
					             but it was empty
					             """);
			}

			[Fact]
			public async Task WhenSingleItemDoesNotSatisfyExpectation_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([3]);

				async Task Act()
					=> await That(subject).HasSingle().Which.IsGreaterThan(4);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has a single item which is greater than 4,
					             but it was 3
					             """);
			}

			[Fact]
			public async Task WhenSingleItemSatisfiesExpectation_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([3]);

				async Task Act()
					=> await That(subject).HasSingle().Which.IsGreaterThan(2);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
