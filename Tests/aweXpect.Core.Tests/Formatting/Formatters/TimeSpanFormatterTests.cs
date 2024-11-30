using System.Text;
using aweXpect.Extensions;

namespace aweXpect.Core.Tests.Formatting.Formatters;

public sealed class TimeSpanFormatterTests
{
	[Fact]
	public async Task ShouldIncludeSingleDigitMinuteEvenWhenOnlySecondsAreSpecified()
	{
		TimeSpan value = 12.Seconds();
		string expectedResult = "0:12";
		StringBuilder sb = new();

		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}

	[Fact]
	public async Task ShouldSupportDoubleDigitDays()
	{
		TimeSpan value = 13.Days(14.Hours(15.Minutes(16.Seconds())));
		string expectedResult = "13.14:15:16";
		StringBuilder sb = new();

		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}

	[Fact]
	public async Task ShouldSupportDoubleDigitHours()
	{
		TimeSpan value = 14.Hours(15.Minutes(16.Seconds()));
		string expectedResult = "14:15:16";
		StringBuilder sb = new();

		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}

	[Fact]
	public async Task ShouldSupportDoubleDigitsMinutes()
	{
		TimeSpan value = 13.Minutes(14.Seconds());
		string expectedResult = "13:14";
		StringBuilder sb = new();

		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}

	[Fact]
	public async Task ShouldSupportSingleDigitDays()
	{
		TimeSpan value = 25.Hours(15.Minutes(16.Seconds()));
		string expectedResult = "1.01:15:16";
		StringBuilder sb = new();

		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}

	[Fact]
	public async Task ShouldSupportSingleDigitHours()
	{
		TimeSpan value = 73.Minutes(14.Seconds());
		string expectedResult = "1:13:14";
		StringBuilder sb = new();

		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}
}
