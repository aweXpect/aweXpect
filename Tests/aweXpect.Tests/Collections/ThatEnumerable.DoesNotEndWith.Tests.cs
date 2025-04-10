#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Equivalency;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed class DoesNotEndWith
	{
		public sealed class Tests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).DoesNotEndWith(0)
						.And.DoesNotEndWith(0);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ShouldSupportCaseInsensitiveComparison()
			{
				IEnumerable<string> subject = ToEnumerable(["FOO", "BAR",]);

				async Task Act()
					=> await That(subject).DoesNotEndWith("bar").IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not end with ["bar"] ignoring case,
					             but it did end with [
					               "BAR"
					             ]
					             """);
			}

			[Fact]
			public async Task ShouldSupportEquivalent()
			{
				IEnumerable<MyClass> subject = Factory.GetFibonacciNumbers(x => new MyClass(x), 6);

				async Task Act()
					=> await That(subject).DoesNotEndWith(
						new MyClass(3),
						new MyClass(5),
						new MyClass(8)
					).Equivalent();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not end with [ThatEnumerable.MyClass { Inner = <null>, Value = 3 }, ThatEnumerable.MyClass { Inner = <null>, Value = 5 }, ThatEnumerable.MyClass { Inner = <null>, Value = 8 }] equivalent,
					             but it did end with [
					               ThatEnumerable.MyClass {
					                 Inner = <null>,
					                 Value = 3
					               },
					               ThatEnumerable.MyClass {
					                 Inner = <null>,
					                 Value = 5
					               },
					               ThatEnumerable.MyClass {
					                 Inner = <null>,
					                 Value = 8
					               }
					             ]
					             """);
			}

			[Fact]
			public async Task WhenCollectionsAreIdentical_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).DoesNotEndWith(1, 2, 3);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not end with [1, 2, 3],
					             but it did end with [
					               1,
					               2,
					               3
					             ]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableHasDifferentEndingElements_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([0, 0, 1, 2, 3,]);
				IEnumerable<int> unexpected = [1, 3,];

				async Task Act()
					=> await That(subject).DoesNotEndWith(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectEndsWithUnexpectedValues_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["foo", "bar", "baz",]);
				IEnumerable<string> unexpected = ["bar", "baz",];

				async Task Act()
					=> await That(subject).DoesNotEndWith(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not end with unexpected,
					             but it did end with [
					               "bar",
					               "baz"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).DoesNotEndWith();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not end with [],
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenUnexpectedContainsAdditionalElements_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).DoesNotEndWith(0, 0, 1, 2, 3);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsEmpty_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2,]);

				async Task Act()
					=> await That(subject).DoesNotEndWith();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not end with [],
					             but it was [
					               1,
					               2
					             ]
					             """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1,]);

				async Task Act()
					=> await That(subject).DoesNotEndWith(null!);

				await That(Act).Throws<ArgumentNullException>()
					.WithParamName("unexpected");
			}
		}
	}
}
#endif
