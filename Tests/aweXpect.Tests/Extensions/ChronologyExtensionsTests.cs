using Testably.Expectations.Extensions;

namespace Testably.Expectations.Tests.Extensions;

public class ChronologyExtensionsTests
{
	[Theory]
	[AutoData]
	public async Task Double_Days_ShouldReturnCorrectTimeSpan(double days)
	{
		TimeSpan expected = TimeSpan.FromDays(days);

		TimeSpan result = days.Days();

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Double_Days_WithOffset_ShouldReturnCorrectTimeSpan(double days,
		TimeSpan offset)
	{
		TimeSpan expected = TimeSpan.FromDays(days) + offset;

		TimeSpan result = days.Days(offset);

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Double_Hours_ShouldReturnCorrectTimeSpan(double hours)
	{
		TimeSpan expected = TimeSpan.FromHours(hours);

		TimeSpan result = hours.Hours();

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Double_Hours_WithOffset_ShouldReturnCorrectTimeSpan(double hours,
		TimeSpan offset)
	{
		TimeSpan expected = TimeSpan.FromHours(hours) + offset;

		TimeSpan result = hours.Hours(offset);

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Double_Milliseconds_ShouldReturnCorrectTimeSpan(double milliseconds)
	{
		TimeSpan expected = TimeSpan.FromMilliseconds(milliseconds);

		TimeSpan result = milliseconds.Milliseconds();

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Double_Milliseconds_WithOffset_ShouldReturnCorrectTimeSpan(
		double milliseconds, TimeSpan offset)
	{
		TimeSpan expected = TimeSpan.FromMilliseconds(milliseconds) + offset;

		TimeSpan result = milliseconds.Milliseconds(offset);

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Double_Minutes_ShouldReturnCorrectTimeSpan(double minutes)
	{
		TimeSpan expected = TimeSpan.FromMinutes(minutes);

		TimeSpan result = minutes.Minutes();

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Double_Minutes_WithOffset_ShouldReturnCorrectTimeSpan(double minutes,
		TimeSpan offset)
	{
		TimeSpan expected = TimeSpan.FromMinutes(minutes) + offset;

		TimeSpan result = minutes.Minutes(offset);

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Double_Seconds_ShouldReturnCorrectTimeSpan(double seconds)
	{
		TimeSpan expected = TimeSpan.FromSeconds(seconds);

		TimeSpan result = seconds.Seconds();

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Double_Seconds_WithOffset_ShouldReturnCorrectTimeSpan(double seconds,
		TimeSpan offset)
	{
		TimeSpan expected = TimeSpan.FromSeconds(seconds) + offset;

		TimeSpan result = seconds.Seconds(offset);

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Int_Days_ShouldReturnCorrectTimeSpan(int days)
	{
		TimeSpan expected = TimeSpan.FromDays(days);

		TimeSpan result = days.Days();

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Int_Days_WithOffset_ShouldReturnCorrectTimeSpan(int days, TimeSpan offset)
	{
		TimeSpan expected = TimeSpan.FromDays(days) + offset;

		TimeSpan result = days.Days(offset);

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Int_Hours_ShouldReturnCorrectTimeSpan(int hours)
	{
		TimeSpan expected = TimeSpan.FromHours(hours);

		TimeSpan result = hours.Hours();

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Int_Hours_WithOffset_ShouldReturnCorrectTimeSpan(int hours, TimeSpan offset)
	{
		TimeSpan expected = TimeSpan.FromHours(hours) + offset;

		TimeSpan result = hours.Hours(offset);

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Int_Milliseconds_ShouldReturnCorrectTimeSpan(int milliseconds)
	{
		TimeSpan expected = TimeSpan.FromMilliseconds(milliseconds);

		TimeSpan result = milliseconds.Milliseconds();

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Int_Milliseconds_WithOffset_ShouldReturnCorrectTimeSpan(int milliseconds,
		TimeSpan offset)
	{
		TimeSpan expected = TimeSpan.FromMilliseconds(milliseconds) + offset;

		TimeSpan result = milliseconds.Milliseconds(offset);

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Int_Minutes_ShouldReturnCorrectTimeSpan(int minutes)
	{
		TimeSpan expected = TimeSpan.FromMinutes(minutes);

		TimeSpan result = minutes.Minutes();

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Int_Minutes_WithOffset_ShouldReturnCorrectTimeSpan(int minutes,
		TimeSpan offset)
	{
		TimeSpan expected = TimeSpan.FromMinutes(minutes) + offset;

		TimeSpan result = minutes.Minutes(offset);

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Int_Seconds_ShouldReturnCorrectTimeSpan(int seconds)
	{
		TimeSpan expected = TimeSpan.FromSeconds(seconds);

		TimeSpan result = seconds.Seconds();

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task Int_Seconds_WithOffset_ShouldReturnCorrectTimeSpan(int seconds,
		TimeSpan offset)
	{
		TimeSpan expected = TimeSpan.FromSeconds(seconds) + offset;

		TimeSpan result = seconds.Seconds(offset);

		await That(result).Should().Be(expected);
	}
}
