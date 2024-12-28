﻿using System.Threading;
using aweXpect.Recording;
// ReSharper disable MethodHasAsyncOverload

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
				ISignalCounter recording = new SignalCounter();
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
				ISignalCounter<string> recording = new SignalCounter<string>();
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
				ISignalCounter recording = new SignalCounter();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ => recording.Signal());

				async Task Act() =>
					await That(recording).Should().BeSignaled().Within(TimeSpan.FromSeconds(10));

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredWithParameterWithinTheGivenTimeout_ShouldSucceed()
			{
				ISignalCounter<string> recording = new SignalCounter<string>();

				_ = Task.Delay(TimeSpan.FromMilliseconds(10))
					.ContinueWith(_ => recording.Signal("foo"));

				async Task Act() =>
					await That(recording).Should().BeSignaled().Within(TimeSpan.FromSeconds(10));

				await That(Act).Should().NotThrow();
			}
		}
	}
}
