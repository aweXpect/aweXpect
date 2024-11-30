using System.Text;

#if NET6_0_OR_GREATER
namespace aweXpect.Core.Tests.Formatting.Formatters;

public sealed class GuidFormatterTests
{
	[Fact]
	public async Task Empty_ShouldUseDefaultFormat()
	{
		Guid value = Guid.Empty;
		string expectedResult = "00000000-0000-0000-0000-000000000000";
		StringBuilder sb = new();
		
		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}

	[Fact]
	public async Task ShouldUseRoundtripFormat()
	{
		Guid value = Guid.NewGuid();
		string expectedResult = value.ToString();
		StringBuilder sb = new();
		
		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}
}
#endif
