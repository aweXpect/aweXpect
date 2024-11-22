namespace Testably.Expectations.Tests.ThatTests;

public sealed class GenericForTests
{
	[Theory]
	[InlineData(true, true, true)]
	[InlineData(true, false, false)]
	[InlineData(false, true, false)]
	[InlineData(false, false, false)]
	public async Task AndCombination_ShouldVerifyAllExpectations(bool a, bool b, bool expectSuccess)
	{
		MyCombinationClass subject = new()
		{
			A = a,
			B = b
		};

		async Task Act()
			=> await That(subject)
				.For(o => o.A, v => v.Should().BeTrue()).And
				.For(o => o.B, v => v.Should().BeTrue());

		await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
			.WithMessage($"""
			              Expected subject to
			              for .A be True and for .B be True,
			              but {(a ? "" : ".A was False")}{(!a && !b ? " and " : "")}{(b ? "" : ".B was False")}
			              """);
	}

	[Theory]
	[InlineData(true, true, true)]
	[InlineData(true, false, true)]
	[InlineData(false, true, true)]
	[InlineData(false, false, false)]
	public async Task OrCombination_ShouldVerifyAnyExpectations(bool a, bool b, bool expectSuccess)
	{
		MyCombinationClass subject = new()
		{
			A = a,
			B = b
		};

		async Task Act()
			=> await That(subject)
				.For(o => o.A, v => v.Should().BeTrue()).Or
				.For(o => o.B, v => v.Should().BeTrue());

		await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
			.WithMessage("""
			             Expected subject to
			             for .A be True or for .B be True,
			             but .A was False and .B was False
			             """);
	}

	[Theory]
	[AutoData]
	public async Task WhenConditionIsNotSatisfied_ShouldFail(int value)
	{
		int expectedValue = value + 1;
		MyClass subject = new() { Value = value };

		async Task Act()
			=> await That(subject)
				.For(o => o.Value, v => v.Should().Be(expectedValue));

		await That(Act).Should().Throw<XunitException>()
			.WithMessage($"""
			              Expected subject to
			              for .Value be {Formatter.Format(expectedValue)},
			              but .Value was {Formatter.Format(value)}
			              """);
	}

	[Theory]
	[AutoData]
	public async Task WhenConditionIsSatisfied_ShouldSucceed(int value)
	{
		MyClass subject = new() { Value = value };

		async Task Act()
			=> await That(subject).For(o => o.Value, v => v.Should().Be(value));

		await That(Act).Should().NotThrow();
	}

	private sealed class MyClass
	{
		public int Value { get; set; }
	}

	private sealed class MyCombinationClass
	{
		public bool A { get; set; }
		public bool B { get; set; }
	}
}
