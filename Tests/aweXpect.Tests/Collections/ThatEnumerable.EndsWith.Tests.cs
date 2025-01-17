#if NET8_0_OR_GREATER
using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed class EndsWith
	{
		public sealed class Tests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).EndsWith(1)
						.And.EndsWith(1);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task ShouldSupportCaseInsensitiveComparison()
			{
				IEnumerable<string> subject = ToEnumerable(["FOO", "BAR"]);

				async Task Act()
					=> await That(subject).EndsWith("bar").IgnoringCase();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task ShouldSupportEquivalent()
			{
				IEnumerable<MyClass> subject = Factory.GetFibonacciNumbers(x => new MyClass(x), 6);

				async Task Act()
					=> await That(subject).EndsWith(
						new MyClass(3),
						new MyClass(5),
						new MyClass(8)
					).Equivalent();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenCollectionsAreIdentical_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).EndsWith(1, 2, 3);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenEnumerableHasDifferentEndingElements_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([0, 0, 1, 2, 3]);
				IEnumerable<int> expected = [1, 3];

				async Task Act()
					=> await That(subject).EndsWith(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             end with expected,
					             but it contained 2 at index 3 instead of 1
					             """);
			}

			[Fact]
			public async Task WhenExpectedContainsAdditionalElements_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).EndsWith(0, 0, 1, 2, 3);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             end with [0, 0, 1, 2, 3],
					             but it contained only 3 items and misses 2 items: [
					               0,
					               0
					             ]
					             """);
			}

			[Fact]
			public async Task WhenExpectedIsEmpty_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).EndsWith();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1]);

				async Task Act()
					=> await That(subject).EndsWith(null!);

				await That(Act).Does().Throw<ArgumentNullException>()
					.WithParamName("expected");
			}

			[Fact]
			public async Task WhenSubjectEndsWithExpectedValues_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["foo", "bar", "baz"]);
				IEnumerable<string> expected = ["bar", "baz"];

				async Task Act()
					=> await That(subject).EndsWith(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).EndsWith();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             end with [],
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
