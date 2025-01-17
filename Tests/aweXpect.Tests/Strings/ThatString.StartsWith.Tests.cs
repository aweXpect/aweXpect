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

				await That(Act).Does().Throw<XunitException>()
					.OnlyIf(!ignoreCase)
					.WithMessage("""
					             Expected subject to
					             start with "SOME",
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

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             start with "TEXT" ignoring case,
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

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             start with "SOME" using IgnoreCaseForVocalsComparer,
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

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectDoesNotStartWithExpected_ShouldFail()
			{
				string subject = "some arbitrary text";
				string expected = "text";

				async Task Act()
					=> await That(subject).StartsWith(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             start with "text",
					             but it was "some arbitrary text"
					             """);
			}

			[Fact]
			public async Task WhenSubjectStartsWithExpected_ShouldSucceed()
			{
				string subject = "some arbitrary text";
				string expected = "some";

				async Task Act()
					=> await That(subject).StartsWith(expected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
