using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aweXpect.Chronology;
using aweXpect.Core.TimeSystem;

namespace aweXpect.Core.Tests.Core.TimeSystem;
public sealed class RealTimeSystemTests
{
	[Fact]
	public async Task Stopwatch_New_ShouldReturnDifferentStopwatches()
	{
		var timeSystem = RealTimeSystem.Instance;
		var stopwatch1 = timeSystem.Stopwatch.New();
		var stopwatch2 = timeSystem.Stopwatch.New();

		stopwatch1.Start();

		await That(stopwatch1.IsRunning).IsTrue();
		await That(stopwatch2.IsRunning).IsFalse();
	}

	[Fact]
	public async Task Stopwatch_ShouldUseRealValues()
	{
		var timeSystem = RealTimeSystem.Instance;
		var stopwatch = timeSystem.Stopwatch.New();

		await That(stopwatch.IsRunning).IsFalse();

		stopwatch.Start();

		await That(stopwatch.IsRunning).IsTrue();

		await Task.Delay(10);
		stopwatch.Stop();

		await That(stopwatch.Elapsed).IsGreaterThan(10.Milliseconds());
	}
}
