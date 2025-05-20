﻿#if NET8_0_OR_GREATER
using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed class IsNotInAscendingOrder
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenItemsAreNotSortedCorrectly_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 2, 3, 1,]);

				async Task Act()
					=> await That(subject).IsNotInAscendingOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenItemsAreSortedCorrectly_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).IsNotInAscendingOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not in ascending order,
					             but it was
					             
					             Collection:
					             [1, 2, 3]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).IsNotInAscendingOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not in ascending order,
					             but it was <null>
					             """);
			}
		}

		public sealed class NegatedTests
		{
			[Fact]
			public async Task WhenItemsAreNotSortedCorrectly_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 2, 3, 1,]);

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.IsNotInAscendingOrder());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is in ascending order,
					             but it had 3 before 1 which is not in ascending order
					             
					             Collection:
					             [1, 1, 2, 3, 1]
					             """);
			}

			[Fact]
			public async Task WhenItemsAreSortedCorrectly_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.IsNotInAscendingOrder());

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class StringTests
		{
			[Fact]
			public async Task ShouldNotIgnoreCasing()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "A",]);

				async Task Act()
					=> await That(subject).IsNotInAscendingOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "A",]);

				async Task Act()
					=> await That(subject).IsNotInAscendingOrder().Using(StringComparer.OrdinalIgnoreCase);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not in ascending order using OrdinalIgnoreCaseComparer,
					             but it was
					             
					             Collection:
					             [
					               "a",
					               "A"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenItemsAreNotSortedCorrectly_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "a",]);

				async Task Act()
					=> await That(subject).IsNotInAscendingOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenItemsAreSortedCorrectly_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);

				async Task Act()
					=> await That(subject).IsNotInAscendingOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not in ascending order,
					             but it was
					             
					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject).IsNotInAscendingOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not in ascending order,
					             but it was <null>
					             """);
			}
		}

		public sealed class MemberTests
		{
			[Fact]
			public async Task WhenItemsAreNotSortedCorrectly_ShouldSucceed()
			{
				IAsyncEnumerable<MyIntClass> subject = ToAsyncEnumerable([1, 1, 2, 3, 1,], x => new MyIntClass(x));

				async Task Act()
					=> await That(subject).IsNotInAscendingOrder(x => x.Value);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenItemsAreSortedCorrectly_ShouldFail()
			{
				IAsyncEnumerable<MyIntClass> subject = ToAsyncEnumerable([1, 2, 3,], x => new MyIntClass(x));

				async Task Act()
					=> await That(subject).IsNotInAscendingOrder(x => x.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not in ascending order for x => x.Value,
					             but it was
					             
					             Collection:
					             [
					               ThatAsyncEnumerable.IsNotInAscendingOrder.MyIntClass {
					                 Value = 1
					               },
					               ThatAsyncEnumerable.IsNotInAscendingOrder.MyIntClass {
					                 Value = 2
					               },
					               ThatAsyncEnumerable.IsNotInAscendingOrder.MyIntClass {
					                 Value = 3
					               }
					             ]
					             """);
			}
		}

		public sealed class NegatedMemberTests
		{
			[Fact]
			public async Task WhenItemsAreNotSortedCorrectly_ShouldFail()
			{
				IAsyncEnumerable<MyIntClass> subject = ToAsyncEnumerable([1, 1, 2, 3, 1,], x => new MyIntClass(x));

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.IsNotInAscendingOrder(x => x.Value));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is in ascending order for x => x.Value,
					             but it had 3 before 1 which is not in ascending order
					             
					             Collection:
					             [
					               ThatAsyncEnumerable.IsNotInAscendingOrder.MyIntClass {
					                 Value = 1
					               },
					               ThatAsyncEnumerable.IsNotInAscendingOrder.MyIntClass {
					                 Value = 1
					               },
					               ThatAsyncEnumerable.IsNotInAscendingOrder.MyIntClass {
					                 Value = 2
					               },
					               ThatAsyncEnumerable.IsNotInAscendingOrder.MyIntClass {
					                 Value = 3
					               },
					               ThatAsyncEnumerable.IsNotInAscendingOrder.MyIntClass {
					                 Value = 1
					               }
					             ]
					             """);
			}

			[Fact]
			public async Task WhenItemsAreSortedCorrectly_ShouldSucceed()
			{
				IAsyncEnumerable<MyIntClass> subject = ToAsyncEnumerable([1, 2, 3,], x => new MyIntClass(x));

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.IsNotInAscendingOrder(x => x.Value));

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class StringMemberTests
		{
			[Fact]
			public async Task ShouldNotIgnoreCasing()
			{
				IAsyncEnumerable<MyStringClass> subject = ToAsyncEnumerable(["a", "A",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).IsNotInAscendingOrder(x => x.Value);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IAsyncEnumerable<MyStringClass> subject = ToAsyncEnumerable(["a", "A",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).IsNotInAscendingOrder(x => x.Value)
						.Using(StringComparer.OrdinalIgnoreCase);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not in ascending order using OrdinalIgnoreCaseComparer for x => x.Value,
					             but it was
					             
					             Collection:
					             [
					               ThatAsyncEnumerable.IsNotInAscendingOrder.StringMemberTests.MyStringClass {
					                 Value = "a"
					               },
					               ThatAsyncEnumerable.IsNotInAscendingOrder.StringMemberTests.MyStringClass {
					                 Value = "A"
					               }
					             ]
					             """);
			}

			[Fact]
			public async Task WhenItemsAreNotSortedCorrectly_ShouldSucceed()
			{
				IAsyncEnumerable<MyStringClass> subject =
					ToAsyncEnumerable(["a", "b", "c", "a",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).IsNotInAscendingOrder(x => x.Value);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenItemsAreSortedCorrectly_ShouldFail()
			{
				IAsyncEnumerable<MyStringClass>
					subject = ToAsyncEnumerable(["a", "b", "c",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).IsNotInAscendingOrder(x => x.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not in ascending order for x => x.Value,
					             but it was
					             
					             Collection:
					             [
					               ThatAsyncEnumerable.IsNotInAscendingOrder.StringMemberTests.MyStringClass {
					                 Value = "a"
					               },
					               ThatAsyncEnumerable.IsNotInAscendingOrder.StringMemberTests.MyStringClass {
					                 Value = "b"
					               },
					               ThatAsyncEnumerable.IsNotInAscendingOrder.StringMemberTests.MyStringClass {
					                 Value = "c"
					               }
					             ]
					             """);
			}

			private sealed class MyStringClass(string value)
			{
				public string Value { get; } = value;
			}
		}

		private sealed class MyIntClass(int value)
		{
			public int Value { get; } = value;
		}
	}
}
#endif
