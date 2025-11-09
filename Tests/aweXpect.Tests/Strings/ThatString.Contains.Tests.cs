namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public partial class Contains
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
			public async Task WhenExpectedIsEmpty_ShouldThrowArgumentException()
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
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				string subject = "some text";
				string? expected = null;

				async Task Act()
					=> await That(subject).Contains(expected!);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains <null> at least once,
					             but "some text" cannot be validated against <null>
					             """);
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
					             but it did not contain "not" in "some text"
					             
					             Actual:
					             some text
					             
					             Expected:
					             not
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
					             but it contained "in" 5 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             
					             Actual:
					             In this text in between the word an investigator should find the word 'IN' multiple times.
					             
					             Expected:
					             in
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
					             but it contained "in" 4 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             
					             Actual:
					             In this text in between the word an investigator should find the word 'IN' multiple times.
					             
					             Expected:
					             in
					             """);
			}
		}
	}
}
