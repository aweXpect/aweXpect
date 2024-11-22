namespace aweXpect.Core.Tests.Core;

public sealed class ExpectationTests
{
	[Theory]
	[InlineData(false, true)]
	[InlineData(true, false)]
	[InlineData(false, false)]
	public async Task And_ShouldFailWhenAnyArgumentFails(bool a, bool b)
	{
		async Task Act()
			=> await That(true).Should().Be(a).And.Be(b);

		await That(Act).Should().Throw<XunitException>();
	}

	[Theory]
	[InlineData(true, true)]
	public async Task And_ShouldRequireBothArgumentsToSucceed(bool a, bool b)
	{
		async Task Act()
			=> await That(true).Should().Be(a).And.Be(b);

		await That(Act).Should().NotThrow();
	}

	[Theory]
	[InlineData(false, false)]
	public async Task Or_ShouldFailWhenBothArgumentsFail(bool a, bool b)
	{
		async Task Act()
			=> await That(true).Should().Be(a).Or.Be(b);

		await That(Act).Should().ThrowException();
	}

	[Theory]
	[InlineData(false, true)]
	[InlineData(true, false)]
	[InlineData(true, true)]
	public async Task Or_ShouldRequireAnyArgumentToSucceed(bool a, bool b)
	{
		async Task Act()
			=> await That(true).Should().Be(a).Or.Be(b);

		await That(Act).Should().NotThrow();
	}
}
