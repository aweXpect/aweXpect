﻿using System.Collections.Generic;
using System.Linq;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class AllBeUnique
	{
		public sealed class Tests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IEnumerable<int> subject = ToEnumerable([1, 1, 1]);

				async Task Act()
					=> await That(subject).Should().AllBeUnique().Using(new AllDifferentComparer());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenAllItemsAreUnique_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().AllBeUnique();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3, 1]);

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
				IEnumerable<int> subject = ToEnumerable([1, 2, 3, 1, 2, -1]);

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
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().AllBeUnique();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique items,
					             but it was <null>
					             """);
			}
		}

		public sealed class StringTests
		{
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
			public async Task WhenDiffersInCasing_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "A"]);

				async Task Act()
					=> await That(subject).Should().AllBeUnique();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenDiffersInCasingAndCasingIsIgnored_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "A"]);

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

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().AllBeUnique();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique items,
					             but it was <null>
					             """);
			}
		}

		public sealed class MemberTests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IEnumerable<MyClass> subject = ToEnumerable([1, 1, 1]).Select(x => new MyClass(x));

				async Task Act()
					=> await That(subject).Should().AllBeUnique(x => x.Value).Using(new AllDifferentComparer());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenAllItemsAreUnique_ShouldSucceed()
			{
				IEnumerable<MyClass> subject = ToEnumerable([1, 2, 3]).Select(x => new MyClass(x));

				async Task Act()
					=> await That(subject).Should().AllBeUnique(x => x.Value);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IEnumerable<MyClass> subject = ToEnumerable([1, 2, 3, 1]).Select(x => new MyClass(x));

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
			public async Task WhenItContainsMultipleDuplicates_ShouldFail()
			{
				IEnumerable<MyClass> subject =
					ToEnumerable([1, 2, 3, 1, 2, -1]).Select(x => new MyClass(x));

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
		}

		public sealed class StringMemberTests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IEnumerable<MyStringClass> subject = ToEnumerable(["a", "a", "a"]).Select(x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).Should().AllBeUnique(x => x.Value).Using(new AllDifferentComparer());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenAllItemsAreUnique_ShouldSucceed()
			{
				IEnumerable<MyStringClass> subject = ToEnumerable(["a", "b", "c"]).Select(x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).Should().AllBeUnique(x => x.Value);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenDiffersInCasing_ShouldSucceed()
			{
				IEnumerable<MyStringClass> subject = ToEnumerable(["a", "A"]).Select(x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).Should().AllBeUnique(x => x.Value);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenDiffersInCasingAndCasingIsIgnored_ShouldFail()
			{
				IEnumerable<MyStringClass> subject = ToEnumerable(["a", "A"]).Select(x => new MyStringClass(x));

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
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IEnumerable<MyStringClass> subject =
					ToEnumerable(["a", "b", "c", "a"]).Select(x => new MyStringClass(x));

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
			public async Task WhenItContainsMultipleDuplicates_ShouldFail()
			{
				IEnumerable<MyStringClass> subject =
					ToEnumerable(["a", "b", "c", "a", "b", "x"]).Select(x => new MyStringClass(x));

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

			private sealed class MyStringClass(string value)
			{
				public string Value { get; } = value;
			}
		}
	}
}
