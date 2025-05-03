namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public class Contains
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActualIsNull_ShouldFail()
			{
				string? subject = null;

				async Task Act()
					=> await That(subject).Contains("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains "foo" at least once,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenExpectedIsEmpty_ShouldFail()
			{
				string subject = "some text";
				string expected = "";

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Throws<ArgumentException>()
					.WithMessage("The 'expected' string cannot be empty.").AsPrefix().And
					.WithParamName("expected");
			}

			[Fact]
			public async Task WhenExpectedStringIsContained_ShouldSucceed()
			{
				string subject = "some text";
				string expected = "me";

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedStringIsNotContained_ShouldFail()
			{
				string subject = "some text";
				string expected = "not";

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains "not" at least once,
					             but it contained it 0 times in "some text"
					             """);
			}
		}

		public sealed class AtLeastTests
		{
			[Fact]
			public async Task WhenExpectedStringOccursEnoughTimes_ShouldSucceed()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).AtLeast(3);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedStringOccursFewerTimes_ShouldFail()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).AtLeast(5);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains "in" at least 5 times,
					             but it contained it 3 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             """);
			}

			[Fact]
			public async Task WhenExpectedStringOccursShouldFail()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "text that does not occur";

				async Task Act()
					=> await That(subject).Contains(expected).AtLeast(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains "text that does not occur" at least once,
					             but it contained it 0 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             """);
			}

			[Fact]
			public async Task WhenMinimumIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).AtLeast(-1);

				await That(Act).ThrowsExactly<ArgumentOutOfRangeException>()
					.WithMessage("*'minimum'*").AsWildcard().And
					.WithParamName("minimum");
			}
		}

		public sealed class AtMostTests
		{
			[Fact]
			public async Task WhenExpectedStringOccursMoreTimes_ShouldFail()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).AtMost(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains "in" at most 2 times,
					             but it contained it 3 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             """);
			}

			[Fact]
			public async Task WhenExpectedStringOccursShouldSucceed()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "text that does not occur";

				async Task Act()
					=> await That(subject).Contains(expected).AtMost(1);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedStringSufficientlyFewTimes_ShouldSucceed()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).AtMost(3);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMaximumIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).AtMost(-1);

				await That(Act).ThrowsExactly<ArgumentOutOfRangeException>()
					.WithMessage("*'maximum'*").AsWildcard().And
					.WithParamName("maximum");
			}
		}

		public sealed class BetweenTests
		{
			[Fact]
			public async Task WhenExpectedStringOccursFewerTimes_ShouldFail()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).Between(4).And(9);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains "in" between 4 and 9 times,
					             but it contained it 3 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             """);
			}

			[Fact]
			public async Task WhenExpectedStringOccursMoreTimes_ShouldFail()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).Between(1).And(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains "in" between 1 and 2 times,
					             but it contained it 3 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             """);
			}

			[Fact]
			public async Task WhenExpectedStringOccursSufficientTimes_ShouldSucceed()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).Between(1).And(4);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMaximumIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).Between(1).And(-3);

				await That(Act).ThrowsExactly<ArgumentOutOfRangeException>()
					.WithMessage("*'maximum'*").AsWildcard().And
					.WithParamName("maximum");
			}

			[Fact]
			public async Task WhenMinimumEqualsMaximum_ShouldBehaveLikeExactly()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).Between(3).And(3);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMinimumIsGreaterThanMaximum_ShouldThrowArgumentException()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).Between(4).And(3);

				await That(Act).ThrowsExactly<ArgumentException>()
					.WithMessage("*'maximum'*greater*'minimum'*").AsWildcard();
			}

			[Fact]
			public async Task WhenMinimumIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).Between(-1).And(3);

				await That(Act).ThrowsExactly<ArgumentOutOfRangeException>()
					.WithMessage("*'minimum'*").AsWildcard().And
					.WithParamName("minimum");
			}
		}

		public sealed class ExactlyTests
		{
			[Fact]
			public async Task
				WhenExpectedIsLessThanZero_ShouldThrowArgumentOutOfRangeException()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).Exactly(-1);

				await That(Act).ThrowsExactly<ArgumentOutOfRangeException>()
					.WithMessage("*'expected'*").AsWildcard().And
					.WithParamName("expected");
			}

			[Fact]
			public async Task WhenExpectedStringOccursCorrectlyOften_ShouldSucceed()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).Exactly(3);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedStringOccursFewerTimes_ShouldFail()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).Exactly(4);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains "in" exactly 4 times,
					             but it contained it 3 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             """);
			}

			[Fact]
			public async Task WhenExpectedStringOccursMoreTimes_ShouldFail()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).Exactly(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains "in" exactly 2 times,
					             but it contained it 3 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             """);
			}
		}

		public sealed class IgnoringCaseTests
		{
			[Fact]
			public async Task ShouldIncludeSettingInExpectationText()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).AtLeast(7).IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains "in" at least 7 times ignoring case,
					             but it contained it 5 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             """);
			}

			[Fact]
			public async Task
				WhenExpectedStringOccursEnoughTimesCaseInsensitive_ShouldSucceed()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).AtLeast(5).IgnoringCase();

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class NeverTests
		{
			[Fact]
			public async Task WhenExpectedStringDoesNotOccur_ShouldSucceed()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "detective";

				async Task Act()
					=> await That(subject).Contains(expected).Never();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedStringOccursMoreTimes_ShouldFail()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "investigator";

				async Task Act()
					=> await That(subject).Contains(expected).Never();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain "investigator",
					             but it contained it 1 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             """);
			}
		}

		public sealed class OnceTests
		{
			[Fact]
			public async Task WhenExpectedStringOccursExactly1Times_ShouldSucceed()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "investigator";

				async Task Act()
					=> await That(subject).Contains(expected).Once();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedStringOccursFewerTimes_ShouldFail()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "detective";

				async Task Act()
					=> await That(subject).Contains(expected).Once();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains "detective" exactly once,
					             but it contained it 0 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             """);
			}

			[Fact]
			public async Task WhenExpectedStringOccursMoreTimes_ShouldFail()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "word";

				async Task Act()
					=> await That(subject).Contains(expected).Once();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains "word" exactly once,
					             but it contained it 2 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             """);
			}
		}

		public sealed class UsingTests
		{
			[Fact]
			public async Task
				WhenExpectedStringOccursEnoughTimesForTheComparer_ShouldSucceed()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).Exactly(4)
						.Using(new IgnoreCaseForVocalsComparer());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task
				WhenExpectedStringOccursIncorrectTimesForTheComparer_ShouldIncludeComparerInMessage()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string expected = "in";

				async Task Act()
					=> await That(subject).Contains(expected).Exactly(5)
						.Using(new IgnoreCaseForVocalsComparer());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains "in" exactly 5 times using IgnoreCaseForVocalsComparer,
					             but it contained it 4 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             """);
			}
		}
	}
}
