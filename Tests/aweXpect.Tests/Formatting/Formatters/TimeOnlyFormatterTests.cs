#if NET6_0_OR_GREATER
using Testably.Expectations.Formatting;

namespace Testably.Expectations.Tests.Formatting.Formatters;

public sealed class TimeOnlyFormatterTests
{
	[Fact]
	public async Task ShouldUseRoundtripFormat()
	{
		TimeOnly value = new(15, 42, 15, 234);
		string result = Formatter.Format(value);

		await That(result).Should().Be("15:42:15.2340000");
	}
}
#endif
