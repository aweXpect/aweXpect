﻿namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public sealed partial class Is
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
					=> await That(subject).Is(pattern).AsWildcard();

				await That(Act).ThrowsException().OnlyIf(!expectMatch);
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
					=> await That(subject).Is(expected).IgnoringLeadingWhiteSpace();

				await That(Act).DoesNotThrow();
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
					=> await That(subject).Is(expected).IgnoringTrailingWhiteSpace();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenStringsAreTheSame_ShouldSucceed(string subject)
			{
				string expected = subject;

				async Task Act()
					=> await That(subject).Is(expected);

				await Act();
			}

			[Fact]
			public async Task WhenStringsDiffer_ShouldFail()
			{
				string subject = "actual text";
				string expected = "expected other text";

				async Task Act()
					=> await That(subject).Is(expected);

				await That(Act).Throws<XunitException>()
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
