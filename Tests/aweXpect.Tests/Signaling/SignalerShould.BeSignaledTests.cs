using System.Threading;
using aweXpect.Signaling;

namespace aweXpect.Tests.Signaling;

public sealed partial class SignalerShould
{
	public sealed partial class BeSignaled
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenNotTriggered_ShouldFail()
			{
				Signaler signaler = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
				CancellationToken token = cts.Token;

				async Task Act() =>
					await That(signaler).Should().BeSignaled().WithCancellation(token);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected signaler to
					             have recorded the callback at least once,
					             but it was never recorded
					             """);
			}

			[Fact]
			public async Task WhenNotTriggeredWithParameter_ShouldFail()
			{
				Signaler<int> signaler = new();
				using CancellationTokenSource cts = new(50.Milliseconds());
				CancellationToken token = cts.Token;

				async Task Act() =>
					await That(signaler).Should().BeSignaled().WithCancellation(token);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected signaler to
					             have recorded the callback at least once,
					             but it was never recorded
					             """);
			}

			[Fact]
			public async Task WhenTriggered_ShouldSucceed()
			{
				Signaler signaler = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ => signaler.Signal());

				async Task Act() =>
					await That(signaler).Should().BeSignaled();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredWithParameter_ShouldSucceed()
			{
				Signaler<int> signaler = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ => signaler.Signal(1));

				async Task Act() =>
					await That(signaler).Should().BeSignaled();

				await That(Act).Should().NotThrow();
			}
		}
	}
}
