using System.Text;

namespace aweXpect.Core.Tests.Formatting.Formatters;

public sealed class StringFormatterTests
{
	[Fact]
	public async Task Strings_ShouldUseDoubleQuotationMarks()
	{
		string value = "foo";
		string expectedResult = "\"foo\"";
		StringBuilder sb = new();

		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}
}
