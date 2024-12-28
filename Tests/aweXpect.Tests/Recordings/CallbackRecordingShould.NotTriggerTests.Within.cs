using System.Threading;
using aweXpect.Recording;
using Record = aweXpect.Recording.Record;

namespace aweXpect.Tests.Recordings;

public sealed partial class CallbackRecordingShould
{
	public sealed partial class NotTrigger
	{
		public sealed class WithinTests
		{
			[Fact]
			public async Task WhenNotTriggeredWithinTheGivenTimeout_ShouldSucceed()
			{
				ICallbackRecording recording = Record.Callback();
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;

				_ = Task.Delay(TimeSpan.FromSeconds(5), token)
					.ContinueWith(_ => recording.Trigger(), token);

				async Task Act() =>
					await That(recording).Should().NotTrigger().Within(TimeSpan.FromMilliseconds(40));

				await That(Act).Should().NotThrow();
				cts.Cancel();
			}

			[Fact]
			public async Task WhenNotTriggeredWithParameterWithinTheGivenTimeout_ShouldSucceed()
			{
				ICallbackRecording<string> recording = Record.Callback<string>();
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;

				_ = Task.Delay(TimeSpan.FromSeconds(5), token)
					.ContinueWith(_ => recording.Trigger("foo"), token);

				async Task Act() =>
					await That(recording).Should().NotTrigger().Within(TimeSpan.FromMilliseconds(40));

				await That(Act).Should().NotThrow();
				cts.Cancel();
			}

			[Fact]
			public async Task WhenTriggeredWithinTheGivenTimeout_ShouldFail()
			{
				ICallbackRecording recording = Record.Callback();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ => recording.Trigger());

				async Task Act() =>
					await That(recording).Should().NotTrigger().Within(TimeSpan.FromSeconds(10));

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
				ICallbackRecording<string> recording = Record.Callback<string>();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ => recording.Trigger("foo"));

				async Task Act() =>
					await That(recording).Should().NotTrigger().Within(TimeSpan.FromSeconds(10));

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
