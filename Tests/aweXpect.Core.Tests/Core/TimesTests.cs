namespace aweXpect.Core.Tests.Core;

public class TimesTests
{
	[Fact]
	public async Task ImplicitConversion_ShouldWorkAsExpected()
	{
		int expectedValue = 5;

		Times times = expectedValue;
		int actualValue = times;

		await That(times.Value).IsEqualTo(expectedValue);
		await That(actualValue).IsEqualTo(expectedValue);
	}
}
