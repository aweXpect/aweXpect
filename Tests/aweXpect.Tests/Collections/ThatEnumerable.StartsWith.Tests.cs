﻿using System.Collections.Generic;
using aweXpect.Equivalency;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed class StartsWith
	{
		public sealed class Tests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).StartsWith(1)
						.And.StartsWith(1);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

				async Task Act()
					=> await That(subject).StartsWith(1, 1, 2, 3, 5);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ShouldSupportEquivalent()
			{
				IEnumerable<MyClass> subject = Factory.GetFibonacciNumbers(x => new MyClass(x), 20);

				async Task Act()
					=> await That(subject).StartsWith(new MyClass(1), new MyClass(1), new MyClass(2)).Equivalent();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenCollectionsAreIdentical_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).StartsWith(1, 2, 3);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableHasDifferentStartingElements_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3,]);
				IEnumerable<int> expected = [1, 3,];

				async Task Act()
					=> await That(subject).StartsWith(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             starts with expected,
					             but it contained 2 at index 1 instead of 3
					             
					             Collection:
					             [
					               1,
					               2,
					               3,
					               (… and maybe others)
					             ]
					             """);
			}

			[Fact]
			public async Task WhenExpectedContainsAdditionalElements_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3,]);
				IEnumerable<int> expected = [1, 2, 3, 4,];

				async Task Act()
					=> await That(subject).StartsWith(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             starts with expected,
					             but it contained only 3 items and misses 1 items: [
					               4
					             ]
					             
					             Collection:
					             [1, 2, 3]
					             """);
			}

			[Fact]
			public async Task WhenExpectedIsEmpty_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).StartsWith();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1,]);

				async Task Act()
					=> await That(subject).StartsWith(null!);

				await That(Act).Throws<ArgumentNullException>()
					.WithParamName("expected");
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).StartsWith();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             starts with [],
					             but it was <null>
					             """);
			}
		}

		public sealed class StringTests
		{
			[Fact]
			public async Task ShouldIncludeOptionsInFailureMessage()
			{
				IEnumerable<string> subject = ToEnumerable(["foo", "bar", "baz",]);

				async Task Act()
					=> await That(subject).StartsWith("FOO", "BAZ").IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             starts with ["FOO", "BAZ"] ignoring case,
					             but it contained "bar" at index 1 instead of "BAZ"
					             
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
				IEnumerable<string> subject = ToEnumerable(["foo", "bar", "baz",]);

				async Task Act()
					=> await That(subject).StartsWith("FOO", "BAR").IgnoringCase();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectStartsWithExpectedValues_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["foo", "bar", "baz",]);
				IEnumerable<string> expected = ToEnumerable(["foo", "bar",]);

				async Task Act()
					=> await That(subject).StartsWith(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
