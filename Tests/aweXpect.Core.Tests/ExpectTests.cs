namespace aweXpect.Core.Tests;

public class ExpectTests
{
	[Fact]
	public async Task ShouldSupportCollectionExpressionsAsSubject()
	{
		async Task Act()
			=> await That([1, 2, 3]).Should().BeInAscendingOrder();

		await That(Act).Should().NotThrow();
	}
}
