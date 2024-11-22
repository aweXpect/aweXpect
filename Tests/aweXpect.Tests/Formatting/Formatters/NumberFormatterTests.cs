namespace Testably.Expectations.Tests.Formatting.Formatters;

public sealed class NumberFormatterTests
{
	[Fact]
	public async Task Numbers_Decimal_ShouldReturnExpectedValue()
	{
		decimal value = new(11.3);
		string expectedResult = "11.3";
		string result = Formatter.Format(value);

		await That(result).Should().Be(expectedResult);
	}

	[Fact]
	public async Task Numbers_Nint_ShouldReturnExpectedValue()
	{
		nint value = -123;
		string expectedResult = "-123";
		string result = Formatter.Format(value);

		await That(result).Should().Be(expectedResult);
	}

	[Fact]
	public async Task Numbers_Nuint_ShouldReturnExpectedValue()
	{
		nuint value = 123;
		string expectedResult = "123";
		string result = Formatter.Format(value);

		await That(result).Should().Be(expectedResult);
	}

	[Theory]
	[InlineData(-1, "-1")]
	[InlineData((uint)2, "2")]
	[InlineData((byte)3, "3")]
	[InlineData((sbyte)-4, "-4")]
	[InlineData((short)-5, "-5")]
	[InlineData((ushort)6, "6")]
	[InlineData((long)-7, "-7")]
	[InlineData((ulong)8, "8")]
	[InlineData(9.1F, "9.1")]
	[InlineData(10.2, "10.2")]
	public async Task Numbers_ShouldReturnExpectedValue(object value, string expectedResult)
	{
		string result = Formatter.Format(value);

		await That(result).Should().Be(expectedResult);
	}
}
