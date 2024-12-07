namespace aweXpect.Tests.ThatTests.Strings;

public sealed partial class StringShould
{
	public class BeTests
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

		[Theory]
		[InlineAutoData(" foo", "foo", true)]
		[InlineAutoData(" foo", "foo", false)]
		[InlineAutoData("foo", " foo", true)]
		[InlineAutoData("\tfoo", "\nfoo", false)]
		[InlineAutoData("\r\nfoo", "foo", true)]
		[InlineAutoData("foo", "\tfoo", false)]
		public async Task WhenStringsDifferInLeadingWhiteSpace_ShouldFailIfNotIgnoringLeadingWhiteSpace(
			string subject, string expected, bool ignoreLeadingWhiteSpace)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringLeadingWhiteSpace(ignoreLeadingWhiteSpace);

			await That(Act).Should().Throw<XunitException>().OnlyIf(!ignoreLeadingWhiteSpace)
				.WithMessage($"""
				              Expected subject to
				              be equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)} which has unexpected whitespace ("*" at the beginning)
				              """).AsWildcard();
		}

		[Theory]
		[InlineAutoData("a\rb", "a\r\nb", 2, true)]
		[InlineAutoData("a\rb", "a\r\nb", 2, false)]
		[InlineAutoData("a\nb", "a\r\nb", 1)]
		[InlineAutoData("a\r\nb", "a\rb", 2)]
		[InlineAutoData("a\r\nb", "a\nb", 1)]
		[InlineAutoData("a\rb", "a\nb", 1)]
		[InlineAutoData("a\nb", "a\rb", 1)]
		public async Task WhenStringsDifferInNewlineStyle_ShouldFailIfNotIgnoringNewlineStyle(
			string subject, string expected, int indexOfFirstMismatch, bool ignoreNewlineStyle)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringNewlineStyle(ignoreNewlineStyle);

			await That(Act).Should().Throw<XunitException>().OnlyIf(!ignoreNewlineStyle)
				.WithMessage($"""
				              Expected subject to
				              be equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)} which differs at index {indexOfFirstMismatch}:
				                {new string(' ', 2 * indexOfFirstMismatch)}↓ (actual)
				                {Formatter.Format(subject)}
				                {Formatter.Format(expected)}
				                {new string(' ', 2 * indexOfFirstMismatch)}↑ (expected)
				              """);
		}

		[Theory]
		[InlineAutoData("foo ", "foo", true)]
		[InlineAutoData("foo ", "foo", false)]
		[InlineAutoData("foo", "foo ", true)]
		[InlineAutoData("foo\t", "foo\n", false)]
		[InlineAutoData("foo\r\n", "foo", true)]
		[InlineAutoData("foo", "foo\t", false)]
		public async Task WhenStringsDifferInTrailingWhiteSpace_ShouldFailIfNotIgnoringTrailingWhiteSpace(
			string subject, string expected, bool ignoreTrailingWhiteSpace)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringTrailingWhiteSpace(ignoreTrailingWhiteSpace);

			await That(Act).Should().Throw<XunitException>().OnlyIf(!ignoreTrailingWhiteSpace)
				.WithMessage($"""
				              Expected subject to
				              be equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)} which has unexpected whitespace ("*" at the end)
				              """).AsWildcard();
		}
	}
}
