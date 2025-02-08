using System.Threading;
using aweXpect.Signaling;

namespace aweXpect.Tests;

public sealed partial class ThatSignaler
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
					await That(signaler).DidNotSignal(2.Times())
						.With(p => p > 1).With(p => p < 3)
						.WithCancellation(token);

				await That(Act).DoesNotThrow();
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
					await That(signaler).DidNotSignal(2.Times())
						.With(p => p > 1)
						.WithCancellation(token);

				await That(Act).DoesNotThrow();
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
					await That(signaler).DidNotSignal(2.Times())
						.With(p => p > 1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that signaler
					             has not recorded the callback at least 2 times with p => p > 1,
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
					await That(signaler).DidNotSignal(2.Times())
						.With(p => p > 2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that signaler
					             has not recorded the callback at least 2 times with p => p > 2,
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
