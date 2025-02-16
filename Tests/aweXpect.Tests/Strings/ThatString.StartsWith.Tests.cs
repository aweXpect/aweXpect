namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public class StartsWith
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(false)]
			[InlineData(true)]
			public async Task
				IgnoringCase_WhenSubjectStartsWithDifferentCase_ShouldFailUnlessCaseIsIgnored(
					bool ignoreCase)
			{
				string subject = "some arbitrary text";
				string expected = "SOME";

				async Task Act()
					=> await That(subject).StartsWith(expected)
						.IgnoringCase(ignoreCase);

				await That(Act).Throws<XunitException>()
					.OnlyIf(!ignoreCase)
					.WithMessage("""
					             Expected that subject
					             starts with "SOME",
					             but it was "some arbitrary text"
					             """);
			}

			[Fact]
			public async Task
				IgnoringCase_WhenSubjectStartsWithDifferentString_ShouldIncludeIgnoringCaseInMessage()
			{
				string subject = "some arbitrary text";
				string expected = "TEXT";

				async Task Act()
					=> await That(subject).StartsWith(expected).IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             starts with "TEXT" ignoring case,
					             but it was "some arbitrary text"
					             """);
			}

			[Fact]
			public async Task
				Using_WhenSubjectStartsWithIncorrectMatchAccordingToComparer_ShouldIncludeComparerInMessage()
			{
				string subject = "some arbitrary text";
				string expected = "SOME";

				async Task Act()
					=> await That(subject).StartsWith(expected)
						.Using(new IgnoreCaseForVocalsComparer());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             starts with "SOME" using IgnoreCaseForVocalsComparer,
					             but it was "some arbitrary text"
					             """);
			}

			[Fact]
			public async Task
				Using_WhenSubjectStartsWithMatchAccordingToComparer_ShouldSucceed()
			{
				string subject = "some arbitrary text";
				string expected = "sOmE";

				async Task Act()
					=> await That(subject).StartsWith(expected)
						.Using(new IgnoreCaseForVocalsComparer());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				string subject = "text";
				string? expected = null;

				async Task Act()
					=> await That(subject).StartsWith(expected!);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             starts with <null>,
					             but "text" cannot be validated against <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectDoesNotStartWithExpected_ShouldFail()
			{
				string subject = "some arbitrary text";
				string expected = "text";

				async Task Act()
					=> await That(subject).StartsWith(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             starts with "text",
					             but it was "some arbitrary text"
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsEqualToExpected_ShouldSucceed()
			{
				string subject = "some text";
				string expected = subject;

				async Task Act()
					=> await That(subject).StartsWith(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				string? subject = null;
				string expected = "text";

				async Task Act()
					=> await That(subject).StartsWith(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             starts with "text",
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsShorterThanExpected_ShouldFail()
			{
				string subject = "text";
				string expected = "text and more";

				async Task Act()
					=> await That(subject).StartsWith(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             starts with "text and more",
					             but it was "text" and with length 4 is shorter than the expected length of 13
					             """);
			}

			[Fact]
			public async Task WhenSubjectStartsWithExpected_ShouldSucceed()
			{
				string subject = "some arbitrary text";
				string expected = "some";

				async Task Act()
					=> await That(subject).StartsWith(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
