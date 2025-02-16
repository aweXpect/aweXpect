using aweXpect.Chronology;
using aweXpect.Core.TimeSystem;

namespace aweXpect.Core.Tests.Core.TimeSystem;

public sealed class RealTimeSystemTests
{
	[Fact]
	public async Task Stopwatch_New_ShouldReturnDifferentStopwatches()
	{
		ITimeSystem timeSystem = RealTimeSystem.Instance;
		IStopwatch stopwatch1 = timeSystem.Stopwatch.New();
		IStopwatch stopwatch2 = timeSystem.Stopwatch.New();

		stopwatch1.Start();

		await That(stopwatch1.IsRunning).IsTrue();
		await That(stopwatch2.IsRunning).IsFalse();
	}

	[Fact]
	public async Task Stopwatch_ShouldUseRealValues()
	{
		ITimeSystem timeSystem = RealTimeSystem.Instance;
		IStopwatch stopwatch = timeSystem.Stopwatch.New();

		await That(stopwatch.IsRunning).IsFalse();

		stopwatch.Start();

		await That(stopwatch.IsRunning).IsTrue();

		await Task.Delay(20.Milliseconds());
		stopwatch.Stop();

		await That(stopwatch.Elapsed).IsGreaterThan(10.Milliseconds());
	}
}
