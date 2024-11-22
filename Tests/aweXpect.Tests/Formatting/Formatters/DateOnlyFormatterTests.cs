#if NET6_0_OR_GREATER
using Testably.Expectations.Formatting;

namespace Testably.Expectations.Tests.Formatting.Formatters;

public sealed class DateOnlyFormatterTests
{
	[Fact]
	public async Task ShouldUseRoundtripFormat()
	{
		DateOnly value = new(2024, 11, 2);
		string result = Formatter.Format(value);

		await That(result).Should().Be("2024-11-02");
	}
}
#endif
