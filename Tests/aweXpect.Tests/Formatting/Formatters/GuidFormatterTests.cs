#if NET6_0_OR_GREATER
using Testably.Expectations.Formatting;

namespace Testably.Expectations.Tests.Formatting.Formatters;

public sealed class GuidFormatterTests
{
	[Fact]
	public async Task Empty_ShouldUseDefaultFormat()
	{
		Guid value = Guid.Empty;
		string result = Formatter.Format(value);

		await That(result).Should().Be("00000000-0000-0000-0000-000000000000");
	}

	[Fact]
	public async Task ShouldUseRoundtripFormat()
	{
		Guid value = Guid.NewGuid();
		string expected = value.ToString();
		string result = Formatter.Format(value);

		await That(result).Should().Be(expected);
	}
}
#endif
