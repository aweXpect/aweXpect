using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core.Sources;

public class DelegateValueSourceTests
{
	[Fact]
	public async Task ForExecutionTime_ShouldUseElapsedFromTimeSystem()
	{
		TimeSystemMock timeSystem = new TimeSystemMock().SetElapsed(TimeSpan.FromSeconds(1.1));

		async Task Act() =>
			await That(() => 1).Should().NotExecuteWithin(TimeSpan.FromSeconds(1))
				.UseTimeSystem(timeSystem);

		await That(Act).Should().NotThrow();
	}
}
