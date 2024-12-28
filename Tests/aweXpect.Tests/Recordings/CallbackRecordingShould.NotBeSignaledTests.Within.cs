using System.Threading;
using aweXpect.Recording;
// ReSharper disable MethodHasAsyncOverload

namespace aweXpect.Tests.Recordings;

public sealed partial class CallbackRecordingShould
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

				_ = Task.Delay(TimeSpan.FromSeconds(5), token)
					.ContinueWith(_ => recording.Signal(), token);

				async Task Act() =>
					await That(recording).Should().NotBeSignaled().Within(TimeSpan.FromMilliseconds(40));

				await That(Act).Should().NotThrow();
				cts.Cancel();
			}

			[Fact]
			public async Task WhenNotTriggeredWithParameterWithinTheGivenTimeout_ShouldSucceed()
			{
				SignalCounter<string> recording = new();
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;

				_ = Task.Delay(TimeSpan.FromSeconds(5), token)
					.ContinueWith(_ => recording.Signal("foo"), token);

				async Task Act() =>
					await That(recording).Should().NotBeSignaled().Within(TimeSpan.FromMilliseconds(40));

				await That(Act).Should().NotThrow();
				cts.Cancel();
			}

			[Fact]
			public async Task WhenTriggeredWithinTheGivenTimeout_ShouldFail()
			{
				SignalCounter recording = new();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ => recording.Signal());

				async Task Act() =>
					await That(recording).Should().NotBeSignaled().Within(TimeSpan.FromSeconds(10));

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

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ => recording.Signal("foo"));

				async Task Act() =>
					await That(recording).Should().NotBeSignaled().Within(TimeSpan.FromSeconds(10));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             not have recorded the callback within 0:10,
					             but it was recorded once with [
					               "foo"
					             ]
					             """);
			}
		}
	}
}
