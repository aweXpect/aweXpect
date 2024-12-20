#if NET8_0_OR_GREATER
using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.Collections;

public sealed partial class AsyncEnumerableShould
{
	public sealed class BeInDescendingOrder
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenItemsAreNotSortedCorrectly_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([3, 3, 2, 1, 3]);

				async Task Act()
					=> await That(subject).Should().BeInDescendingOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be in descending order,
					             but it had 1 before 3 which is not in descending order in [
					               3,
					               3,
					               2,
					               1,
					               3
					             ]
					             """);
			}

			[Fact]
			public async Task WhenItemsAreSortedCorrectly_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([3, 2, 1]);

				async Task Act()
					=> await That(subject).Should().BeInDescendingOrder();

				await That(Act).Should().NotThrow();
			}
		}

		public sealed class StringTests
		{
			[Fact]
			public async Task ShouldNotIgnoreCasing()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["A", "a"]);

				async Task Act()
					=> await That(subject).Should().BeInDescendingOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be in descending order,
					             but it had "A" before "a" which is not in descending order in [
					               "A",
					               "a"
					             ]
					             """);
			}

			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["A", "a"]);

				async Task Act()
					=> await That(subject).Should().BeInDescendingOrder().Using(StringComparer.OrdinalIgnoreCase);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenItemsAreNotSortedCorrectly_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "b", "a", "c"]);

				async Task Act()
					=> await That(subject).Should().BeInDescendingOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be in descending order,
					             but it had "a" before "c" which is not in descending order in [
					               "c",
					               "b",
					               "a",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenItemsAreSortedCorrectly_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "b", "a"]);

				async Task Act()
					=> await That(subject).Should().BeInDescendingOrder();

				await That(Act).Should().NotThrow();
			}
		}
		
		public sealed class MemberTests
		{
			[Fact]
			public async Task WhenItemsAreNotSortedCorrectly_ShouldFail()
			{
				IAsyncEnumerable<MyIntClass> subject = ToAsyncEnumerable([3, 3, 2, 1, 3], x => new MyIntClass(x));

				async Task Act()
					=> await That(subject).Should().BeInDescendingOrder(x => x.Value);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be in descending order for x => x.Value,
					             but it had 1 before 3 which is not in descending order in [
					               MyIntClass {
					                 Value = 3
					               },
					               MyIntClass {
					                 Value = 3
					               },
					               MyIntClass {
					                 Value = 2
					               },
					               MyIntClass {
					                 Value = 1
					               },
					               MyIntClass {
					                 Value = 3
					               }
					             ]
					             """);
			}

			[Fact]
			public async Task WhenItemsAreSortedCorrectly_ShouldSucceed()
			{
				IAsyncEnumerable<MyIntClass> subject = ToAsyncEnumerable([3, 2, 1], x => new MyIntClass(x));

				async Task Act()
					=> await That(subject).Should().BeInDescendingOrder(x => x.Value);

				await That(Act).Should().NotThrow();
			}

			private sealed class MyIntClass(int value)
			{
				public int Value { get; } = value;
			}
		}

		public sealed class StringMemberTests
		{
			[Fact]
			public async Task ShouldNotIgnoreCasing()
			{
				IAsyncEnumerable<MyStringClass> subject = ToAsyncEnumerable(["A", "a"], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).Should().BeInDescendingOrder(x => x.Value);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be in descending order for x => x.Value,
					             but it had "A" before "a" which is not in descending order in [
					               MyStringClass {
					                 Value = "A"
					               },
					               MyStringClass {
					                 Value = "a"
					               }
					             ]
					             """);
			}

			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IAsyncEnumerable<MyStringClass> subject = ToAsyncEnumerable(["A", "a"], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).Should().BeInDescendingOrder(x => x.Value)
						.Using(StringComparer.OrdinalIgnoreCase);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenItemsAreNotSortedCorrectly_ShouldFail()
			{
				IAsyncEnumerable<MyStringClass> subject =
					ToAsyncEnumerable(["c", "b", "a", "c"], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).Should().BeInDescendingOrder(x => x.Value);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be in descending order for x => x.Value,
					             but it had "a" before "c" which is not in descending order in [
					               MyStringClass {
					                 Value = "c"
					               },
					               MyStringClass {
					                 Value = "b"
					               },
					               MyStringClass {
					                 Value = "a"
					               },
					               MyStringClass {
					                 Value = "c"
					               }
					             ]
					             """);
			}

			[Fact]
			public async Task WhenItemsAreSortedCorrectly_ShouldSucceed()
			{
				IAsyncEnumerable<MyStringClass> subject = ToAsyncEnumerable(["c", "b", "a"], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).Should().BeInDescendingOrder(x => x.Value);

				await That(Act).Should().NotThrow();
			}

			private sealed class MyStringClass(string value)
			{
				public string Value { get; } = value;
			}
		}
	}
}
#endif
