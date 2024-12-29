using System.Threading;
using aweXpect.Recording;
// ReSharper disable MethodHasAsyncOverload

namespace aweXpect.Tests.Recordings;

public sealed partial class SignalCounterShould
{
	public sealed partial class BeSignaled
	{
		public sealed class WithinTests
		{
			[Fact]
			public async Task WhenNotTriggeredWithinTheGivenTimeout_ShouldFail()
			{
				SignalCounter recording = new();
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;

				_ = Task.Delay(5.Seconds(), token)
					.ContinueWith(_ => recording.Signal(), token);

				async Task Act() =>
					await That(recording).Should().BeSignaled().Within(40.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             have recorded the callback at least once within 0:00.040,
					             but it was never recorded
					             """);
				cts.Cancel();
			}

			[Fact]
			public async Task WhenNotTriggeredWithParameterWithinTheGivenTimeout_ShouldFail()
			{
				SignalCounter<string> recording = new();
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;

				_ = Task.Delay(5.Seconds(), token)
					.ContinueWith(_ => recording.Signal("foo"), token);

				async Task Act() =>
					await That(recording).Should().BeSignaled().Within(40.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             have recorded the callback at least once within 0:00.040,
					             but it was never recorded
					             """);
				cts.Cancel();
			}

			[Fact]
			public async Task WhenTriggeredWithinTheGivenTimeout_ShouldSucceed()
			{
				SignalCounter recording = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ => recording.Signal());

				async Task Act() =>
					await That(recording).Should().BeSignaled().Within(10.Seconds());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredWithParameterWithinTheGivenTimeout_ShouldSucceed()
			{
				SignalCounter<string> recording = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ => recording.Signal("foo"));

				async Task Act() =>
					await That(recording).Should().BeSignaled().Within(10.Seconds());

				await That(Act).Should().NotThrow();
			}
		}
	}
}
