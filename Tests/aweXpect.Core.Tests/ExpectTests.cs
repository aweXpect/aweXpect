namespace aweXpect.Core.Tests;

public class ExpectTests
{
	[Fact]
	public async Task ShouldSupportCollectionExpressionsAsSubject()
	{
		async Task Act()
			=> await That([1, 2, 3]).IsInAscendingOrder();

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task ShouldSupportTaskAsSubject()
	{
		Task<int> sut = Task.FromResult(42);

		async Task Act()
			=> await That(sut).IsGreaterThan(41);

		await That(Act).DoesNotThrow();
	}

#if NET8_0_OR_GREATER
	[Fact]
	public async Task ShouldSupportValueTaskAsSubject()
	{
		ValueTask<int> sut = ValueTask.FromResult(42);

		async Task Act()
			=> await That(sut).IsGreaterThan(41);

		await That(Act).DoesNotThrow();
	}
#endif
}
