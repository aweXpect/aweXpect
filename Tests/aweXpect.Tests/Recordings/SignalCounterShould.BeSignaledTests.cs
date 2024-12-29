using System.Threading;
using aweXpect.Recording;

namespace aweXpect.Tests.Recordings;

public sealed partial class SignalCounterShould
{
	public sealed partial class BeSignaled
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenNotTriggered_ShouldFail()
			{
				SignalCounter recording = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
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
				SignalCounter<int> recording = new();
				using CancellationTokenSource cts = new(50.Milliseconds());
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
				SignalCounter recording = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ => recording.Signal());

				async Task Act() =>
					await That(recording).Should().BeSignaled();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredWithParameter_ShouldSucceed()
			{
				SignalCounter<int> recording = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ => recording.Signal(1));

				async Task Act() =>
					await That(recording).Should().BeSignaled();

				await That(Act).Should().NotThrow();
			}
		}
	}
}
