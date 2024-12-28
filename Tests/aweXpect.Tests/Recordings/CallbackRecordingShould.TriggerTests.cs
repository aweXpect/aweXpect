using System.Threading;
using aweXpect.Recording;
using Record = aweXpect.Recording.Record;

namespace aweXpect.Tests.Recordings;

public sealed partial class CallbackRecordingShould
{
	public sealed partial class Trigger
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenNotTriggered_ShouldFail()
			{
				ICallbackRecording recording = Record.Callback();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(TimeSpan.FromMilliseconds(50));
				CancellationToken token = cts.Token;

				async Task Act() =>
					await That(recording).Should().Trigger().WithCancellation(token);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             have recorded the callback at least once,
					             but it was never recorded
					             """);
			}

			[Fact]
			public async Task WhenNotTriggeredWithParameter_ShouldFail()
			{
				ICallbackRecording<int> recording = Record.Callback<int>();
				using CancellationTokenSource cts = new(TimeSpan.FromMilliseconds(50));
				CancellationToken token = cts.Token;

				async Task Act() =>
					await That(recording).Should().Trigger().WithCancellation(token);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             have recorded the callback at least once,
					             but it was never recorded
					             """);
			}

			[Fact]
			public async Task WhenTriggered_ShouldSucceed()
			{
				ICallbackRecording recording = Record.Callback();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ => recording.Trigger());

				async Task Act() =>
					await That(recording).Should().Trigger();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredWithParameter_ShouldSucceed()
			{
				ICallbackRecording<int> recording = Record.Callback<int>();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ => recording.Trigger(1));

				async Task Act() =>
					await That(recording).Should().Trigger();

				await That(Act).Should().NotThrow();
			}
		}
	}
}
