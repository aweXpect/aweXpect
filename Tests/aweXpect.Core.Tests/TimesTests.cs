namespace aweXpect.Core.Tests;

public class TimesTests
{
	[Theory]
	[AutoData]
	public async Task ExplicitConstructor_ShouldSetValueProperty(int value)
	{
		Times times = new(value);

		await That(times.Value).Should().Be(value);
	}

	[Theory]
	[AutoData]
	public async Task ExtensionMethod_ShouldSetValueProperty(int value)
	{
		Times times = value.Times();

		await That(times.Value).Should().Be(value);
	}

	[Theory]
	[AutoData]
	public async Task ImplicitOperator_ShouldSetValueProperty(int value)
	{
		Times times = value;

		await That(times.Value).Should().Be(value);
	}
}
