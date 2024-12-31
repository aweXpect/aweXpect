using System.Threading;
using aweXpect.Signaling;

namespace aweXpect.Tests.Signaling;

public sealed partial class SignalerShould
{
	public sealed partial class NotBeSignaled
	{
		public sealed class WithTests
		{
			[Fact]
			public async Task WhenApplyingMultiplePredicates_ShouldVerifyAll()
			{
				Signaler<int> signaler = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
				CancellationToken token = cts.Token;

				signaler.Signal(1);
				signaler.Signal(2);
				signaler.Signal(3);

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled(2.Times())
						.With(p => p > 1).With(p => p < 3)
						.WithCancellation(token);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenNotTriggeredOftenEnoughMatchingPredicate_ShouldSucceed()
			{
				Signaler<int> signaler = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
				CancellationToken token = cts.Token;

				signaler.Signal(1);
				signaler.Signal(2);

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled(2.Times())
						.With(p => p > 1)
						.WithCancellation(token);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredMoreOftenMatchingPredicate_ShouldFail()
			{
				Signaler<int> signaler = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ =>
					{
						signaler.Signal(1);
						signaler.Signal(2);
						signaler.Signal(3);
						signaler.Signal(4);
					});

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled(2.Times())
						.With(p => p > 1);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected signaler to
					             not have recorded the callback at least 2 times with p => p > 1,
					             but it was recorded ? times in [
					               1,
					               2,
					               *
					             ]
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenTriggeredOftenEnoughMatchingPredicate_ShouldFail()
			{
				Signaler<int> signaler = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ =>
					{
						signaler.Signal(1);
						signaler.Signal(2);
						signaler.Signal(3);
						signaler.Signal(4);
					});

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled(2.Times())
						.With(p => p > 2);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected signaler to
					             not have recorded the callback at least 2 times with p => p > 2,
					             but it was recorded 2 times in [
					               1,
					               2,
					               3,
					               4
					             ]
					             """);
			}
		}
	}
}
