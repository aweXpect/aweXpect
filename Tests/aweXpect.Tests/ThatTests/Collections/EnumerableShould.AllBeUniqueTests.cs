using System.Collections.Generic;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class AllBeUniqueTests
	{
		[Fact]
		public async Task ForMember_ShouldUseCustomComparer()
		{
			IEnumerable<MyUniqueClass> subject = ToEnumerable(["a", "a", "a"]).Select(x => new MyUniqueClass(x));

			async Task Act()
				=> await That(subject).Should().AllBeUnique(x => x.Value).Using(new AllDifferentComparer());

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForMember_WhenAllItemsAreUnique_ShouldSucceed()
		{
			IEnumerable<MyUniqueClass> subject = ToEnumerable(["a", "b", "c"]).Select(x => new MyUniqueClass(x));

			async Task Act()
				=> await That(subject).Should().AllBeUnique(x => x.Value);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForMember_WhenItContainsDuplicates_ShouldFail()
		{
			IEnumerable<MyUniqueClass> subject = ToEnumerable(["a", "b", "c", "a"]).Select(x => new MyUniqueClass(x));

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
			IEnumerable<MyUniqueClass> subject =
				ToEnumerable(["a", "b", "c", "a", "b", "x"]).Select(x => new MyUniqueClass(x));

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
			IEnumerable<string> subject = ToEnumerable(["a", "a", "a"]);

			async Task Act()
				=> await That(subject).Should().AllBeUnique().Using(new AllDifferentComparer());

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenAllItemsAreUnique_ShouldSucceed()
		{
			IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);

			async Task Act()
				=> await That(subject).Should().AllBeUnique();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenItContainsDuplicates_ShouldFail()
		{
			IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "a"]);

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
			IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "a", "b", "x"]);

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

		private sealed class MyUniqueClass(string value)
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
