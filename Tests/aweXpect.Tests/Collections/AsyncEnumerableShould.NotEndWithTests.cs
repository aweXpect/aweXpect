#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.Collections;

public sealed partial class AsyncEnumerableShould
{
	public sealed class NotEndWith
	{
		public sealed class Tests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

				async Task Act()
					=> await That(subject).Should().NotEndWith(0)
						.And.NotEndWith(0);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task ShouldSupportCaseInsensitiveComparison()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["FOO", "BAR"]);

				async Task Act()
					=> await That(subject).Should().NotEndWith("bar").IgnoringCase();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not end with ["bar"] ignoring case,
					             but it did in [
					               "FOO",
					               "BAR"
					             ]
					             """);
			}

			[Fact]
			public async Task ShouldSupportEquivalent()
			{
				IAsyncEnumerable<MyClass> subject = Factory.GetAsyncFibonacciNumbers(x => new MyClass(x), 6);

				async Task Act()
					=> await That(subject).Should().NotEndWith(
						new MyClass(3),
						new MyClass(5),
						new MyClass(8)
					).Equivalent();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not end with [MyClass { Value = 3 }, MyClass { Value = 5 }, MyClass { Value = 8 }] equivalent,
					             but it did in [
					               MyClass {
					                 Value = 1
					               },
					               MyClass {
					                 Value = 1
					               },
					               MyClass {
					                 Value = 2
					               },
					               MyClass {
					                 Value = 3
					               },
					               MyClass {
					                 Value = 5
					               },
					               MyClass {
					                 Value = 8
					               }
					             ]
					             """);
			}

			[Fact]
			public async Task WhenCollectionsAreIdentical_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().NotEndWith(1, 2, 3);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not end with [1, 2, 3],
					             but it did in [
					               1,
					               2,
					               3
					             ]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableHasDifferentEndingElements_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([0, 0, 1, 2, 3]);
				IEnumerable<int> unexpected = [1, 3];

				async Task Act()
					=> await That(subject).Should().NotEndWith(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedContainsAdditionalElements_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().NotEndWith(0, 0, 1, 2, 3);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsEmpty_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2]);

				async Task Act()
					=> await That(subject).Should().NotEndWith();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not end with [],
					             but it did in [
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
					=> await That(subject).Should().NotEndWith(null!);

				await That(Act).Should().Throw<ArgumentNullException>()
					.WithParamName("unexpected");
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().NotEndWith();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not end with [],
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectEndsWithUnexpectedValues_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["foo", "bar", "baz"]);
				IEnumerable<string> unexpected = ["bar", "baz"];

				async Task Act()
					=> await That(subject).Should().NotEndWith(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not end with unexpected,
					             but it did in [
					               "foo",
					               "bar",
					               "baz"
					             ]
					             """);
			}
		}
	}
}
#endif
