﻿using System.Threading;
using aweXpect.Signaling;

namespace aweXpect.Tests.Signaling;

public sealed partial class SignalerShould
{
	public sealed partial class NotBeSignaled
	{
		public sealed class TimesTests
		{
			[Fact]
			public async Task WhenNotTriggeredOftenEnough_ShouldSucceed()
			{
				Signaler signaler = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
				CancellationToken token = cts.Token;

				signaler.Signal();

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled(2.Times()).WithCancellation(token);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenNotTriggeredWithParameter_ShouldSucceeded()
			{
				Signaler<int> signaler = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
				CancellationToken token = cts.Token;

				signaler.Signal(1);
				signaler.Signal(2);

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled(3.Times()).WithCancellation(token);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenTriggeredMoreOften_ShouldFail()
			{
				Signaler signaler = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ =>
					{
						signaler.Signal();
						signaler.Signal();
						signaler.Signal();
					});

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled(2.Times());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected signaler to
					             not have recorded the callback at least 2 times,
					             but it was recorded ? times
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenTriggeredOftenEnough_ShouldFail()
			{
				Signaler signaler = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ =>
					{
						signaler.Signal();
						signaler.Signal();
					});

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled(2.Times());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected signaler to
					             not have recorded the callback at least 2 times,
					             but it was recorded 2 times
					             """);
			}

			[Fact]
			public async Task WhenTriggeredWithParameterMoreOften_ShouldFail()
			{
				Signaler<int> signaler = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ =>
					{
						signaler.Signal(1);
						signaler.Signal(2);
						signaler.Signal(3);
						signaler.Signal(4);
					});

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled(3.Times());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected signaler to
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
				Signaler<int> signaler = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ =>
					{
						signaler.Signal(1);
						signaler.Signal(2);
						signaler.Signal(3);
					});

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled(3.Times());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected signaler to
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