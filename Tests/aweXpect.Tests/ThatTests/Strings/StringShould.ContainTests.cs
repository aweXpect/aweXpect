using Testably.Expectations.Tests.TestHelpers;

namespace Testably.Expectations.Tests.ThatTests.Strings;

public sealed partial class StringShould
{
	public class ContainTests
	{
		[Fact]
		public async Task AtLeast_WhenExpectedStringOccursEnoughTimes_ShouldSucceed()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).AtLeast(3);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AtLeast_WhenExpectedStringOccursFewerTimes_ShouldFail()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).AtLeast(5);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             contain "in" at least 5 times,
				             but it contained it 3 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
				             """);
		}

		[Fact]
		public async Task AtLeast_WhenExpectedStringOccursNever_ShouldFail()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "text that does not occur";

			async Task Act()
				=> await That(subject).Should().Contain(expected).AtLeast(1);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             contain "text that does not occur" at least once,
				             but it contained it 0 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
				             """);
		}

		[Fact]
		public async Task AtLeast_WhenMinimumIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).AtLeast(-1);

			await That(Act).Should().ThrowExactly<ArgumentOutOfRangeException>()
				.WithParamName("minimum").And
				.WithMessage("*'minimum'*").AsWildcard();
		}

		[Fact]
		public async Task AtMost_WhenExpectedStringOccursMoreTimes_ShouldFail()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).AtMost(2);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             contain "in" at most 2 times,
				             but it contained it 3 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
				             """);
		}

		[Fact]
		public async Task AtMost_WhenExpectedStringOccursNever_ShouldSucceed()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "text that does not occur";

			async Task Act()
				=> await That(subject).Should().Contain(expected).AtMost(1);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AtMost_WhenExpectedStringSufficientlyFewTimes_ShouldSucceed()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).AtMost(3);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AtMost_WhenMaximumIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).AtMost(-1);

			await That(Act).Should().ThrowExactly<ArgumentOutOfRangeException>()
				.WithMessage("*'maximum'*").AsWildcard().And
				.WithParamName("maximum");
		}

		[Fact]
		public async Task Between_WhenExpectedStringOccursFewerTimes_ShouldFail()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Between(4).And(9);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             contain "in" between 4 and 9 times,
				             but it contained it 3 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
				             """);
		}

		[Fact]
		public async Task Between_WhenExpectedStringOccursMoreTimes_ShouldFail()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Between(1).And(2);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             contain "in" between 1 and 2 times,
				             but it contained it 3 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
				             """);
		}

		[Fact]
		public async Task Between_WhenExpectedStringOccursSufficientTimes_ShouldSucceed()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Between(1).And(4);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Between_WhenMaximumIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Between(1).And(-3);

			await That(Act).Should().ThrowExactly<ArgumentOutOfRangeException>()
				.WithMessage("*'maximum'*").AsWildcard().And
				.WithParamName("maximum");
		}

		[Fact]
		public async Task Between_WhenMinimumEqualsMaximum_ShouldBehaveLikeExactly()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Between(3).And(3);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Between_WhenMinimumIsGreaterThanMaximum_ShouldThrowArgumentException()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Between(4).And(3);

			await That(Act).Should().ThrowExactly<ArgumentException>()
				.WithMessage("*'maximum'*greater*'minimum'*").AsWildcard();
		}

		[Fact]
		public async Task Between_WhenMinimumIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Between(-1).And(3);

			await That(Act).Should().ThrowExactly<ArgumentOutOfRangeException>()
				.WithMessage("*'minimum'*").AsWildcard().And
				.WithParamName("minimum");
		}

		[Fact]
		public async Task
			Exactly_WhenExpectedIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Exactly(-1);

			await That(Act).Should().ThrowExactly<ArgumentOutOfRangeException>()
				.WithMessage("*'expected'*").AsWildcard().And
				.WithParamName("expected");
		}

		[Fact]
		public async Task Exactly_WhenExpectedStringOccursCorrectlyOften_ShouldSucceed()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Exactly(3);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Exactly_WhenExpectedStringOccursFewerTimes_ShouldFail()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Exactly(4);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             contain "in" exactly 4 times,
				             but it contained it 3 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
				             """);
		}

		[Fact]
		public async Task Exactly_WhenExpectedStringOccursMoreTimes_ShouldFail()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Exactly(2);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             contain "in" exactly 2 times,
				             but it contained it 3 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
				             """);
		}

		[Fact]
		public async Task IgnoringCase_ShouldIncludeSettingInExpectationText()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).AtLeast(7).IgnoringCase();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             contain "in" at least 7 times ignoring case,
				             but it contained it 5 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
				             """);
		}

		[Fact]
		public async Task
			IgnoringCase_WhenExpectedStringOccursEnoughTimesCaseInsensitive_ShouldSucceed()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).AtLeast(5).IgnoringCase();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Never_WhenExpectedStringDoesNotOccur_ShouldSucceed()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "detective";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Never();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Never_WhenExpectedStringOccursMoreTimes_ShouldFail()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "investigator";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Never();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not contain "investigator",
				             but it contained it 1 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
				             """);
		}

		[Fact]
		public async Task Once_WhenExpectedStringOccursExactly1Times_ShouldSucceed()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "investigator";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Once();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Once_WhenExpectedStringOccursFewerTimes_ShouldFail()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "detective";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Once();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             contain "detective" exactly once,
				             but it contained it 0 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
				             """);
		}

		[Fact]
		public async Task Once_WhenExpectedStringOccursMoreTimes_ShouldFail()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "word";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Once();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             contain "word" exactly once,
				             but it contained it 2 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
				             """);
		}

		[Fact]
		public async Task
			Using_WhenExpectedStringOccursEnoughTimesForTheComparer_ShouldSucceed()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Exactly(4)
					.Using(new IgnoreCaseForVocalsComparer());

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task
			Using_WhenExpectedStringOccursIncorrectTimesForTheComparer_ShouldIncludeComparerInMessage()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "in";

			async Task Act()
				=> await That(subject).Should().Contain(expected).Exactly(5)
					.Using(new IgnoreCaseForVocalsComparer());

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             contain "in" exactly 5 times using IgnoreCaseForVocalsComparer,
				             but it contained it 4 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
				             """);
		}

		[Fact]
		public async Task WhenExpectedStringIsContained_ShouldSucceed()
		{
			string subject = "some text";
			string expected = "me";

			async Task Act()
				=> await That(subject).Should().Contain(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenExpectedStringIsNotContained_ShouldFail()
		{
			string subject = "some text";
			string expected = "not";

			async Task Act()
				=> await That(subject).Should().Contain(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             contain "not" at least once,
				             but it contained it 0 times in "some text"
				             """);
		}
	}

	public class NotContainTests
	{
		[Fact]
		public async Task IgnoringCase_ShouldIncludeSettingInExpectationText()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "INVESTIGATOR";

			async Task Act()
				=> await That(subject).Should().NotContain(expected).IgnoringCase();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not contain "INVESTIGATOR" ignoring case,
				             but it contained it 1 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
				             """);
		}

		[Fact]
		public async Task Using_ShouldIncludeComparerInExpectationText()
		{
			string subject =
				"In this text in between the word an investigator should find the word 'IN' multiple times.";
			string expected = "InvEstIgAtOr";

			async Task Act()
				=> await That(subject).Should().NotContain(expected)
					.Using(new IgnoreCaseForVocalsComparer());

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not contain "InvEstIgAtOr" using IgnoreCaseForVocalsComparer,
				             but it contained it 1 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
				             """);
		}

		[Fact]
		public async Task WhenExpectedStringIsContained_ShouldFail()
		{
			string subject = "some text";
			string expected = "me";

			async Task Act()
				=> await That(subject).Should().NotContain(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not contain "me",
				             but it contained it 1 times in "some text"
				             """);
		}

		[Fact]
		public async Task WhenExpectedStringIsNotContained_ShouldSucceed()
		{
			string subject = "some text";
			string expected = "not";

			async Task Act()
				=> await That(subject).Should().NotContain(expected);

			await That(Act).Should().NotThrow();
		}
	}
}
