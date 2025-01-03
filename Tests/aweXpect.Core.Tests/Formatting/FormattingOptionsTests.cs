﻿namespace aweXpect.Core.Tests.Formatting;

public class FormattingOptionsTests
{
	[Fact]
	public async Task MultipleLines_ShouldUseLineBreaks()
	{
		FormattingOptions? multipleLines = FormattingOptions.MultipleLines;

		await That(multipleLines.UseLineBreaks).Should().BeTrue();
	}

	[Fact]
	public async Task SingleLine_ShouldNotUseLineBreaks()
	{
		FormattingOptions? multipleLines = FormattingOptions.SingleLine;

		await That(multipleLines.UseLineBreaks).Should().BeFalse();
	}
}
