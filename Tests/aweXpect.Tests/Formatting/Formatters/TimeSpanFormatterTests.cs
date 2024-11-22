using Testably.Expectations.Extensions;

namespace Testably.Expectations.Tests.Formatting.Formatters;

public sealed class TimeSpanFormatterTests
{
	[Fact]
	public async Task ShouldIncludeSingleDigitMinuteEvenWhenOnlySecondsAreSpecified()
	{
		TimeSpan value = 12.Seconds();
		string result = Formatter.Format(value);

		await That(result).Should().Be("0:12");
	}

	[Fact]
	public async Task ShouldSupportDoubleDigitDays()
	{
		TimeSpan value = 13.Days(14.Hours(15.Minutes(16.Seconds())));
		string result = Formatter.Format(value);

		await That(result).Should().Be("13.14:15:16");
	}

	[Fact]
	public async Task ShouldSupportDoubleDigitHours()
	{
		TimeSpan value = 14.Hours(15.Minutes(16.Seconds()));
		string result = Formatter.Format(value);

		await That(result).Should().Be("14:15:16");
	}

	[Fact]
	public async Task ShouldSupportDoubleDigitsMinutes()
	{
		TimeSpan value = 13.Minutes(14.Seconds());
		string result = Formatter.Format(value);

		await That(result).Should().Be("13:14");
	}

	[Fact]
	public async Task ShouldSupportSingleDigitDays()
	{
		TimeSpan value = 25.Hours(15.Minutes(16.Seconds()));
		string result = Formatter.Format(value);

		await That(result).Should().Be("1.01:15:16");
	}

	[Fact]
	public async Task ShouldSupportSingleDigitHours()
	{
		TimeSpan value = 73.Minutes(14.Seconds());
		string result = Formatter.Format(value);

		await That(result).Should().Be("1:13:14");
	}
}
