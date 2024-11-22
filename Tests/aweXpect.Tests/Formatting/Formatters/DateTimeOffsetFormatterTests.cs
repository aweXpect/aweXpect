namespace Testably.Expectations.Tests.Formatting.Formatters;

public sealed class DateTimeOffsetFormatterTests
{
	[Fact]
	public async Task ShouldUseRoundtripFormat()
	{
		DateTimeOffset value = new(2024, 11, 2, 15, 42, 08, 123, TimeSpan.FromHours(3));
		string result = Formatter.Format(value);

		await That(result).Should().Be("2024-11-02T15:42:08.1230000+03:00");
	}
}
