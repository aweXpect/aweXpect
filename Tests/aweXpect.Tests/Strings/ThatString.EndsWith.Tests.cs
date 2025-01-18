namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public class EndsWith
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(false)]
			[InlineData(true)]
			public async Task
				IgnoringCase_WhenSubjectEndsWithDifferentCase_ShouldFailUnlessCaseIsIgnored(
					bool ignoreCase)
			{
				string subject = "some arbitrary text";
				string expected = "TEXT";

				async Task Act()
					=> await That(subject).EndsWith(expected).IgnoringCase(ignoreCase);

				await That(Act).Throws<XunitException>()
					.OnlyIf(!ignoreCase)
					.WithMessage("""
					             Expected subject to
					             end with "TEXT",
					             but it was "some arbitrary text"
					             """);
			}

			[Fact]
			public async Task
				IgnoringCase_WhenSubjectEndsWithDifferentString_ShouldIncludeIgnoringCaseInMessage()
			{
				string subject = "some arbitrary text";
				string expected = "SOME";

				async Task Act()
					=> await That(subject).EndsWith(expected).IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             end with "SOME" ignoring case,
					             but it was "some arbitrary text"
					             """);
			}

			[Fact]
			public async Task
				Using_WhenSubjectEndsWithIncorrectMatchAccordingToComparer_ShouldIncludeComparerInMessage()
			{
				string subject = "some arbitrary text";
				string expected = "TEXT";

				async Task Act()
					=> await That(subject).EndsWith(expected)
						.Using(new IgnoreCaseForVocalsComparer());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             end with "TEXT" using IgnoreCaseForVocalsComparer,
					             but it was "some arbitrary text"
					             """);
			}

			[Fact]
			public async Task
				Using_WhenSubjectEndsWithMatchAccordingToComparer_ShouldSucceed()
			{
				string subject = "some arbitrary text";
				string expected = "tExt";

				async Task Act()
					=> await That(subject).EndsWith(expected)
						.Using(new IgnoreCaseForVocalsComparer());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectDoesNotEndWithExpected_ShouldFail()
			{
				string subject = "some arbitrary text";
				string expected = "some";

				async Task Act()
					=> await That(subject).EndsWith(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             end with "some",
					             but it was "some arbitrary text"
					             """);
			}

			[Fact]
			public async Task WhenSubjectEndsWithExpected_ShouldSucceed()
			{
				string subject = "some arbitrary text";
				string expected = "text";

				async Task Act()
					=> await That(subject).EndsWith(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
