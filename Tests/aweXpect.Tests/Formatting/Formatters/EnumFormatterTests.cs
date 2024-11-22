namespace Testably.Expectations.Tests.Formatting.Formatters;

public sealed class EnumFormatterTests
{
	[Theory]
	[InlineData(Dummy.Foo, "Foo")]
	[InlineData(Dummy.Bar, "Bar")]
	public async Task ShouldUseStringRepresentation(Dummy value, string expectedResult)
	{
		string result = Formatter.Format(value);

		await That(result).Should().Be(expectedResult);
	}

	public enum Dummy
	{
		Foo,
		Bar
	}
}
