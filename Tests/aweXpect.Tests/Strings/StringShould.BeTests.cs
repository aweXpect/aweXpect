namespace aweXpect.Tests.Strings;

public sealed partial class StringShould
{
	public sealed partial class Be
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData("some message", "*me me*", true)]
			[InlineData("some message", "*ME ME*", false)]
			[InlineData("some message", "some?message", true)]
			[InlineData("some message", "some*message", true)]
			[InlineData("some message", "some me?age", false)]
			[InlineData("some message", "some me??age", true)]
			public async Task AsWildcard_ShouldDefaultToCaseSensitiveMatch(
				string subject, string pattern, bool expectMatch)
			{
				async Task Act()
					=> await That(subject).Should().Be(pattern).AsWildcard();

				await That(Act).Should().ThrowException().OnlyIf(!expectMatch);
			}

			[Theory]
			[InlineAutoData(" foo", "foo")]
			[InlineAutoData("foo", " foo")]
			[InlineAutoData("\tfoo", "\nfoo")]
			[InlineAutoData("\r\nfoo", "foo")]
			[InlineAutoData("foo", "\tfoo")]
			public async Task IgnoringLeadingWhiteSpace_WhenStringsDifferOnlyInLeadingWhiteSpace_ShouldSucceed(
				string subject, string expected)
			{
				async Task Act()
					=> await That(subject).Should().Be(expected).IgnoringLeadingWhiteSpace();

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[InlineAutoData("foo ", "foo")]
			[InlineAutoData("foo", "foo ")]
			[InlineAutoData("foo\t", "foo\n")]
			[InlineAutoData("foo\r\n", "foo")]
			[InlineAutoData("foo", "foo\t")]
			public async Task IgnoringTrailingWhiteSpace_WhenStringsDifferOnlyInTrailingWhiteSpace_ShouldSucceed(
				string subject, string expected)
			{
				async Task Act()
					=> await That(subject).Should().Be(expected).IgnoringTrailingWhiteSpace();

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenStringsAreTheSame_ShouldSucceed(string subject)
			{
				string expected = subject;

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await Act();
			}

			[Fact]
			public async Task WhenStringsDiffer_ShouldFail()
			{
				string subject = "actual text";
				string expected = "expected other text";

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be equal to "expected other text",
					             but it was "actual text" which differs at index 0:
					                ↓ (actual)
					               "actual text"
					               "expected other text"
					                ↑ (expected)
					             """);
			}
		}
	}
}
