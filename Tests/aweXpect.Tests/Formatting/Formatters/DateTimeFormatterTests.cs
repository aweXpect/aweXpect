namespace Testably.Expectations.Tests.Formatting.Formatters;

public sealed class DateTimeFormatterTests
{
	[Fact]
	public async Task ShouldUseRoundtripFormat()
	{
		DateTime value = new(2024, 11, 2, 15, 42, 08, 123);
		string result = Formatter.Format(value);

		await That(result).Should().Be("2024-11-02T15:42:08.1230000");
	}
}
