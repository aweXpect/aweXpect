using System.Threading;
using aweXpect.Recording;

namespace aweXpect.Tests.Recordings;

public sealed partial class CallbackRecordingShould
{
	public sealed partial class NotBeSignaled
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenNotTriggered_ShouldSucceed()
			{
				ISignalCounter recording = new SignalCounter();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(TimeSpan.FromMilliseconds(50));
				CancellationToken token = cts.Token;

				async Task Act() =>
					await That(recording).Should().NotBeSignaled().WithCancellation(token);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenNotTriggeredWithParameter_ShouldSucceed()
			{
				ISignalCounter<int> recording = new SignalCounter<int>();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(TimeSpan.FromMilliseconds(50));
				CancellationToken token = cts.Token;

				async Task Act() =>
					await That(recording).Should().NotBeSignaled().WithCancellation(token);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggered_ShouldFail()
			{
				ISignalCounter recording = new SignalCounter();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ => recording.Signal());

				async Task Act() =>
					await That(recording).Should().NotBeSignaled();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             not have recorded the callback,
					             but it was recorded once
					             """);
			}

			[Fact]
			public async Task WhenTriggeredWithParameter_ShouldFail()
			{
				ISignalCounter<int> recording = new SignalCounter<int>();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ => recording.Signal(42));

				async Task Act() =>
					await That(recording).Should().NotBeSignaled();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             not have recorded the callback,
					             but it was recorded once with [
					               42
					             ]
					             """);
			}
		}
	}
}
