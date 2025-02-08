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
				string expected = "INVESTIGATOR";

				async Task Act()
					=> await That(subject).DoesNotContain(expected).IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
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
					=> await That(subject).DoesNotContain(expected)
						.Using(new IgnoreCaseForVocalsComparer());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
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
					=> await That(subject).DoesNotContain(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
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
					=> await That(subject).DoesNotContain(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
