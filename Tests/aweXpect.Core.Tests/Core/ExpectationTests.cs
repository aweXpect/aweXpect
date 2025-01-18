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
			=> await That(true).Is(a).And.Is(b);

		await That(Act).Does().Throw<XunitException>();
	}

	[Theory]
	[InlineData(true, true)]
	public async Task And_ShouldRequireBothArgumentsToSucceed(bool a, bool b)
	{
		async Task Act()
			=> await That(true).Is(a).And.Is(b);

		await That(Act).Does().NotThrow();
	}

	[Theory]
	[InlineData(false, false)]
	public async Task Or_ShouldFailWhenBothArgumentsFail(bool a, bool b)
	{
		async Task Act()
			=> await That(true).Is(a).Or.Is(b);

		await That(Act).Does().ThrowException();
	}

	[Theory]
	[InlineData(false, true)]
	[InlineData(true, false)]
	[InlineData(true, true)]
	public async Task Or_ShouldRequireAnyArgumentToSucceed(bool a, bool b)
	{
		async Task Act()
			=> await That(true).Is(a).Or.Is(b);

		await That(Act).Does().NotThrow();
	}
}
