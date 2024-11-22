namespace Testably.Expectations.Tests.Formatting.Formatters;

public sealed class StringFormatterTests
{
	[Fact]
	public async Task Strings_ShouldUseDoubleQuotationMarks()
	{
		string value = "foo";

		string result = Formatter.Format(value);

		await That(result).Should().Be("\"foo\"");
	}
}
