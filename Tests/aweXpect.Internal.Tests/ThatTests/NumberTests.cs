namespace aweXpect.Internal.Tests.ThatTests;

public sealed class NumberTests
{
	[Theory]
	[AutoData]
	public async Task ShouldSupportValues_byte(byte subject, byte expected)
	{
		async Task Act()
			=> await That(subject).Should().Be(expected);

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected subject to
			              be equal to {Formatter.Format(expected)},
			              but it was {Formatter.Format(subject)}
			              """);
	}

	[Theory]
	[AutoData]
	public async Task ShouldSupportValues_decimal(decimal subject, decimal expected)
	{
		async Task Act()
			=> await That(subject).Should().Be(expected);

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected subject to
			              be equal to {Formatter.Format(expected)},
			              but it was {Formatter.Format(subject)}
			              """);
	}

	[Theory]
	[AutoData]
	public async Task ShouldSupportValues_double(double subject, double expected)
	{
		async Task Act()
			=> await That(subject).Should().Be(expected);

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected subject to
			              be equal to {Formatter.Format(expected)},
			              but it was {Formatter.Format(subject)}
			              """);
	}

	[Theory]
	[AutoData]
	public async Task ShouldSupportValues_float(float subject, float expected)
	{
		async Task Act()
			=> await That(subject).Should().Be(expected);

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected subject to
			              be equal to {Formatter.Format(expected)},
			              but it was {Formatter.Format(subject)}
			              """);
	}

	[Theory]
	[AutoData]
	public async Task ShouldSupportValues_int(int subject, int expected)
	{
		async Task Act()
			=> await That(subject).Should().Be(expected);

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected subject to
			              be equal to {Formatter.Format(expected)},
			              but it was {Formatter.Format(subject)}
			              """);
	}

	[Theory]
	[AutoData]
	public async Task ShouldSupportValues_long(long subject, long expected)
	{
		async Task Act()
			=> await That(subject).Should().Be(expected);

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected subject to
			              be equal to {Formatter.Format(expected)},
			              but it was {Formatter.Format(subject)}
			              """);
	}

	[Theory]
	[AutoData]
	public async Task ShouldSupportValues_sbyte(sbyte subject, sbyte expected)
	{
		async Task Act()
			=> await That(subject).Should().Be(expected);

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected subject to
			              be equal to {Formatter.Format(expected)},
			              but it was {Formatter.Format(subject)}
			              """);
	}

	[Theory]
	[AutoData]
	public async Task ShouldSupportValues_short(short subject, short expected)
	{
		async Task Act()
			=> await That(subject).Should().Be(expected);

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected subject to
			              be equal to {Formatter.Format(expected)},
			              but it was {Formatter.Format(subject)}
			              """);
	}

	[Theory]
	[AutoData]
	public async Task ShouldSupportValues_uint(uint subject, uint expected)
	{
		async Task Act()
			=> await That(subject).Should().Be(expected);

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected subject to
			              be equal to {Formatter.Format(expected)},
			              but it was {Formatter.Format(subject)}
			              """);
	}

	[Theory]
	[AutoData]
	public async Task ShouldSupportValues_ulong(ulong subject, ulong expected)
	{
		async Task Act()
			=> await That(subject).Should().Be(expected);

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected subject to
			              be equal to {Formatter.Format(expected)},
			              but it was {Formatter.Format(subject)}
			              """);
	}

	[Theory]
	[AutoData]
	public async Task ShouldSupportValues_ushort(ushort subject, ushort expected)
	{
		async Task Act()
			=> await That(subject).Should().Be(expected);

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected subject to
			              be equal to {Formatter.Format(expected)},
			              but it was {Formatter.Format(subject)}
			              """);
	}
}
