using System.Threading;
using aweXpect.Recording;

namespace aweXpect.Tests.Recordings;

public sealed partial class CallbackRecordingShould
{
	public sealed partial class BeSignaled
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenNotTriggered_ShouldFail()
			{
				ISignalCounter recording = new SignalCounter();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(TimeSpan.FromMilliseconds(50));
				CancellationToken token = cts.Token;

				async Task Act() =>
					await That(recording).Should().BeSignaled().WithCancellation(token);

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
				ISignalCounter<int> recording = new SignalCounter<int>();
				using CancellationTokenSource cts = new(TimeSpan.FromMilliseconds(50));
				CancellationToken token = cts.Token;

				async Task Act() =>
					await That(recording).Should().BeSignaled().WithCancellation(token);

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
				ISignalCounter recording = new SignalCounter();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ => recording.Signal());

				async Task Act() =>
					await That(recording).Should().BeSignaled();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredWithParameter_ShouldSucceed()
			{
				ISignalCounter<int> recording = new SignalCounter<int>();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ => recording.Signal(1));

				async Task Act() =>
					await That(recording).Should().BeSignaled();

				await That(Act).Should().NotThrow();
			}
		}
	}
}
