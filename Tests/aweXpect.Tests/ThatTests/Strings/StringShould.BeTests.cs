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
		[InlineData("a\rb", "a\r\nb", 2)]
		[InlineData("a\nb", "a\r\nb", 1)]
		[InlineData("a\r\nb", "a\rb", 2)]
		[InlineData("a\r\nb", "a\nb", 1)]
		[InlineData("a\rb", "a\nb", 1)]
		[InlineData("a\nb", "a\rb", 1)]
		public async Task WhenStringsDifferInNewlineStyle_ShouldFail(string subject, string expected,
			int indexOfFirstMismatch)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
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
		[InlineData("a\rb", "a\r\nb")]
		[InlineData("a\nb", "a\r\nb")]
		[InlineData("a\r\nb", "a\rb")]
		[InlineData("a\r\nb", "a\nb")]
		[InlineData("a\rb", "a\nb")]
		[InlineData("a\nb", "a\rb")]
		public async Task WhenStringsDifferInNewlineStyle_WithIgnoringNewlineStyle_ShouldSucceed(string subject, string expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringNewlineStyle();

			await That(Act).Should().NotThrow();
		}
	}
}
