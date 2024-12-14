#if NET6_0_OR_GREATER
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
			IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable(["a", "a", "a"], x => new MyClass(x));

			async Task Act()
				=> await That(subject).Should().AllBeUnique(x => x.Value).Using(new AllDifferentComparer());

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForMember_WhenAllItemsAreUnique_ShouldSucceed()
		{
			IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable(["a", "b", "c"], x => new MyClass(x));

			async Task Act()
				=> await That(subject).Should().AllBeUnique(x => x.Value);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForMember_WhenItContainsDuplicates_ShouldFail()
		{
			IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable(["a", "b", "c", "a"], x => new MyClass(x));

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
		public async Task ForMember_WhenItContainsMultipleDuplicates_ShouldFail()
		{
			IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable(["a", "b", "c", "a", "b", "x"], x => new MyClass(x));

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
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "a"]);

			async Task Act()
				=> await That(subject).Should().AllBeUnique().Using(new AllDifferentComparer());

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenAllItemsAreUnique_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);

			async Task Act()
				=> await That(subject).Should().AllBeUnique();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenItContainsDuplicates_ShouldFail()
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
		public async Task WhenItContainsMultipleDuplicates_ShouldFail()
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

		private sealed class MyClass(string value)
		{
			public string Value { get; } = value;
		}

		private class AllDifferentComparer : IEqualityComparer<object>
		{
			public bool Equals(object? x, object? y) => false;

			public int GetHashCode(object obj) => obj.GetHashCode();
		}
	}
}
#endif
