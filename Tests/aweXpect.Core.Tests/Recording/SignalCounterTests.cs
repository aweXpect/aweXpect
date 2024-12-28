using aweXpect.Recording;

namespace aweXpect.Core.Tests.Recording;

public class SignalCounterTests
{
	[Fact]
	public async Task X()
	{
		SignalCounter recording = new SignalCounter();

		_ = Task.Delay(1000).ContinueWith(_ => recording.Signal());

		SignalCounterResult result = recording.Wait(timeout: TimeSpan.FromMilliseconds(1100));

		await That(result.IsSuccess).Should().BeTrue();
	}

	[Fact]
	public async Task X2()
	{
		SignalCounter recording = new SignalCounter();

		_ = Task.Delay(10000).ContinueWith(_ => recording.Signal());

		SignalCounterResult result = recording.Wait(timeout: TimeSpan.FromMilliseconds(500));

		await That(result.IsSuccess).Should().BeFalse();
	}
}
