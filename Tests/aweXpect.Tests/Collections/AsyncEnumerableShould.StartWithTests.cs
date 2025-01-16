#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class AsyncEnumerableShould
{
	public sealed class StartWith
	{
		public sealed class Tests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

				async Task Act()
					=> await That(subject).Should().StartWith(1)
						.And.StartWith(1);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

				async Task Act()
					=> await That(subject).Should().StartWith(1, 1, 2, 3, 5);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task ShouldSupportCaseInsensitiveComparison()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["FOO", "BAR"]);

				async Task Act()
					=> await That(subject).Should().StartWith("foo").IgnoringCase();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task ShouldSupportEquivalent()
			{
				IAsyncEnumerable<MyClass> subject = Factory.GetAsyncFibonacciNumbers(x => new MyClass(x), 20);

				async Task Act()
					=> await That(subject).Should().StartWith(
						new MyClass(1),
						new MyClass(1),
						new MyClass(2)
					).Equivalent();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenCollectionsAreIdentical_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().StartWith(1, 2, 3);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenEnumerableHasDifferentStartingElements_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);
				IEnumerable<int> expected = [1, 3];

				async Task Act()
					=> await That(subject).Should().StartWith(expected);

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
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().StartWith(1, 2, 3, 4);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             start with [1, 2, 3, 4],
					             but it contained only 3 items and misses 1 items: [
					               4
					             ]
					             """);
			}

			[Fact]
			public async Task WhenExpectedIsEmpty_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).Should().StartWith();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1]);

				async Task Act()
					=> await That(subject).Should().StartWith(null!);

				await That(Act).Does().Throw<ArgumentNullException>()
					.WithParamName("expected");
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().StartWith();

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
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["foo", "bar", "baz"]);
				IEnumerable<string> expected = ["foo", "bar"];

				async Task Act()
					=> await That(subject).Should().StartWith(expected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
#endif
