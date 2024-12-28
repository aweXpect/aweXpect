using System.Threading;
using aweXpect.Recording;
using Record = aweXpect.Recording.Record;

namespace aweXpect.Tests.Recordings;

public sealed partial class CallbackRecordingShould
{
	public sealed partial class Trigger
	{
		public sealed class TimesTests
		{
			[Fact]
			public async Task WhenNotTriggeredOftenEnough_ShouldFail()
			{
				ICallbackRecording recording = Record.Callback();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(TimeSpan.FromMilliseconds(50));
				CancellationToken token = cts.Token;

				recording.Trigger();

				async Task Act() =>
					await That(recording).Should().Trigger(2.Times()).WithCancellation(token);

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
				ICallbackRecording<int> recording = Record.Callback<int>();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(TimeSpan.FromMilliseconds(50));
				CancellationToken token = cts.Token;

				recording.Trigger(1);
				recording.Trigger(2);

				async Task Act() =>
					await That(recording).Should().Trigger(3.Times()).WithCancellation(token);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected recording to
					             have recorded the callback at least 3 times,
					             but it was only recorded 2 times with [
					               1,
					               2
					             ]
					             """);
			}

			[Fact]
			public async Task WhenTriggeredMoreOften_ShouldSucceed()
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
					await That(recording).Should().Trigger(2.Times());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredOftenEnough_ShouldSucceed()
			{
				ICallbackRecording recording = Record.Callback();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ =>
					{
						recording.Trigger();
						recording.Trigger();
					});

				async Task Act() =>
					await That(recording).Should().Trigger(2.Times());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredWithParameterMoreOften_ShouldSucceed()
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
					await That(recording).Should().Trigger(3.Times());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredWithParameterOftenEnough_ShouldSucceed()
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
					await That(recording).Should().Trigger(3.Times());

				await That(Act).Should().NotThrow();
			}
		}
	}
}
