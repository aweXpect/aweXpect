namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public sealed partial class IsEqualTo
	{
		public sealed class AsRegexTests
		{
			[Theory(Skip="Temporarily disable until next Core update")]
			[InlineData("some message", ".*me me.*", true)]
			[InlineData("some message", ".*ME ME.*", false)]
			public async Task ShouldDefaultToCaseSensitiveMatch(
				string subject, string pattern, bool expectMatch)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(pattern).AsRegex();

				await That(Act).ThrowsException().OnlyIf(!expectMatch)
					.WithMessage($"""
					              Expected subject to
					              match regex {Formatter.Format(pattern)},
					              but it did not match:
					                ↓ (actual)
					                {Formatter.Format(subject)}
					                {Formatter.Format(pattern)}
					                ↑ (regex pattern)
					              """);
			}

			[Theory(Skip="Temporarily disable until next Core update")]
			[InlineData(true)]
			[InlineData(false)]
			public async Task WhenIgnoringCase_ShouldIgnoreCase(
				bool ignoreCase)
			{
				string subject = "some message";
				string pattern = ".*ME ME.*";

				async Task Act()
					=> await That(subject).IsEqualTo(pattern)
						.AsRegex().IgnoringCase(ignoreCase);

				await That(Act).ThrowsException().OnlyIf(!ignoreCase)
					.WithMessage("""
					             Expected subject to
					             match regex ".*ME ME.*",
					             but it did not match:
					               ↓ (actual)
					               "some message"
					               ".*ME ME.*"
					               ↑ (regex pattern)
					             """);
			}
		}
	}
}
