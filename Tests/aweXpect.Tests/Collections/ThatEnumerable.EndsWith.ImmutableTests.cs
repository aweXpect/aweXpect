﻿#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Collections.Immutable;
using aweXpect.Equivalency;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class EndsWith
	{
		public sealed class ImmutableTests
		{
			[Fact]
			public async Task ShouldSupportEquivalent()
			{
				ImmutableArray<MyClass> subject = [..Factory.GetFibonacciNumbers(x => new MyClass(x), 6),];

				async Task Act()
					=> await That(subject).EndsWith(
						new MyClass(3),
						new MyClass(5),
						new MyClass(8)
					).Equivalent();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenCollectionsAreIdentical_ShouldSucceed()
			{
				ImmutableArray<int> subject = [1, 2, 3,];

				async Task Act()
					=> await That(subject).EndsWith(1, 2, 3);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableHasDifferentEndingElements_ShouldFail()
			{
				ImmutableArray<int> subject = [0, 0, 1, 2, 3,];
				IEnumerable<int> expected = [1, 3,];

				async Task Act()
					=> await That(subject).EndsWith(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             ends with expected,
					             but it contained 2 at index 3 instead of 1

					             Collection:
					             [0, 0, 1, 2, 3, (… and maybe others)]
					             """);
			}

			[Fact]
			public async Task WhenExpectedContainsAdditionalElements_ShouldFail()
			{
				ImmutableArray<int> subject = [1, 2, 3,];

				async Task Act()
					=> await That(subject).EndsWith(0, 0, 1, 2, 3);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             ends with [0, 0, 1, 2, 3],
					             but it contained only 3 items and misses 2 items: [
					               0,
					               0
					             ]

					             Collection:
					             [1, 2, 3]
					             """);
			}

			[Fact]
			public async Task WhenExpectedIsEmpty_ShouldSucceed()
			{
				ImmutableArray<int> subject = [];

				async Task Act()
					=> await That(subject).EndsWith();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				ImmutableArray<int> subject = [1,];

				async Task Act()
					=> await That(subject).EndsWith(null!);

				await That(Act).Throws<ArgumentNullException>()
					.WithParamName("expected");
			}
		}

		public sealed class ImmutableStringTests
		{
			[Fact]
			public async Task ShouldIncludeOptionsInFailureMessage()
			{
				ImmutableArray<string?> subject = ["foo", "bar", "baz",];

				async Task Act()
					=> await That(subject).EndsWith("FOO", "BAZ").IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             ends with ["FOO", "BAZ"] ignoring case,
					             but it contained "bar" at index 1 instead of "FOO"

					             Collection:
					             [
					               "foo",
					               "bar",
					               "baz",
					               (… and maybe others)
					             ]
					             """);
			}

			[Fact]
			public async Task ShouldSupportIgnoringCase()
			{
				ImmutableArray<string> subject = ["foo", "bar", "baz",];

				async Task Act()
					=> await That(subject)!.EndsWith("BAR", "BAZ").IgnoringCase();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectEndsWithExpectedValues_ShouldSucceed()
			{
				ImmutableArray<string?> subject = ["foo", "bar", "baz",];
				IEnumerable<string> expected = ["bar", "baz",];

				async Task Act()
					=> await That(subject).EndsWith(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
