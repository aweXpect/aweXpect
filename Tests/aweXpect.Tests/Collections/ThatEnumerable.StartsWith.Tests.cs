using System.Collections.Generic;
using aweXpect.Tests.TestHelpers;

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

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

				async Task Act()
					=> await That(subject).StartsWith(1, 1, 2, 3, 5);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task ShouldSupportEquivalent()
			{
				IEnumerable<MyClass> subject = Factory.GetFibonacciNumbers(x => new MyClass(x), 20);

				async Task Act()
					=> await That(subject).StartsWith(new MyClass(1), new MyClass(1), new MyClass(2)).Equivalent();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenCollectionsAreIdentical_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).StartsWith(1, 2, 3);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenEnumerableHasDifferentStartingElements_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);
				IEnumerable<int> expected = [1, 3];

				async Task Act()
					=> await That(subject).StartsWith(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             start with expected,
					             but it contained 2 at index 1 instead of 3
					             """);
			}

			[Fact]
			public async Task WhenExpectedContainsAdditionalElements_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);
				IEnumerable<int> expected = [1, 2, 3, 4];

				async Task Act()
					=> await That(subject).StartsWith(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             start with expected,
					             but it contained only 3 items and misses 1 items: [
					               4
					             ]
					             """);
			}

			[Fact]
			public async Task WhenExpectedIsEmpty_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).StartsWith();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1]);

				async Task Act()
					=> await That(subject).StartsWith(null!);

				await That(Act).Does().Throw<ArgumentNullException>()
					.WithParamName("expected");
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).StartsWith();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             start with [],
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectStartsWithExpectedValues_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["foo", "bar", "baz"]);
				IEnumerable<string> expected = ToEnumerable(["foo", "bar"]);

				async Task Act()
					=> await That(subject).StartsWith(expected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
