using System.Threading;
using aweXpect.Recording;

namespace aweXpect.Tests.Recordings;

public sealed partial class SignalCounterShould
{
	public sealed partial class NotBeSignaled
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenNotTriggered_ShouldSucceed()
			{
				SignalCounter recording = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
				CancellationToken token = cts.Token;

				async Task Act() =>
					await That(recording).Should().NotBeSignaled().WithCancellation(token);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenNotTriggeredWithParameter_ShouldSucceed()
			{
				SignalCounter<int> recording = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
				CancellationToken token = cts.Token;

				async Task Act() =>
					await That(recording).Should().NotBeSignaled().WithCancellation(token);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggered_ShouldFail()
			{
				SignalCounter recording = new();

				_ = Task.Delay(10.Milliseconds())
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
				SignalCounter<int> recording = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ => recording.Signal(42));

				async Task Act() =>
					await That(recording).Should().NotBeSignaled();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             not have recorded the callback,
					             but it was recorded once in [
					               42
					             ]
					             """);
			}
		}
	}
}
