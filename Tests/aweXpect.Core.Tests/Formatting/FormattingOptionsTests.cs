namespace aweXpect.Core.Tests.Formatting;

public class FormattingOptionsTests
{
	[Fact]
	public async Task Indented_ShouldUseLineBreaks()
	{
		FormattingOptions options = FormattingOptions.Indented;

		await That(options.UseLineBreaks).IsTrue();
		await That(options.Indentation).IsEqualTo("  ");
	}

	[Fact]
	public async Task MultipleLines_ShouldUseLineBreaks()
	{
		FormattingOptions options = FormattingOptions.MultipleLines;

		await That(options.UseLineBreaks).IsTrue();
		await That(options.Indentation).IsEmpty();
	}

	[Fact]
	public async Task SingleLine_ShouldNotUseLineBreaks()
	{
		FormattingOptions options = FormattingOptions.SingleLine;

		await That(options.UseLineBreaks).IsFalse();
		await That(options.Indentation).IsEmpty();
	}
}
