﻿#if NET8_0_OR_GREATER
using System.Collections.Immutable;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class MoreThan
	{
		public sealed class ImmutableItemsTests
		{
			[Fact]
			public async Task WhenEnumerableContainsEnoughEqualItems_ShouldSucceed()
			{
				ImmutableArray<int> subject = [1, 1, 1, 1, 2, 2, 3,];

				async Task Act()
					=> await That(subject).MoreThan(3).AreEqualTo(1);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooFewEqualItems_ShouldFail()
			{
				ImmutableArray<int> subject = [1, 1, 1, 1, 2, 2, 3,];

				async Task Act()
					=> await That(subject).MoreThan(5).AreEqualTo(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equal to 1 for more than 5 items,
					             but only 4 of 7 were

					             Not matching items:
					             [2, 2, 3]

					             Collection:
					             [1, 1, 1, 1, 2, 2, 3]
					             """);
			}
		}

		public sealed class ImmutableStringTests
		{
			[Fact]
			public async Task ShouldSupportIgnoringCase()
			{
				ImmutableArray<string?> subject = ["foo", "FOO", "bar",];

				async Task Act()
					=> await That(subject).MoreThan(2).AreEqualTo("foo").IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equal to "foo" ignoring case for more than 2 items,
					             but only 2 of 3 were

					             Not matching items:
					             [
					               "bar"
					             ]

					             Collection:
					             [
					               "foo",
					               "FOO",
					               "bar"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsExpectedNumberOfEqualItems_ShouldFail()
			{
				ImmutableArray<string> subject = ["foo", "foo", "bar",];

				async Task Act()
					=> await That(subject)!.MoreThan(2).AreEqualTo("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equal to "foo" for more than 2 items,
					             but only 2 of 3 were

					             Not matching items:
					             [
					               "bar"
					             ]

					             Collection:
					             [
					               "foo",
					               "foo",
					               "bar"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsTooFewEqualItems_ShouldFail()
			{
				ImmutableArray<string> subject = ["foo", "FOO", "foo", "bar",];

				async Task Act()
					=> await That(subject)!.MoreThan(2).AreEqualTo("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equal to "foo" for more than 2 items,
					             but only 2 of 4 were

					             Not matching items:
					             [
					               "FOO",
					               "bar"
					             ]

					             Collection:
					             [
					               "foo",
					               "FOO",
					               "foo",
					               "bar"
					             ]
					             """);
			}
		}
	}
}
#endif
