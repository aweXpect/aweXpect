#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.Collections;

public sealed partial class AsyncEnumerableShould
{
	public sealed class NotStartWith
	{
		public sealed class Tests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

				async Task Act()
					=> await That(subject).Should().NotStartWith(0)
						.And.NotStartWith(0);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

				async Task Act()
					=> await That(subject).Should().NotStartWith(1, 1, 2, 3, 4);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task ShouldSupportCaseInsensitiveComparison()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["FOO", "BAR"]);

				async Task Act()
					=> await That(subject).Should().NotStartWith("foo").IgnoringCase();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not start with ["foo"] ignoring case,
					             but it did start with [
					               "FOO"
					             ]
					             """);
			}

			[Fact]
			public async Task ShouldSupportEquivalent()
			{
				IAsyncEnumerable<MyClass> subject = Factory.GetAsyncFibonacciNumbers(x => new MyClass(x), 20);
				IEnumerable<MyClass> unexpected = [new(1), new(1), new(2)];

				async Task Act()
					=> await That(subject).Should().NotStartWith(unexpected).Equivalent();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not start with unexpected equivalent,
					             but it did start with [
					               MyClass {
					                 Value = 1
					               },
					               MyClass {
					                 Value = 1
					               },
					               MyClass {
					                 Value = 2
					               }
					             ]
					             """);
			}

			[Fact]
			public async Task WhenCollectionsAreIdentical_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().NotStartWith(1, 2, 3);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not start with [1, 2, 3],
					             but it did start with [
					               1,
					               2,
					               3
					             ]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableHasDifferentStartingElements_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);
				IEnumerable<int> unexpected = [1, 3];

				async Task Act()
					=> await That(subject).Should().NotStartWith(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().NotStartWith();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not start with [],
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectStartsWithUnexpectedValues_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["foo", "bar", "baz"]);
				IEnumerable<string> unexpected = ["foo", "bar"];

				async Task Act()
					=> await That(subject).Should().NotStartWith(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not start with unexpected,
					             but it did start with [
					               "foo",
					               "bar"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenUnexpectedContainsAdditionalElements_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().NotStartWith(1, 2, 3, 4);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsEmpty_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2]);

				async Task Act()
					=> await That(subject).Should().NotStartWith();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not start with [],
					             but it was [
					               1,
					               2
					             ]
					             """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1]);

				async Task Act()
					=> await That(subject).Should().NotStartWith(null!);

				await That(Act).Should().Throw<ArgumentNullException>()
					.WithParamName("unexpected");
			}
		}
	}
}
#endif
