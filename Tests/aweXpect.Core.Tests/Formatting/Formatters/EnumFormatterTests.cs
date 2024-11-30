using System.Text;

namespace aweXpect.Core.Tests.Formatting.Formatters;

public sealed class EnumFormatterTests
{
	public enum Dummy
	{
		Foo,
		Bar
	}

	[Theory]
	[InlineData(Dummy.Foo, "Foo")]
	[InlineData(Dummy.Bar, "Bar")]
	public async Task ShouldUseStringRepresentation(Dummy value, string expectedResult)
	{
		StringBuilder sb = new();

		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}
}
