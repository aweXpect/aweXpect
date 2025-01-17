using System.Threading;
using aweXpect.Signaling;

namespace aweXpect.Tests;

public sealed partial class ThatSignaler
{
	public sealed partial class BeSignaled
	{
		public sealed class TimesTests
		{
			[Fact]
			public async Task WhenNotTriggeredOftenEnough_ShouldFail()
			{
				Signaler signaler = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
				CancellationToken token = cts.Token;

				signaler.Signal();

				async Task Act() =>
					await That(signaler).Signaled(2.Times()).WithCancellation(token);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected signaler to
					             have recorded the callback at least 2 times,
					             but it was only recorded once
					             """);
			}

			[Fact]
			public async Task WhenNotTriggeredWithParameter_ShouldFail()
			{
				Signaler<int> signaler = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
				CancellationToken token = cts.Token;

				signaler.Signal(1);
				signaler.Signal(2);

				async Task Act() =>
					await That(signaler).Signaled(3.Times()).WithCancellation(token);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected signaler to
					             have recorded the callback at least 3 times,
					             but it was only recorded 2 times in [
					               1,
					               2
					             ]
					             """);
			}

			[Fact]
			public async Task WhenTriggeredMoreOften_ShouldSucceed()
			{
				Signaler signaler = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ =>
					{
						signaler.Signal();
						signaler.Signal();
						signaler.Signal();
					});

				async Task Act() =>
					await That(signaler).Signaled(2.Times());

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredOftenEnough_ShouldSucceed()
			{
				Signaler signaler = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ =>
					{
						signaler.Signal();
						signaler.Signal();
					});

				async Task Act() =>
					await That(signaler).Signaled(2.Times());

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredWithParameterMoreOften_ShouldSucceed()
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
					await That(signaler).Signaled(3.Times());

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredWithParameterOftenEnough_ShouldSucceed()
			{
				Signaler<int> signaler = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ =>
					{
						signaler.Signal(1);
						signaler.Signal(2);
						signaler.Signal(3);
					});

				async Task Act() =>
					await That(signaler).Signaled(3.Times());

				await That(Act).Does().NotThrow();
			}
		}
	}
}
