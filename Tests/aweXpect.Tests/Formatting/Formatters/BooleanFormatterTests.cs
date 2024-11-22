namespace Testably.Expectations.Tests.Formatting.Formatters;

public sealed class BooleanFormatterTests
{
	[Theory]
	[InlineData(true, "True")]
	[InlineData(false, "False")]
	public async Task Booleans_ShouldHaveCapitalizedFirstLetter(bool value, string expectedResult)
	{
		string result = Formatter.Format(value);

		await That(result).Should().Be(expectedResult);
	}
}
