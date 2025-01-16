using System.Collections.Generic;
using System.Linq;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class EnumerableShould
{
	public sealed partial class Contain
	{
		public sealed class ItemTests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).Should().Contain(1)
						.And.Contain(1);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

				async Task Act()
					=> await That(subject).Should().Contain(5);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(1, true)]
			[InlineData(2, true)]
			[InlineData(3, false)]
			public async Task ShouldSupportAtLeast(int minimum, bool expectSuccess)
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Should().Contain(1).AtLeast(minimum.Times());

				await That(Act).Does().Throw<XunitException>().OnlyIf(!expectSuccess)
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
				IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Should().Contain(1).AtMost(maximum.Times());

				await That(Act).Does().Throw<XunitException>().OnlyIf(!expectSuccess)
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
				IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Should().Contain(1).Between(minimum).And(maximum.Times());

				await That(Act).Does().Throw<XunitException>().OnlyIf(!expectSuccess)
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

			[Fact]
			public async Task ShouldSupportEquivalent()
			{
				IEnumerable<MyClass> subject = Factory.GetFibonacciNumbers(20).Select(x => new MyClass
				{
					Value = x
				});
				MyClass expected = new()
				{
					Value = 1
				};

				async Task Act()
					=> await That(subject).Should().Contain(expected).AtLeast(1).Equivalent();

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(1, false)]
			[InlineData(2, true)]
			[InlineData(3, false)]
			public async Task ShouldSupportExactly(int times, bool expectSuccess)
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Should().Contain(1).Exactly(times);

				await That(Act).Does().Throw<XunitException>().OnlyIf(!expectSuccess)
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
				List<int> subject, int expected)
			{
				subject.Add(expected);

				async Task Act()
					=> await That(subject).Should().Contain(expected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableDoesNotContainsExpectedValue_ShouldFail(
				int[] subject, int expected)
			{
				while (subject.Contains(expected))
				{
					expected++;
				}

				async Task Act()
					=> await That(subject).Should().Contain(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              contain {Formatter.Format(expected)} at least once,
					              but it contained it 0 times in {Formatter.Format(subject, FormattingOptions.MultipleLines)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				int expected = 42;
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().Contain(expected);

				await That(Act).Does().Throw<XunitException>()
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
				string[] sut = ["green", "blue", "yellow"];

				async Task Act()
					=> await That(sut).Should().Contain("GREEN");

				await That(Act).Does().Throw<XunitException>()
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
				string[] sut = ["green", "blue", "yellow"];

				async Task Act()
					=> await That(sut).Should().Contain("red");

				await That(Act).Does().Throw<XunitException>()
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
				string[] sut = ["green", "blue", "yellow"];

				async Task Act()
					=> await That(sut).Should().Contain("blue");

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenIgnoringCase_ShouldSucceedForCaseSensitiveDifference()
			{
				string[] sut = ["green", "blue", "yellow"];

				async Task Act()
					=> await That(sut).Should().Contain("GREEN").IgnoringCase();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				string expected = "foo";
				IEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject!).Should().Contain(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain "foo" at least once,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WithAtLeast_ShouldVerifyCorrectNumberOfTimes()
			{
				string[] sut = ["green", "green", "blue", "yellow"];

				async Task Act()
					=> await That(sut).Should().Contain("green").AtLeast(3.Times());

				await That(Act).Does().Throw<XunitException>()
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
				string[] sut = ["green", "green", "green", "green", "blue", "yellow"];

				async Task Act()
					=> await That(sut).Should().Contain("green").AtMost(2.Times());

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected sut to
					             contain "green" at most 2 times,
					             but it contained it at least 3 times in [
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
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).Should().Contain(_ => true)
						.And.Contain(_ => true);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

				async Task Act()
					=> await That(subject).Should().Contain(x => x == 5);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(1, true)]
			[InlineData(2, true)]
			[InlineData(3, false)]
			public async Task ShouldSupportAtLeast(int minimum, bool expectSuccess)
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Should().Contain(x => x == 1).AtLeast(minimum.Times());

				await That(Act).Does().Throw<XunitException>().OnlyIf(!expectSuccess)
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
				IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Should().Contain(x => x == 1).AtMost(maximum.Times());

				await That(Act).Does().Throw<XunitException>().OnlyIf(!expectSuccess)
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
				IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Should().Contain(x => x == 1).Between(minimum).And(maximum.Times());

				await That(Act).Does().Throw<XunitException>().OnlyIf(!expectSuccess)
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
				IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).Should().Contain(x => x == 1).Exactly(times);

				await That(Act).Does().Throw<XunitException>().OnlyIf(!expectSuccess)
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
				List<int> subject, int expected)
			{
				subject.Add(expected);

				async Task Act()
					=> await That(subject).Should().Contain(x => x == expected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableDoesNotContainsExpectedValue_ShouldFail(
				int[] subject, int expected)
			{
				while (subject.Contains(expected))
				{
					expected++;
				}

				async Task Act()
					=> await That(subject).Should().Contain(x => x == expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              contain item matching x => x == expected at least once,
					              but it contained it 0 times in {Formatter.Format(subject, FormattingOptions.MultipleLines)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().Contain(_ => true);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain item matching _ => true at least once,
					             but it was <null>
					             """);
			}
		}
	}
}
