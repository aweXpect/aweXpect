#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed partial class Contains
	{
		public sealed class ItemTests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

				async Task Act()
					=> await That(subject).Contains(1)
						.And.Contains(1);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

				async Task Act()
					=> await That(subject).Contains(5);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(1, true)]
			[InlineData(2, true)]
			[InlineData(3, false)]
			public async Task ShouldSupportAtLeast(int minimum, bool expectSuccess)
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Contains(1).AtLeast(minimum.Times());

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected subject to
					              contain 1 at least {minimum} times,
					              but it contained it 2 times in [
					                1,
					                1,
					                2,
					                3,
					                5,
					                8,
					                13,
					                21,
					                34,
					                55,
					                …
					              ]
					              """);
			}

			[Theory]
			[InlineData(1, false)]
			[InlineData(2, true)]
			[InlineData(3, true)]
			public async Task ShouldSupportAtMost(int maximum, bool expectSuccess)
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Contains(1).AtMost(maximum.Times());

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage("""
					             Expected subject to
					             contain 1 at most once,
					             but it contained it at least 2 times in [
					               1,
					               1,
					               2,
					               3,
					               5,
					               8,
					               13,
					               21,
					               34,
					               55,
					               …
					             ]
					             """);
			}

			[Theory]
			[InlineData(1, 2, true)]
			[InlineData(2, 3, true)]
			[InlineData(3, 4, false)]
			public async Task ShouldSupportBetween(int minimum, int maximum, bool expectSuccess)
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Contains(1).Between(minimum).And(maximum.Times());

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected subject to
					              contain 1 between {minimum} and {maximum} times,
					              but it contained it 2 times in [
					                1,
					                1,
					                2,
					                3,
					                5,
					                8,
					                13,
					                21,
					                34,
					                55,
					                …
					              ]
					              """);
			}

#if DEBUG //TODO: Enable again after Core update
			[Fact]
			public async Task ShouldSupportEquivalent()
			{
				IAsyncEnumerable<MyClass> subject = Factory.GetAsyncFibonacciNumbers(x => new MyClass
				{
					Value = x
				}, 20);
				MyClass expected = new()
				{
					Value = 1
				};

				async Task Act()
					=> await That(subject).Contains(expected).AtLeast(1).Equivalent();

				await That(Act).DoesNotThrow();
			}
#endif

			[Theory]
			[InlineData(1, false)]
			[InlineData(2, true)]
			[InlineData(3, false)]
			public async Task ShouldSupportExactly(int times, bool expectSuccess)
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Contains(1).Exactly(times);

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected subject to
					              contain 1 exactly {(times == 1 ? "once" : $"{times} times")},
					              but it contained it {(times == 1 ? "at least " : "")}2 times in [
					                1,
					                1,
					                2,
					                3,
					                5,
					                8,
					                13,
					                21,
					                34,
					                55,
					                …
					              ]
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableContainsExpectedValue_ShouldSucceed(
				List<int> values, int expected)
			{
				values.Add(expected);
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(values.ToArray());

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableDoesNotContainsExpectedValue_ShouldFail(
				int[] values, int expected)
			{
				while (values.Contains(expected))
				{
					expected++;
				}

				IAsyncEnumerable<int> subject = ToAsyncEnumerable(values);

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              contain {Formatter.Format(expected)} at least once,
					              but it contained it 0 times in {Formatter.Format(values, FormattingOptions.MultipleLines)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				int expected = 42;
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain 42 at least once,
					             but it was <null>
					             """);
			}
		}

		public sealed class StringItemTests
		{
			[Fact]
			public async Task ShouldCompareCaseSensitive()
			{
				IAsyncEnumerable<string> sut = ToAsyncEnumerable(["green", "blue", "yellow"]);

				async Task Act()
					=> await That(sut).Contains("GREEN");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected sut to
					             contain "GREEN" at least once,
					             but it contained it 0 times in [
					               "green",
					               "blue",
					               "yellow"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenExpectedIsNotPartOfStringEnumerable_ShouldFail()
			{
				IAsyncEnumerable<string> sut = ToAsyncEnumerable(["green", "blue", "yellow"]);

				async Task Act()
					=> await That(sut).Contains("red");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected sut to
					             contain "red" at least once,
					             but it contained it 0 times in [
					               "green",
					               "blue",
					               "yellow"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenExpectedIsPartOfStringEnumerable_ShouldSucceed()
			{
				IAsyncEnumerable<string> sut = ToAsyncEnumerable(["green", "blue", "yellow"]);

				async Task Act()
					=> await That(sut).Contains("blue");

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenIgnoringCase_ShouldSucceedForCaseSensitiveDifference()
			{
				IAsyncEnumerable<string> sut = ToAsyncEnumerable(["green", "blue", "yellow"]);

				async Task Act()
					=> await That(sut).Contains("GREEN").IgnoringCase();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				string expected = "foo";
				IAsyncEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain "foo" at least once,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WithAtLeast_ShouldVerifyCorrectNumberOfTimes()
			{
				IAsyncEnumerable<string> sut = ToAsyncEnumerable(["green", "green", "blue", "yellow"]);

				async Task Act()
					=> await That(sut).Contains("green").AtLeast(3.Times());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected sut to
					             contain "green" at least 3 times,
					             but it contained it 2 times in [
					               "green",
					               "green",
					               "blue",
					               "yellow"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAtMost_ShouldVerifyCorrectNumberOfTimes()
			{
				IAsyncEnumerable<string>
					sut = ToAsyncEnumerable(["green", "green", "green", "green", "blue", "yellow"]);

				async Task Act()
					=> await That(sut).Contains("green").AtMost(2.Times());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected sut to
					             contain "green" at most 2 times,
					             but it contained it 4 times in [
					               "green",
					               "green",
					               "green",
					               "green",
					               "blue",
					               "yellow"
					             ]
					             """);
			}
		}

		public sealed class PredicateTests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

				async Task Act()
					=> await That(subject).Contains(_ => true)
						.And.Contains(_ => true);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

				async Task Act()
					=> await That(subject).Contains(x => x == 5);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(1, true)]
			[InlineData(2, true)]
			[InlineData(3, false)]
			public async Task ShouldSupportAtLeast(int minimum, bool expectSuccess)
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Contains(x => x == 1).AtLeast(minimum.Times());

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected subject to
					              contain item matching x => x == 1 at least {minimum} times,
					              but it contained it 2 times in [
					                1,
					                1,
					                2,
					                3,
					                5,
					                8,
					                13,
					                21,
					                34,
					                55,
					                …
					              ]
					              """);
			}

			[Theory]
			[InlineData(1, false)]
			[InlineData(2, true)]
			[InlineData(3, true)]
			public async Task ShouldSupportAtMost(int maximum, bool expectSuccess)
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Contains(x => x == 1).AtMost(maximum.Times());

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage("""
					             Expected subject to
					             contain item matching x => x == 1 at most once,
					             but it contained it at least 2 times in [
					               1,
					               1,
					               2,
					               3,
					               5,
					               8,
					               13,
					               21,
					               34,
					               55,
					               …
					             ]
					             """);
			}

			[Theory]
			[InlineData(1, 2, true)]
			[InlineData(2, 3, true)]
			[InlineData(3, 4, false)]
			public async Task ShouldSupportBetween(int minimum, int maximum, bool expectSuccess)
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Contains(x => x == 1).Between(minimum).And(maximum.Times());

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected subject to
					              contain item matching x => x == 1 between {minimum} and {maximum} times,
					              but it contained it 2 times in [
					                1,
					                1,
					                2,
					                3,
					                5,
					                8,
					                13,
					                21,
					                34,
					                55,
					                …
					              ]
					              """);
			}

			[Theory]
			[InlineData(1, false)]
			[InlineData(2, true)]
			[InlineData(3, false)]
			public async Task ShouldSupportExactly(int times, bool expectSuccess)
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Contains(x => x == 1).Exactly(times);

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected subject to
					              contain item matching x => x == 1 exactly {(times == 1 ? "once" : $"{times} times")},
					              but it contained it {(times == 1 ? "at least " : "")}2 times in [
					                1,
					                1,
					                2,
					                3,
					                5,
					                8,
					                13,
					                21,
					                34,
					                55,
					                …
					              ]
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableContainsExpectedValue_ShouldSucceed(
				List<int> values, int expected)
			{
				values.Add(expected);
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(values.ToArray());

				async Task Act()
					=> await That(subject).Contains(x => x == expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableDoesNotContainsExpectedValue_ShouldFail(
				int[] values, int expected)
			{
				while (values.Contains(expected))
				{
					expected++;
				}

				IAsyncEnumerable<int> subject = ToAsyncEnumerable(values.ToArray());

				async Task Act()
					=> await That(subject).Contains(x => x == expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              contain item matching x => x == expected at least once,
					              but it contained it 0 times in {Formatter.Format(values, FormattingOptions.MultipleLines)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).Contains(_ => true);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain item matching _ => true at least once,
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
