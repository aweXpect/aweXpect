#if NET6_0_OR_GREATER
using System.Collections.Generic;
using System.Linq;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class AsyncEnumerableShould
{
	public sealed class ContainTests
	{
		[Fact]
		public async Task ForStrings_ShouldCompareCaseSensitive()
		{
			IAsyncEnumerable<string> sut = ToAsyncEnumerable(["green", "blue", "yellow"]);

			async Task Act()
				=> await That(sut).Should().Contain("GREEN");

			await That(Act).Should().Throw<XunitException>()
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
		public async Task ForStrings_WhenExpectedIsNotPartOfStringEnumerable_ShouldFail()
		{
			IAsyncEnumerable<string> sut = ToAsyncEnumerable(["green", "blue", "yellow"]);

			async Task Act()
				=> await That(sut).Should().Contain("red");

			await That(Act).Should().Throw<XunitException>()
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
		public async Task ForStrings_WhenExpectedIsPartOfStringEnumerable_ShouldSucceed()
		{
			IAsyncEnumerable<string> sut = ToAsyncEnumerable(["green", "blue", "yellow"]);

			async Task Act()
				=> await That(sut).Should().Contain("blue");

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForStrings_WhenIgnoringCase_ShouldSucceedForCaseSensitiveDifference()
		{
			IAsyncEnumerable<string> sut = ToAsyncEnumerable(["green", "blue", "yellow"]);

			async Task Act()
				=> await That(sut).Should().Contain("GREEN").IgnoringCase();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForStrings_WithAtLeast_ShouldVerifyCorrectNumberOfTimes()
		{
			IAsyncEnumerable<string> sut = ToAsyncEnumerable(["green", "green", "blue", "yellow"]);

			async Task Act()
				=> await That(sut).Should().Contain("green").AtLeast(3.Times());

			await That(Act).Should().Throw<XunitException>()
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
		public async Task ForStrings_WithAtMost_ShouldVerifyCorrectNumberOfTimes()
		{
			IAsyncEnumerable<string> sut = ToAsyncEnumerable(["green", "green", "green", "green", "blue", "yellow"]);

			async Task Act()
				=> await That(sut).Should().Contain("green").AtMost(2.Times());

			await That(Act).Should().Throw<XunitException>()
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

		[Fact]
		public async Task Item_DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().Contain(1)
					.And.Contain(1);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Item_DoesNotMaterializeEnumerable()
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().Contain(5);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1, true)]
		[InlineData(2, true)]
		[InlineData(3, false)]
		public async Task Item_ShouldSupportAtLeast(int minimum, bool expectSuccess)
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(1).AtLeast(minimum.Times());

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
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
		public async Task Item_ShouldSupportAtMost(int maximum, bool expectSuccess)
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(1).AtMost(maximum.Times());

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
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
		public async Task Item_ShouldSupportBetween(int minimum, int maximum, bool expectSuccess)
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(1).Between(minimum).And(maximum.Times());

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
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
		public async Task Item_ShouldSupportEquivalent()
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
				=> await That(subject).Should().Contain(expected).AtLeast(1).Equivalent();

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1, false)]
		[InlineData(2, true)]
		[InlineData(3, false)]
		public async Task Item_ShouldSupportExactly(int times, bool expectSuccess)
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(1).Exactly(times);

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
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
		public async Task Item_WhenEnumerableContainsExpectedValue_ShouldSucceed(
			List<int> values, int expected)
		{
			values.Add(expected);
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values.ToArray());

			async Task Act()
				=> await That(subject).Should().Contain(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task Item_WhenEnumerableDoesNotContainsExpectedValue_ShouldFail(
			int[] values, int expected)
		{
			while (values.Contains(expected))
			{
				expected++;
			}

			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values);

			async Task Act()
				=> await That(subject).Should().Contain(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              contain {Formatter.Format(expected)} at least once,
				              but it contained it 0 times in {Formatter.Format(values, FormattingOptions.MultipleLines)}
				              """);
		}

		[Fact]
		public async Task Predicate_DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().Contain(_ => true)
					.And.Contain(_ => true);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Predicate_DoesNotMaterializeEnumerable()
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().Contain(x => x == 5);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1, true)]
		[InlineData(2, true)]
		[InlineData(3, false)]
		public async Task Predicate_ShouldSupportAtLeast(int minimum, bool expectSuccess)
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(x => x == 1).AtLeast(minimum.Times());

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
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
		public async Task Predicate_ShouldSupportAtMost(int maximum, bool expectSuccess)
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(x => x == 1).AtMost(maximum.Times());

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
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
		public async Task Predicate_ShouldSupportBetween(int minimum, int maximum, bool expectSuccess)
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(x => x == 1).Between(minimum).And(maximum.Times());

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
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
		public async Task Predicate_ShouldSupportExactly(int times, bool expectSuccess)
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(x => x == 1).Exactly(times);

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
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
		public async Task Predicate_WhenEnumerableContainsExpectedValue_ShouldSucceed(
			List<int> values, int expected)
		{
			values.Add(expected);
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values.ToArray());

			async Task Act()
				=> await That(subject).Should().Contain(x => x == expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task Predicate_WhenEnumerableDoesNotContainsExpectedValue_ShouldFail(
			int[] values, int expected)
		{
			while (values.Contains(expected))
			{
				expected++;
			}

			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values.ToArray());

			async Task Act()
				=> await That(subject).Should().Contain(x => x == expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              contain item matching x => x == expected at least once,
				              but it contained it 0 times in {Formatter.Format(values, FormattingOptions.MultipleLines)}
				              """);
		}
	}

	public sealed class NotContainTests
	{
		[Fact]
		public async Task ForStrings_WhenStringDiffersInCase_ShouldSucceed()
		{
			string[] values = ["a", "b", "c"];
			string unexpected = "A";

			IAsyncEnumerable<string> subject = ToAsyncEnumerable(values);

			async Task Act()
				=> await That(subject).Should().NotContain(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForStrings_WhenStringDiffersInCase_WhileIgnoringCase_ShouldFail()
		{
			string[] values = ["a", "b", "c"];
			string unexpected = "A";

			IAsyncEnumerable<string> subject = ToAsyncEnumerable(values);

			async Task Act()
				=> await That(subject).Should().NotContain(unexpected).IgnoringCase();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not contain "A" ignoring case,
				             but it did
				             """);
		}

		[Fact]
		public async Task Item_DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().NotContain(0)
					.And.NotContain(0);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Item_DoesNotMaterializeEnumerable()
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().NotContain(5);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not contain 5,
				             but it did
				             """);
		}

		[Fact]
		public async Task Item_ShouldSupportEquivalent()
		{
			IAsyncEnumerable<MyClass> subject = Factory.GetAsyncFibonacciNumbers(x => new MyClass(x), 20);
			MyClass unexpected = new()
			{
				Value = 5
			};

			async Task Act()
				=> await That(subject).Should().NotContain(unexpected).Equivalent();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not contain MyClass {
				               Value = 5
				             } using EquivalencyComparer,
				             but it did
				             """);
		}

		[Theory]
		[AutoData]
		public async Task Item_WhenEnumerableContainsUnexpectedValue_ShouldFail(
			List<int> values, int unexpected)
		{
			values.Add(unexpected);
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values.ToArray());

			async Task Act()
				=> await That(subject).Should().NotContain(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not contain {Formatter.Format(unexpected)},
				              but it did
				              """);
		}

		[Theory]
		[AutoData]
		public async Task Item_WhenEnumerableDoesNotContainsUnexpectedValue_ShouldSucceed(
			int[] values, int unexpected)
		{
			while (values.Contains(unexpected))
			{
				unexpected++;
			}

			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values);

			async Task Act()
				=> await That(subject).Should().NotContain(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Predicate_DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().NotContain(x => x == 0)
					.And.NotContain(x => x == 0);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Predicate_DoesNotMaterializeEnumerable()
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().NotContain(x => x == 5);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not contain item matching x => x == 5,
				             but it did
				             """);
		}

		[Theory]
		[AutoData]
		public async Task Predicate_WhenEnumerableContainsUnexpectedValue_ShouldFail(
			List<int> values, int unexpected)
		{
			values.Add(unexpected);
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values.ToArray());

			async Task Act()
				=> await That(subject).Should().NotContain(x => x == unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not contain item matching x => x == unexpected,
				             but it did
				             """);
		}

		[Theory]
		[AutoData]
		public async Task Predicate_WhenEnumerableDoesNotContainsUnexpectedValue_ShouldSucceed(
			int[] values, int unexpected)
		{
			while (values.Contains(unexpected))
			{
				unexpected++;
			}

			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values);

			async Task Act()
				=> await That(subject).Should().NotContain(x => x == unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
#endif
