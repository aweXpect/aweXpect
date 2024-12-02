﻿#if NET6_0_OR_GREATER
using System.Text;

namespace aweXpect.Core.Tests.Formatting.Formatters;

public sealed class DateOnlyFormatterTests
{
	[Fact]
	public async Task Nullable_ShouldUseRoundtripFormat()
	{
		DateOnly? value = new(2024, 11, 2);
		string expectedResult = "2024-11-02";
		StringBuilder sb = new();

		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}

	[Fact]
	public async Task ShouldUseRoundtripFormat()
	{
		DateOnly value = new(2024, 11, 2);
		string expectedResult = "2024-11-02";
		StringBuilder sb = new();

		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}
}
#endif
