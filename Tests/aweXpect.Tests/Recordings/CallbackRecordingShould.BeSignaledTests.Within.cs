using System.Threading;
using aweXpect.Recording;
using Record = aweXpect.Recording.Record;

namespace aweXpect.Tests.Recordings;

public sealed partial class CallbackRecordingShould
{
	public sealed partial class BeSignaled
	{
		public sealed class WithinTests
		{
			[Fact]
			public async Task WhenNotTriggeredWithinTheGivenTimeout_ShouldFail()
			{
				ICallbackRecording recording = Record.Callback();
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;

				_ = Task.Delay(TimeSpan.FromSeconds(5), token)
					.ContinueWith(_ => recording.Signal(), token);

				async Task Act() =>
					await That(recording).Should().BeSignaled().Within(TimeSpan.FromMilliseconds(40));

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
				ICallbackRecording<string> recording = Record.Callback<string>();
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;

				_ = Task.Delay(TimeSpan.FromSeconds(5), token)
					.ContinueWith(_ => recording.Signal("foo"), token);

				async Task Act() =>
					await That(recording).Should().BeSignaled().Within(TimeSpan.FromMilliseconds(40));

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
				ICallbackRecording recording = Record.Callback();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ => recording.Signal());

				async Task Act() =>
					await That(recording).Should().BeSignaled().Within(TimeSpan.FromSeconds(10));

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredWithParameterWithinTheGivenTimeout_ShouldSucceed()
			{
				ICallbackRecording<string> recording = Record.Callback<string>();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ => recording.Signal("foo"));

				async Task Act() =>
					await That(recording).Should().BeSignaled().Within(TimeSpan.FromSeconds(10));

				await That(Act).Should().NotThrow();
			}
		}
	}
}
