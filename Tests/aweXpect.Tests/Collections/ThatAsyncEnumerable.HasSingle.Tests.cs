#if NET8_0_OR_GREATER
using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed class HasSingle
	{
		public sealed class Tests
		{
			[Fact]
			public async Task ShouldReturnSingleItem()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([42,]);

				int result = await That(subject).HasSingle();

				await That(result).IsEqualTo(42);
			}

			[Fact]
			public async Task WhenAsyncEnumerableContainsMoreThanOneElement_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3,]);

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
			public async Task WhenAsyncEnumerableContainsSingleElement_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1,]);

				int result = await That(subject).HasSingle();

				await That(result).IsEqualTo(1);
			}

			[Fact]
			public async Task WhenAsyncEnumerableIsEmpty_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Array.Empty<int>());

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
				IAsyncEnumerable<string>? subject = null;

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

		public sealed class NegatedTests
		{
			[Fact]
			public async Task WhenEnumerableContainsMoreThanOneElement_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.HasSingle());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsSingleElement_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1,]);

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.HasSingle());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not have a single item,
					             but it did
					             """);
			}

			[Fact]
			public async Task WhenEnumerableIsEmpty_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.HasSingle());

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class WhichTests
		{
			[Fact]
			public async Task ShouldReturnSingleItem()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([42,]);

				int result = await That(subject).HasSingle().Which.IsGreaterThan(41).And
					.IsLessThan(43);

				await That(result).IsEqualTo(42);
			}

			[Fact]
			public async Task WhenAsyncEnumerableContainsMoreThanOneElement_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3,]);

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
			public async Task WhenAsyncEnumerableIsEmpty_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Array.Empty<int>());

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
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([3,]);

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
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([3,]);

				async Task Act()
					=> await That(subject).HasSingle().Which.IsGreaterThan(2);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
