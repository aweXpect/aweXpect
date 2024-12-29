using System.Threading;
using aweXpect.Recording;

namespace aweXpect.Tests.Recordings;

public sealed partial class SignalCounterShould
{
	public sealed partial class NotBeSignaled
	{
		public sealed class TimesTests
		{
			[Fact]
			public async Task WhenNotTriggeredOftenEnough_ShouldSucceed()
			{
				SignalCounter recording = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(TimeSpan.FromMilliseconds(50));
				CancellationToken token = cts.Token;

				recording.Signal();

				async Task Act() =>
					await That(recording).Should().NotBeSignaled(2.Times()).WithCancellation(token);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenNotTriggeredWithParameter_ShouldSucceeded()
			{
				SignalCounter<int> recording = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(TimeSpan.FromMilliseconds(50));
				CancellationToken token = cts.Token;

				recording.Signal(1);
				recording.Signal(2);

				async Task Act() =>
					await That(recording).Should().NotBeSignaled(3.Times()).WithCancellation(token);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredMoreOften_ShouldFail()
			{
				SignalCounter recording = new();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ =>
					{
						recording.Signal();
						recording.Signal();
						recording.Signal();
					});

				async Task Act() =>
					await That(recording).Should().NotBeSignaled(2.Times());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             not have recorded the callback at least 2 times,
					             but it was recorded ? times
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenTriggeredOftenEnough_ShouldFail()
			{
				SignalCounter recording = new();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ =>
					{
						recording.Signal();
						recording.Signal();
					});

				async Task Act() =>
					await That(recording).Should().NotBeSignaled(2.Times());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             not have recorded the callback at least 2 times,
					             but it was recorded 2 times
					             """);
			}

			[Fact]
			public async Task WhenTriggeredWithParameterMoreOften_ShouldFail()
			{
				SignalCounter<int> recording = new();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ =>
					{
						recording.Signal(1);
						recording.Signal(2);
						recording.Signal(3);
						recording.Signal(4);
					});

				async Task Act() =>
					await That(recording).Should().NotBeSignaled(3.Times());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             not have recorded the callback at least 3 times,
					             but it was recorded ? times in [
					               1,
					               2,
					               3*
					             ]
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenTriggeredWithParameterOftenEnough_ShouldFail()
			{
				SignalCounter<int> recording = new();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ =>
					{
						recording.Signal(1);
						recording.Signal(2);
						recording.Signal(3);
					});

				async Task Act() =>
					await That(recording).Should().NotBeSignaled(3.Times());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             not have recorded the callback at least 3 times,
					             but it was recorded 3 times in [
					               1,
					               2,
					               3
					             ]
					             """);
			}
		}
	}
}
