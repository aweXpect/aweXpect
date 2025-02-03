namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public sealed partial class IsEqualTo
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task WhenStringsAreTheSame_ShouldSucceed(string subject)
			{
				string expected = subject;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await Act();
			}

			[Fact]
			public async Task WhenStringsDiffer_ShouldFail()
			{
				string subject = "actual text";
				string expected = "expected other text";

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

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

		public sealed class IgnoringLeadingWhiteSpaceTests
		{
			[Theory(Skip="Temporarily disable until core update")]
			[InlineAutoData("foo", " bar", 0)]
			[InlineAutoData(" foo", "bar", 1)]
			[InlineAutoData(" \tfoo", "bar", 2)]
			public async Task ShouldIncludeCorrectIndexInMessage(
				string subject, string expected, int index)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringLeadingWhiteSpace();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be equal to {Formatter.Format(expected)} ignoring leading white-space,
					              but it was {Formatter.Format(subject.TrimStart())} which differs at index {index}:
					                 ↓ (actual)
					                "foo"
					                "bar"
					                 ↑ (expected)
					              """);
			}

			[Theory(Skip="Temporarily disable until core update")]
			[InlineAutoData("foo\nbar", " \n bar", 1, 1)]
			[InlineAutoData(" \n\n foo", "bar", 3, 2)]
			[InlineAutoData(" \r\n \tfoo", "bar", 2, 3)]
			public async Task ShouldIncludeCorrectLineAndColumnInMessage(
				string subject, string expected, int line, int column)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringLeadingWhiteSpace();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be equal to {Formatter.Format(expected)} ignoring leading white-space,
					              but it was {Formatter.Format(subject.TrimStart())} which differs on line {line} and column {column}:
					                 ↓ (actual)
					                {Formatter.Format(subject.TrimStart())}
					                {Formatter.Format(expected.TrimStart())}
					                 ↑ (expected)
					              """);
			}

			[Theory]
			[InlineAutoData(" foo", "foo")]
			[InlineAutoData("foo", " foo")]
			[InlineAutoData("\tfoo", "\nfoo")]
			[InlineAutoData("\r\nfoo", "foo")]
			[InlineAutoData("foo", "\tfoo")]
			public async Task WhenStringsDifferOnlyInLeadingWhiteSpace_ShouldSucceed(
				string subject, string expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringLeadingWhiteSpace();

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class IgnoringNewlineStyleTests
		{
			[Theory]
			[InlineAutoData("foo\nbar", "foo\rbar")]
			[InlineAutoData("foo\rbar", "foo\nbar")]
			[InlineAutoData("foo\nbar", "foo\r\nbar")]
			[InlineAutoData("foo\rbar", "foo\r\nbar")]
			[InlineAutoData("foo\r\nbar", "foo\nbar")]
			[InlineAutoData("foo\r\nbar", "foo\rbar")]
			public async Task WhenStringsDifferOnlyInNewlineStyle_ShouldSucceed(
				string subject, string expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringNewlineStyle();

				await That(Act).DoesNotThrow();
			}

			[Fact(Skip="Temporarily disable until core update")]
			public async Task ShouldIncludeCorrectIndexInMessage()
			{
				string subject = "foo\nbar";
				string expected = "foo\r\nbaz";

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringNewlineStyle();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be equal to "foo\r\nbaz" ignoring newline style,
					             but it was "foo\nbar" which differs on line 2 and column 3:
					                       ↓ (actual)
					               "foo\nbar"
					               "foo\nbaz"
					                       ↑ (expected)
					             """);
			}
		}

		public sealed class IgnoringTrailingWhiteSpaceTests
		{
			[Fact(Skip="Temporarily disable until core update")]
			public async Task ShouldConsiderNewlineForSwitchingToLineColumn()
			{
				string subject = "foo-boo\nbaz\t";
				string expected = "foo-bar";

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringTrailingWhiteSpace();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be equal to "foo-bar" ignoring trailing white-space,
					             but it was "foo-boo\nbaz" which differs on line 1 and column 6:
					                     ↓ (actual)
					               "foo-boo\nbaz"
					               "foo-bar"
					                     ↑ (expected)
					             """);
			}

			[Fact(Skip="Temporarily disable until core update")]
			public async Task ShouldIncludeCorrectIndexInMessage()
			{
				string subject = "foo-boo\t";
				string expected = "foo-bar";

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringTrailingWhiteSpace();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be equal to "foo-bar" ignoring trailing white-space,
					             but it was "foo-boo" which differs at index 5:
					                     ↓ (actual)
					               "foo-boo"
					               "foo-bar"
					                     ↑ (expected)
					             """);
			}

			[Theory]
			[InlineAutoData("foo ", "foo")]
			[InlineAutoData("foo", "foo ")]
			[InlineAutoData("foo\t", "foo\n")]
			[InlineAutoData("foo\r\n", "foo")]
			[InlineAutoData("foo", "foo\t")]
			public async Task WhenStringsDifferOnlyInTrailingWhiteSpace_ShouldSucceed(
				string subject, string expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringTrailingWhiteSpace();

				await That(Act).DoesNotThrow();
			}
		}
	}
}
