﻿#if NET6_0_OR_GREATER
using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class AsyncEnumerableShould
{
	public sealed class AllBeUniqueTests
	{
		[Fact]
		public async Task ForMember_ShouldUseCustomComparer()
		{
			IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable([1, 1, 1], x => new MyClass(x));

			async Task Act()
				=> await That(subject).Should().AllBeUnique(x => x.Value).Using(new AllDifferentComparer());

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForMember_WhenAllItemsAreUnique_ShouldSucceed()
		{
			IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable([1, 2, 3], x => new MyClass(x));

			async Task Act()
				=> await That(subject).Should().AllBeUnique(x => x.Value);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForMember_WhenItContainsDuplicates_ShouldFail()
		{
			IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable([1, 2, 3, 1], x => new MyClass(x));

			async Task Act()
				=> await That(subject).Should().AllBeUnique(x => x.Value);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             only have unique items for x => x.Value,
				             but it contained 1 duplicate:
				               1
				             """);
		}

		[Fact]
		public async Task ForMember_WhenItContainsMultipleDuplicates_ShouldFail()
		{
			IAsyncEnumerable<MyClass> subject =
				ToAsyncEnumerable([1, 2, 3, 1, 2, -1], x => new MyClass(x));

			async Task Act()
				=> await That(subject).Should().AllBeUnique(x => x.Value);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             only have unique items for x => x.Value,
				             but it contained 2 duplicates:
				               1,
				               2
				             """);
		}

		[Fact]
		public async Task ForStringMember_ShouldUseCustomComparer()
		{
			IAsyncEnumerable<MyStringClass> subject = ToAsyncEnumerable(["a", "a", "a"], x => new MyStringClass(x));

			async Task Act()
				=> await That(subject).Should().AllBeUnique(x => x.Value).Using(new AllDifferentComparer());

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForStringMember_WhenAllItemsAreUnique_ShouldSucceed()
		{
			IAsyncEnumerable<MyStringClass> subject = ToAsyncEnumerable(["a", "b", "c"], x => new MyStringClass(x));

			async Task Act()
				=> await That(subject).Should().AllBeUnique(x => x.Value);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForStringMember_WhenDiffersInCasing_ShouldSucceed()
		{
			IAsyncEnumerable<MyStringClass> subject = ToAsyncEnumerable(["a", "A"], x => new MyStringClass(x));

			async Task Act()
				=> await That(subject).Should().AllBeUnique(x => x.Value);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForStringMember_WhenDiffersInCasingAndCasingIsIgnored_ShouldFail()
		{
			IAsyncEnumerable<MyStringClass> subject = ToAsyncEnumerable(["a", "A"], x => new MyStringClass(x));

			async Task Act()
				=> await That(subject).Should().AllBeUnique(x => x.Value).IgnoringCase();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             only have unique items for x => x.Value ignoring case,
				             but it contained 1 duplicate:
				               "A"
				             """);
		}

		[Fact]
		public async Task ForStringMember_WhenItContainsDuplicates_ShouldFail()
		{
			IAsyncEnumerable<MyStringClass>
				subject = ToAsyncEnumerable(["a", "b", "c", "a"], x => new MyStringClass(x));

			async Task Act()
				=> await That(subject).Should().AllBeUnique(x => x.Value);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             only have unique items for x => x.Value,
				             but it contained 1 duplicate:
				               "a"
				             """);
		}

		[Fact]
		public async Task ForStringMember_WhenItContainsMultipleDuplicates_ShouldFail()
		{
			IAsyncEnumerable<MyStringClass> subject =
				ToAsyncEnumerable(["a", "b", "c", "a", "b", "x"], x => new MyStringClass(x));

			async Task Act()
				=> await That(subject).Should().AllBeUnique(x => x.Value);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             only have unique items for x => x.Value,
				             but it contained 2 duplicates:
				               "a",
				               "b"
				             """);
		}

		[Fact]
		public async Task ShouldUseCustomComparer()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1]);

			async Task Act()
				=> await That(subject).Should().AllBeUnique().Using(new AllDifferentComparer());

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenAllItemsAreUnique_ShouldSucceed()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

			async Task Act()
				=> await That(subject).Should().AllBeUnique();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenItContainsDuplicates_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3, 1]);

			async Task Act()
				=> await That(subject).Should().AllBeUnique();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             only have unique items,
				             but it contained 1 duplicate:
				               1
				             """);
		}

		[Fact]
		public async Task WhenItContainsMultipleDuplicates_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3, 1, 2, -1]);

			async Task Act()
				=> await That(subject).Should().AllBeUnique();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             only have unique items,
				             but it contained 2 duplicates:
				               1,
				               2
				             """);
		}

		[Fact]
		public async Task ForStrings_ShouldUseCustomComparer()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "a"]);

			async Task Act()
				=> await That(subject).Should().AllBeUnique().Using(new AllDifferentComparer());

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForStrings_WhenAllItemsAreUnique_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);

			async Task Act()
				=> await That(subject).Should().AllBeUnique();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForStrings_WhenDiffersInCasing_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "A"]);

			async Task Act()
				=> await That(subject).Should().AllBeUnique();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForStrings_WhenDiffersInCasingAndCasingIsIgnored_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "A"]);

			async Task Act()
				=> await That(subject).Should().AllBeUnique().IgnoringCase();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             only have unique items ignoring case,
				             but it contained 1 duplicate:
				               "A"
				             """);
		}

		[Fact]
		public async Task ForStrings_WhenItContainsDuplicates_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "a"]);

			async Task Act()
				=> await That(subject).Should().AllBeUnique();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             only have unique items,
				             but it contained 1 duplicate:
				               "a"
				             """);
		}

		[Fact]
		public async Task ForStrings_WhenItContainsMultipleDuplicates_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "a", "b", "x"]);

			async Task Act()
				=> await That(subject).Should().AllBeUnique();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             only have unique items,
				             but it contained 2 duplicates:
				               "a",
				               "b"
				             """);
		}

		private sealed class MyStringClass(string value)
		{
			public string Value { get; } = value;
		}

		private class AllDifferentComparer : IEqualityComparer<object>
		{
			bool IEqualityComparer<object>.Equals(object? x, object? y) => false;

			int IEqualityComparer<object>.GetHashCode(object obj) => obj.GetHashCode();
		}
	}
}
#endif