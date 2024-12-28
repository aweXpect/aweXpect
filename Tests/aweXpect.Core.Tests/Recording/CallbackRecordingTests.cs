using aweXpect.Recording;
using Record = aweXpect.Recording.Record;

namespace aweXpect.Core.Tests.Recording;

public class CallbackRecordingTests
{
	[Fact]
	public async Task X()
	{
		ICallbackRecording recording = Record.Callback();

		_ = Task.Delay(1000).ContinueWith(_ => recording.Trigger());

		ICallbackRecordingResult result = recording.Wait(timeout: TimeSpan.FromMilliseconds(1100));

		await That(result.IsSuccess).Should().BeTrue();
	}

	[Fact]
	public async Task X2()
	{
		ICallbackRecording recording = Record.Callback();

		_ = Task.Delay(10000).ContinueWith(_ => recording.Trigger());

		ICallbackRecordingResult result = recording.Wait(timeout: TimeSpan.FromMilliseconds(500));

		await That(result.IsSuccess).Should().BeFalse();
	}
}
