using System.Threading;
using aweXpect.Recording;
using Record = aweXpect.Recording.Record;

namespace aweXpect.Tests.Recordings;

public sealed partial class CallbackRecordingShould
{
	public sealed partial class NotTrigger
	{
		public sealed class TimesTests
		{
			[Fact]
			public async Task WhenNotTriggeredOftenEnough_ShouldSucceed()
			{
				ICallbackRecording recording = Record.Callback();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(TimeSpan.FromMilliseconds(50));
				CancellationToken token = cts.Token;

				recording.Trigger();

				async Task Act() =>
					await That(recording).Should().NotTrigger(2.Times()).WithCancellation(token);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenNotTriggeredWithParameter_ShouldSucceeded()
			{
				ICallbackRecording<int> recording = Record.Callback<int>();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(TimeSpan.FromMilliseconds(50));
				CancellationToken token = cts.Token;

				recording.Trigger(1);
				recording.Trigger(2);

				async Task Act() =>
					await That(recording).Should().NotTrigger(3.Times()).WithCancellation(token);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredMoreOften_ShouldFail()
			{
				ICallbackRecording recording = Record.Callback();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ =>
					{
						recording.Trigger();
						recording.Trigger();
						recording.Trigger();
					});

				async Task Act() =>
					await That(recording).Should().NotTrigger(2.Times());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             not have recorded the callback at least 2 times,
					             but it was recorded 3 times
					             """);
			}

			[Fact]
			public async Task WhenTriggeredOftenEnough_ShouldFail()
			{
				ICallbackRecording recording = Record.Callback();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ =>
					{
						recording.Trigger();
						recording.Trigger();
					});

				async Task Act() =>
					await That(recording).Should().NotTrigger(2.Times());

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
				ICallbackRecording<int> recording = Record.Callback<int>();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ =>
					{
						recording.Trigger(1);
						recording.Trigger(2);
						recording.Trigger(3);
						recording.Trigger(4);
					});

				async Task Act() =>
					await That(recording).Should().NotTrigger(3.Times());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             not have recorded the callback at least 3 times,
					             but it was recorded 4 times with [
					               1,
					               2,
					               3,
					               4
					             ]
					             """);
			}

			[Fact]
			public async Task WhenTriggeredWithParameterOftenEnough_ShouldFail()
			{
				ICallbackRecording<int> recording = Record.Callback<int>();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ =>
					{
						recording.Trigger(1);
						recording.Trigger(2);
						recording.Trigger(3);
					});

				async Task Act() =>
					await That(recording).Should().NotTrigger(3.Times());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             not have recorded the callback at least 3 times,
					             but it was recorded 3 times with [
					               1,
					               2,
					               3
					             ]
					             """);
			}
		}
	}
}
