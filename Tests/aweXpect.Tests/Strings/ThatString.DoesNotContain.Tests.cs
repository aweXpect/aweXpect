namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public class DoesNotContain
	{
		public sealed class Tests
		{
			[Fact]
			public async Task IgnoringCase_ShouldIncludeSettingInExpectationText()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string unexpected = "INVESTIGATOR";

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected).IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain "INVESTIGATOR" ignoring case,
					             but it contained it 1 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             """);
			}

			[Fact]
			public async Task Using_ShouldIncludeComparerInExpectationText()
			{
				string subject =
					"In this text in between the word an investigator should find the word 'IN' multiple times.";
				string unexpected = "InvEstIgAtOr";

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected)
						.Using(new IgnoreCaseForVocalsComparer());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain "InvEstIgAtOr" using IgnoreCaseForVocalsComparer,
					             but it contained it 1 times in "In this text in between the word an investigator should find the word 'IN' multiple times."
					             """);
			}

			[Fact]
			public async Task WhenUnexpectedIsEmpty_ShouldThrowArgumentException()
			{
				string subject = "some text";
				string unexpected = "";

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected);

				await That(Act).Throws<ArgumentException>()
					.WithMessage("The 'unexpected' string cannot be empty.").AsPrefix().And
					.WithParamName("unexpected");
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldFail()
			{
				string subject = "some text";
				string? unexpected = null;

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected!);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain <null>,
					             but "some text" cannot be validated against <null>
					             """);
			}

			[Fact]
			public async Task WhenUnexpectedStringIsContained_ShouldFail()
			{
				string subject = "some text";
				string unexpected = "me";

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain "me",
					             but it contained it 1 times in "some text"
					             """);
			}

			[Fact]
			public async Task WhenUnexpectedStringIsNotContained_ShouldSucceed()
			{
				string subject = "some text";
				string unexpected = "not";

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
