#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed class HasItem
	{
		public sealed class PredicateTests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

				async Task Act()
					=> await That(subject).HasItem(_ => true).AtAnyIndex()
						.And.HasItem(_ => true).AtIndex(0);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

				async Task Act()
					=> await That(subject).HasItem(a => a == 5).AtAnyIndex();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsDifferentItemAtGivenIndex_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(0, 1, 2);

				async Task Act()
					=> await That(subject).HasItem(_ => false).AtIndex(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item _ => false at index 2,
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
					=> await That(subject).HasItem(_ => true).AtIndex(2);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsNoItemAtGivenIndex_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(0, 1, 2);

				async Task Act()
					=> await That(subject).HasItem(_ => true).AtIndex(3);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item _ => true at index 3,
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
					=> await That(subject).HasItem(_ => true).AtAnyIndex();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item _ => true,
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
					=> await That(subject).HasItem(_ => true).AtAnyIndex();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item _ => true,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_WithFixedIndex_ShouldFail()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).HasItem(_ => true).AtIndex(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item _ => true at index 0,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WithMultipleFailures_ShouldIncludeCollectionOnlyOnce()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);

				async Task Act()
					=> await That(subject).HasItem(_ => false).AtIndex(0).And.HasItem(_ => false).AtIndex(1).And
						.HasItem(_ => false)
						.AtAnyIndex();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item _ => false at index 0 and has item _ => false at index 1 and has item _ => false,
					             but it had item "a" at index 0 and it had item "b" at index 1 and it did not match at any index

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}
		}

		public sealed class ItemTests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

				async Task Act()
					=> await That(subject).HasItem(1).AtAnyIndex()
						.And.HasItem(1).AtIndex(0);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

				async Task Act()
					=> await That(subject).HasItem(5).AtAnyIndex();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsDifferentItemAtGivenIndex_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(0, 1, 2, 3, 4, 5);

				async Task Act()
					=> await That(subject).HasItem(3).AtIndex(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item 3 at index 2,
					             but it had item 2 at index 2

					             Collection:
					             [0, 1, 2, 3, 4, 5]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsExpectedItemAtGivenIndex_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(0, 1, 2, 3, 4, 5);

				async Task Act()
					=> await That(subject).HasItem(2).AtIndex(2);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsNoItemAtGivenIndex_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(0, 1, 2);

				async Task Act()
					=> await That(subject).HasItem(5).AtIndex(3);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item 5 at index 3,
					             but it did not contain any item at index 3

					             Collection:
					             [0, 1, 2]
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableIsEmpty_ShouldFail(int expected)
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).HasItem(expected).AtAnyIndex();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              has item {expected},
					              but it did not contain any item

					              Collection:
					              []
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_WithAnyIndex_ShouldFail()
			{
				int expected = 42;
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).HasItem(expected).AtAnyIndex();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item 42,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_WithFixedIndex_ShouldFail()
			{
				int expected = 42;
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).HasItem(expected).AtIndex(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item 42 at index 0,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WithMultipleFailures_ShouldIncludeCollectionOnlyOnce()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);

				async Task Act()
					=> await That(subject).HasItem("d").AtIndex(0).And.HasItem("e").AtIndex(1).And.HasItem("f")
						.AtAnyIndex();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item "d" at index 0 and has item "e" at index 1 and has item "f",
					             but it had item "a" at index 0 and it had item "b" at index 1 and it did not match at any index

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}
		}

		public sealed class EquivalentTests
		{
			[Fact]
			public async Task WhenEquivalentItemIsFound_ShouldSucceed()
			{
				IAsyncEnumerable<MyClass> subject =
					ToAsyncEnumerable(Factory.GetFibonacciNumbers(20).Select(x => new MyClass(x)).ToArray());
				MyClass expected = new(5);

				async Task Act()
					=> await That(subject).HasItem(expected).Equivalent().AtAnyIndex();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEquivalentItemIsNotFound_ShouldFail()
			{
				IAsyncEnumerable<MyClass> subject =
					ToAsyncEnumerable(Factory.GetFibonacciNumbers(20).Select(x => new MyClass(x)).ToArray());
				MyClass expected = new(4);

				async Task Act()
					=> await That(subject).HasItem(expected).Equivalent().AtAnyIndex();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item MyClass {
					               StringValue = "",
					               Value = 4
					             } equivalent,
					             but it did not match at any index

					             Collection:
					             [
					               MyClass {
					                 StringValue = "",
					                 Value = 1
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 1
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 2
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 3
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 5
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 8
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 13
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 21
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 34
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 55
					               },
					               …
					             ]
					             """);
			}
		}

		public sealed class UsingTests
		{
			[Fact]
			public async Task WithAllDifferentComparer_ShouldFail()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).HasItem(1).Using(new AllDifferentComparer()).AtAnyIndex();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item 1 using AllDifferentComparer,
					             but it did not match at any index

					             Collection:
					             [
					               1,
					               1,
					               2,
					               3,
					               5,
					               8,
					               13,
					               21,
					               34,
					               55,
					               …
					             ]
					             """);
			}

			[Fact]
			public async Task WithAllEqualComparer_ShouldSucceed()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).HasItem(4).Using(new AllEqualComparer()).AtAnyIndex();

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
