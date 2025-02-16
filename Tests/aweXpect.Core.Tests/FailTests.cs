namespace aweXpect.Core.Tests;

public sealed class FailTests
{
	[Theory]
	[AutoData]
	public async Task Test_ShouldThrowException(string reason)
	{
		void Act() => Fail.Test(reason);

		await That(Act).Throws<XunitException>()
			.WithMessage(reason);
	}

	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public async Task Unless_ShouldThrowException(bool condition, string reason)
	{
		void Act() => Fail.Unless(condition, reason);

		await That(Act).Throws<XunitException>().OnlyIf(!condition)
			.WithMessage(reason);
	}

	[Theory]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public async Task When_ShouldThrowException(bool condition, string reason)
	{
		void Act() => Fail.When(condition, reason);

		await That(Act).Throws<XunitException>().OnlyIf(condition)
			.WithMessage(reason);
	}
}
