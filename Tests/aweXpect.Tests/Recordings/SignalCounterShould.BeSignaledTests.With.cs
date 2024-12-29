using System.Threading;
using aweXpect.Recording;

namespace aweXpect.Tests.Recordings;

public sealed partial class SignalCounterShould
{
	public sealed partial class BeSignaled
	{
		public sealed class WithTests
		{
			[Fact]
			public async Task WhenApplyingMultiplePredicates_ShouldVerifyAll()
			{
				SignalCounter<int> recording = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
				CancellationToken token = cts.Token;

				recording.Signal(1);
				recording.Signal(2);
				recording.Signal(3);

				async Task Act() =>
					await That(recording).Should().BeSignaled(2.Times())
						.With(p => p > 1).With(p => p < 3)
						.WithCancellation(token);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             have recorded the callback at least 2 times with p => p > 1 and with p => p < 3,
					             but it was only recorded once in [
					               1,
					               2,
					               3
					             ]
					             """);
			}

			[Fact]
			public async Task WhenNotTriggeredOftenEnoughMatchingPredicate_ShouldFail()
			{
				SignalCounter<int> recording = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
				CancellationToken token = cts.Token;

				recording.Signal(1);
				recording.Signal(2);

				async Task Act() =>
					await That(recording).Should().BeSignaled(2.Times())
						.With(p => p > 1)
						.WithCancellation(token);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             have recorded the callback at least 2 times with p => p > 1,
					             but it was only recorded once in [
					               1,
					               2
					             ]
					             """);
			}

			[Fact]
			public async Task WhenTriggeredMoreOftenMatchingPredicate_ShouldSucceed()
			{
				SignalCounter<int> recording = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ =>
					{
						recording.Signal(1);
						recording.Signal(2);
						recording.Signal(3);
						recording.Signal(4);
					});

				async Task Act() =>
					await That(recording).Should().BeSignaled(2.Times())
						.With(p => p < 4);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredOftenEnoughMatchingPredicate_ShouldSucceed()
			{
				SignalCounter<int> recording = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ =>
					{
						recording.Signal(1);
						recording.Signal(2);
						recording.Signal(3);
						recording.Signal(4);
					});

				async Task Act() =>
					await That(recording).Should().BeSignaled(2.Times())
						.With(p => p < 3);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
