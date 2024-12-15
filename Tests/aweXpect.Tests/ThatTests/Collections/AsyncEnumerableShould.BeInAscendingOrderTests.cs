﻿#if NET6_0_OR_GREATER
using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class AsyncEnumerableShould
{
	public sealed class BeInAscendingOrderTests
	{
		[Fact]
		public async Task ForMember_WhenItemsAreNotSortedCorrectly_ShouldFail()
		{
			IAsyncEnumerable<MyIntClass> subject = ToAsyncEnumerable([1, 1, 2, 3, 1], x => new MyIntClass(x));

			async Task Act()
				=> await That(subject).Should().BeInAscendingOrder(x => x.Value);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be in ascending order for x => x.Value,
				             but it had 3 before 1 which is not in ascending order in [
				               MyIntClass {
				                 Value = 1
				               },
				               MyIntClass {
				                 Value = 1
				               },
				               MyIntClass {
				                 Value = 2
				               },
				               MyIntClass {
				                 Value = 3
				               },
				               MyIntClass {
				                 Value = 1
				               }
				             ]
				             """);
		}

		[Fact]
		public async Task ForMember_WhenItemsAreSortedCorrectly_ShouldSucceed()
		{
			IAsyncEnumerable<MyIntClass> subject = ToAsyncEnumerable([1, 2, 3], x => new MyIntClass(x));

			async Task Act()
				=> await That(subject).Should().BeInAscendingOrder(x => x.Value);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForStringMember_ShouldUseCustomComparer()
		{
			IAsyncEnumerable<MyStringClass> subject = ToAsyncEnumerable(["a", "A"], x => new MyStringClass(x));

			async Task Act()
				=> await That(subject).Should().BeInAscendingOrder(x => x.Value)
					.Using(StringComparer.OrdinalIgnoreCase);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForStringMember_ShouldNotIgnoreCasing()
		{
			IAsyncEnumerable<MyStringClass> subject = ToAsyncEnumerable(["a", "A"], x => new MyStringClass(x));

			async Task Act()
				=> await That(subject).Should().BeInAscendingOrder(x => x.Value);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be in ascending order for x => x.Value,
				             but it had "a" before "A" which is not in ascending order in [
				               MyStringClass {
				                 Value = "a"
				               },
				               MyStringClass {
				                 Value = "A"
				               }
				             ]
				             """);
		}

		[Fact]
		public async Task ForStringMember_WhenItemsAreNotSortedCorrectly_ShouldFail()
		{
			IAsyncEnumerable<MyStringClass> subject = ToAsyncEnumerable(["a", "b", "c", "a"], x => new MyStringClass(x));

			async Task Act()
				=> await That(subject).Should().BeInAscendingOrder(x => x.Value);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be in ascending order for x => x.Value,
				             but it had "c" before "a" which is not in ascending order in [
				               MyStringClass {
				                 Value = "a"
				               },
				               MyStringClass {
				                 Value = "b"
				               },
				               MyStringClass {
				                 Value = "c"
				               },
				               MyStringClass {
				                 Value = "a"
				               }
				             ]
				             """);
		}

		[Fact]
		public async Task ForStringMember_WhenItemsAreSortedCorrectly_ShouldSucceed()
		{
			IAsyncEnumerable<MyStringClass> subject = ToAsyncEnumerable(["a", "b", "c"], x => new MyStringClass(x));

			async Task Act()
				=> await That(subject).Should().BeInAscendingOrder(x => x.Value);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenItemsAreNotSortedCorrectly_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 2, 3, 1]);

			async Task Act()
				=> await That(subject).Should().BeInAscendingOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be in ascending order,
				             but it had 3 before 1 which is not in ascending order in [
				               1,
				               1,
				               2,
				               3,
				               1
				             ]
				             """);
		}

		[Fact]
		public async Task WhenItemsAreSortedCorrectly_ShouldSucceed()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

			async Task Act()
				=> await That(subject).Should().BeInAscendingOrder();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForStrings_ShouldUseCustomComparer()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "A"]);

			async Task Act()
				=> await That(subject).Should().BeInAscendingOrder().Using(StringComparer.OrdinalIgnoreCase);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForStrings_ShouldNotIgnoreCasing()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "A"]);

			async Task Act()
				=> await That(subject).Should().BeInAscendingOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be in ascending order,
				             but it had "a" before "A" which is not in ascending order in [
				               "a",
				               "A"
				             ]
				             """);
		}

		[Fact]
		public async Task ForStrings_WhenItemsAreNotSortedCorrectly_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "a"]);

			async Task Act()
				=> await That(subject).Should().BeInAscendingOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be in ascending order,
				             but it had "c" before "a" which is not in ascending order in [
				               "a",
				               "b",
				               "c",
				               "a"
				             ]
				             """);
		}

		[Fact]
		public async Task ForStrings_WhenItemsAreSortedCorrectly_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);

			async Task Act()
				=> await That(subject).Should().BeInAscendingOrder();

			await That(Act).Should().NotThrow();
		}

		private sealed class MyIntClass(int value)
		{
			public int Value { get; } = value;
		}

		private sealed class MyStringClass(string value)
		{
			public string Value { get; } = value;
		}
	}
}
#endif
