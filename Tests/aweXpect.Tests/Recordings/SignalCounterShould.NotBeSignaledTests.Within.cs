using System.Threading;
using aweXpect.Recording;
// ReSharper disable MethodHasAsyncOverload

namespace aweXpect.Tests.Recordings;

public sealed partial class SignalCounterShould
{
	public sealed partial class NotBeSignaled
	{
		public sealed class WithinTests
		{
			[Fact]
			public async Task WhenNotTriggeredWithinTheGivenTimeout_ShouldSucceed()
			{
				SignalCounter recording = new();
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;

				_ = Task.Delay(5.Seconds(), token)
					.ContinueWith(_ => recording.Signal(), token);

				async Task Act() =>
					await That(recording).Should().NotBeSignaled().Within(40.Milliseconds());

				await That(Act).Should().NotThrow();
				cts.Cancel();
			}

			[Fact]
			public async Task WhenNotTriggeredWithParameterWithinTheGivenTimeout_ShouldSucceed()
			{
				SignalCounter<string> recording = new();
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;

				_ = Task.Delay(5.Seconds(), token)
					.ContinueWith(_ => recording.Signal("foo"), token);

				async Task Act() =>
					await That(recording).Should().NotBeSignaled().Within(40.Milliseconds());

				await That(Act).Should().NotThrow();
				cts.Cancel();
			}

			[Fact]
			public async Task WhenTriggeredWithinTheGivenTimeout_ShouldFail()
			{
				SignalCounter recording = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ => recording.Signal());

				async Task Act() =>
					await That(recording).Should().NotBeSignaled().Within(10.Seconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             not have recorded the callback within 0:10,
					             but it was recorded once
					             """);
			}

			[Fact]
			public async Task WhenTriggeredWithParameterWithinTheGivenTimeout_ShouldFail()
			{
				SignalCounter<string> recording = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ => recording.Signal("foo"));

				async Task Act() =>
					await That(recording).Should().NotBeSignaled().Within(10.Seconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             not have recorded the callback within 0:10,
					             but it was recorded once in [
					               "foo"
					             ]
					             """);
			}
		}
	}
}
