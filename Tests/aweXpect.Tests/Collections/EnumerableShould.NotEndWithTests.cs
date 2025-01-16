#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class NotEndWith
	{
		public sealed class Tests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).Should().NotEndWith(0)
						.And.NotEndWith(0);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task ShouldSupportCaseInsensitiveComparison()
			{
				IEnumerable<string> subject = ToEnumerable(["FOO", "BAR"]);

				async Task Act()
					=> await That(subject).Should().NotEndWith("bar").IgnoringCase();

				await That(Act).Does().Throw<XunitException>()
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
				IEnumerable<MyClass> subject = Factory.GetFibonacciNumbers(x => new MyClass(x), 6);

				async Task Act()
					=> await That(subject).Should().NotEndWith(
						new MyClass(3),
						new MyClass(5),
						new MyClass(8)
					).Equivalent();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not end with [MyClass { Inner = <null>, Value = 3 }, MyClass { Inner = <null>, Value = 5 }, MyClass { Inner = <null>, Value = 8 }] equivalent,
					             but it did in [
					               MyClass {
					                 Inner = <null>,
					                 Value = 1
					               },
					               MyClass {
					                 Inner = <null>,
					                 Value = 1
					               },
					               MyClass {
					                 Inner = <null>,
					                 Value = 2
					               },
					               MyClass {
					                 Inner = <null>,
					                 Value = 3
					               },
					               MyClass {
					                 Inner = <null>,
					                 Value = 5
					               },
					               MyClass {
					                 Inner = <null>,
					                 Value = 8
					               }
					             ]
					             """);
			}

			[Fact]
			public async Task WhenCollectionsAreIdentical_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().NotEndWith(1, 2, 3);

				await That(Act).Does().Throw<XunitException>()
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
				IEnumerable<int> subject = ToEnumerable([0, 0, 1, 2, 3]);
				IEnumerable<int> unexpected = [1, 3];

				async Task Act()
					=> await That(subject).Should().NotEndWith(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedContainsAdditionalElements_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().NotEndWith(0, 0, 1, 2, 3);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsEmpty_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2]);

				async Task Act()
					=> await That(subject).Should().NotEndWith();

				await That(Act).Does().Throw<XunitException>()
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
				IEnumerable<int> subject = ToEnumerable([1]);

				async Task Act()
					=> await That(subject).Should().NotEndWith(null!);

				await That(Act).Does().Throw<ArgumentNullException>()
					.WithParamName("unexpected");
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().NotEndWith();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not end with [],
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectEndsWithUnexpectedValues_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["foo", "bar", "baz"]);
				IEnumerable<string> unexpected = ["bar", "baz"];

				async Task Act()
					=> await That(subject).Should().NotEndWith(unexpected);

				await That(Act).Does().Throw<XunitException>()
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
