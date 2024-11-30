using System.Text;

namespace aweXpect.Core.Tests.Formatting.Formatters;

public sealed class NumberFormatterTests
{
	public static TheoryData<object?, string> GetValues
		=> new()
		{
			{ -1, "-1" },
			{ (uint)2, "2" },
			{ (byte)3, "3" },
			{ (sbyte)-4, "-4" },
			{ (short)-5, "-5" },
			{ (ushort)6, "6" },
			{ (long)-7, "-7" },
			{ (ulong)8, "8" },
			{ 9.1F, "9.1" },
			{ 10.2, "10.2" },
			{ new decimal(11.3), "11.3" },
			{ (int?)-10, "-10" },
			{ (uint?)20, "20" },
			{ (byte?)30, "30" },
			{ (sbyte?)-40, "-40" },
			{ (short?)-50, "-50" },
			{ (ushort?)60, "60" },
			{ (long?)-70, "-70" },
			{ (ulong?)80, "80" },
			{ (float?)9.01F, "9.01" },
			{ (double?)10.02, "10.02" },
			{ (decimal?)new decimal(11.03), "11.03" }
		};

	[Fact]
	public async Task Numbers_Nint_ShouldReturnExpectedValue()
	{
		nint value = -123;
		string expectedResult = "-123";
		StringBuilder sb = new();

		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}

	[Fact]
	public async Task Numbers_Nuint_ShouldReturnExpectedValue()
	{
		nuint value = 123;
		string expectedResult = "123";
		StringBuilder sb = new();

		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}

	[Theory]
	[MemberData(nameof(GetValues))]
	public async Task Numbers_ShouldReturnExpectedValue(object? value, string expectedResult)
	{
		StringBuilder sb = new();

		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}

	[Fact]
	public async Task NullableNumbers_Nint_ShouldReturnExpectedValue()
	{
		nint? value = -123;
		string expectedResult = "-123";
		StringBuilder sb = new();

		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}

	[Fact]
	public async Task NullableNumbers_Nuint_ShouldReturnExpectedValue()
	{
		nuint? value = 123;
		string expectedResult = "123";
		StringBuilder sb = new();

		string result = Formatter.Format(value);
		Formatter.Format(sb, value);

		await That(result).Should().Be(expectedResult);
		await That(sb.ToString()).Should().Be(expectedResult);
	}
}
