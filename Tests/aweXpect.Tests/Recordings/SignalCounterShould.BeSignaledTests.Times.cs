using System.Threading;
using aweXpect.Recording;

namespace aweXpect.Tests.Recordings;

public sealed partial class SignalCounterShould
{
	public sealed partial class BeSignaled
	{
		public sealed class TimesTests
		{
			[Fact]
			public async Task WhenNotTriggeredOftenEnough_ShouldFail()
			{
				SignalCounter recording = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
				CancellationToken token = cts.Token;

				recording.Signal();

				async Task Act() =>
					await That(recording).Should().BeSignaled(2.Times()).WithCancellation(token);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             have recorded the callback at least 2 times,
					             but it was only recorded once
					             """);
			}

			[Fact]
			public async Task WhenNotTriggeredWithParameter_ShouldFail()
			{
				SignalCounter<int> recording = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
				CancellationToken token = cts.Token;

				recording.Signal(1);
				recording.Signal(2);

				async Task Act() =>
					await That(recording).Should().BeSignaled(3.Times()).WithCancellation(token);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
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
				SignalCounter recording = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ =>
					{
						recording.Signal();
						recording.Signal();
						recording.Signal();
					});

				async Task Act() =>
					await That(recording).Should().BeSignaled(2.Times());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredOftenEnough_ShouldSucceed()
			{
				SignalCounter recording = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ =>
					{
						recording.Signal();
						recording.Signal();
					});

				async Task Act() =>
					await That(recording).Should().BeSignaled(2.Times());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredWithParameterMoreOften_ShouldSucceed()
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
					await That(recording).Should().BeSignaled(3.Times());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredWithParameterOftenEnough_ShouldSucceed()
			{
				SignalCounter<int> recording = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ =>
					{
						recording.Signal(1);
						recording.Signal(2);
						recording.Signal(3);
					});

				async Task Act() =>
					await That(recording).Should().BeSignaled(3.Times());

				await That(Act).Should().NotThrow();
			}
		}
	}
}
