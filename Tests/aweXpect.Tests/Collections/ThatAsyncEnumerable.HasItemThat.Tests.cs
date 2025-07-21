#if NET8_0_OR_GREATER
using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed class HasItemThat
	{
		public sealed class Tests
		{
			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

				async Task Act()
					=> await That(subject).HasItemThat(it => it.IsEqualTo(5));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsDifferentItemAtGivenIndex_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(0, 1, 2);

				async Task Act()
					=> await That(subject).HasItemThat(it => it.IsEqualTo(1)).AtIndex(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item that is equal to 1 at index 2,
					             but it had item 2 at index 2

					             Collection:
					             [0, 1, 2]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsExpectedItemAtGivenIndex_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(0, 1, 2);

				async Task Act()
					=> await That(subject).HasItemThat(it => it.IsEqualTo(2)).AtIndex(2);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsNoItemAtGivenIndex_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(0, 1, 2);

				async Task Act()
					=> await That(subject).HasItemThat(it => it.IsEqualTo(3)).AtIndex(3);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item that is equal to 3 at index 3,
					             but it did not contain any item at index 3

					             Collection:
					             [0, 1, 2]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableIsEmpty_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).HasItemThat(it => it.IsNotEqualTo(0));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item that is not equal to 0,
					             but it did not contain any item

					             Collection:
					             []
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_WithAnyIndex_ShouldFail()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).HasItemThat(it => it.IsNotEqualTo(0));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item that is not equal to 0,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_WithFixedIndex_ShouldFail()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).HasItemThat(it => it.IsNotEqualTo(0)).AtIndex(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item that is not equal to 0 at index 0,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WithMultipleFailures_ShouldIncludeCollectionOnlyOnce()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);

				async Task Act()
					=> await That(subject).HasItemThat(x => x.StartsWith("a").And.EndsWith("b")).AtIndex(0).And
						.HasItemThat(x => x.Contains("c").IgnoringCase()).AtIndex(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item that starts with "a" and ends with "b" at index 0 and has item that contains "c" at least once ignoring case at index 1,
					             but it had item "a" at index 0 and it had item "b" at index 1

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}
		}
	}
}
#endif
