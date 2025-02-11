using System.Diagnostics;
using System.Threading;
using aweXpect.Chronology;
using aweXpect.Customization;
using aweXpect.Signaling;

namespace aweXpect.Core.Tests.Customization;

public sealed class CustomizeSettingsTests
{
	[Fact]
	public async Task DefaultSignalerTimeout_ShouldBeUsedInSignaler()
	{
		Signaler signaler = new();
		await That(Customize.aweXpect.Settings().DefaultSignalerTimeout.Get()).IsEqualTo(30000.Milliseconds());
		using (IDisposable __ = Customize.aweXpect.Settings().DefaultSignalerTimeout.Set(10.Milliseconds()))
		{
			_ = Task.Delay(1000.Milliseconds()).ContinueWith(_ => signaler.Signal());
			SignalerResult result = signaler.Wait();
			await That(result.IsSuccess).IsFalse();
			await That(Customize.aweXpect.Settings().DefaultSignalerTimeout.Get()).IsEqualTo(10.Milliseconds());
		}

		{
			_ = Task.Delay(LowTimeout).ContinueWith(_ => signaler.Signal());
			SignalerResult result = signaler.Wait();
			await That(result.IsSuccess).IsTrue();
			await That(Customize.aweXpect.Settings().DefaultSignalerTimeout.Get()).IsEqualTo(30000.Milliseconds());
		}
	}

	[Fact]
	public async Task DefaultTimeComparisonTimeout_ShouldBeUsedInTimeComparisons()
	{
		DateTime time = DateTime.UtcNow;
		DateTime otherTime = time.AddMilliseconds(10);
		await That(Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get()).IsEqualTo(TimeSpan.Zero);
		async Task Act() => await That(time).IsEqualTo(otherTime);
		await That(Act).ThrowsException();
		using (IDisposable __ = Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Set(10.Milliseconds()))
		{
			await That(Act).DoesNotThrow();
		}

		await That(Act).ThrowsException();
	}

	[Fact(Skip="Temporarily disable until next Core update")]
	public async Task TestCancellation_FromCancellationToken_ShouldBeApplied()
	{
		Stopwatch stopwatch = new();
		CancellationToken cancelledToken = new(true);
		using (IDisposable __ = Customize.aweXpect.Settings().TestCancellation
			       .Set(TestCancellation.FromCancellationToken(() => cancelledToken)))
		{
			stopwatch.Start();
			await That(cancellationToken => Task.Delay(5.Seconds(), cancellationToken))
				.Throws<TaskCanceledException>();
			stopwatch.Stop();
		}

		await That(stopwatch.Elapsed).IsLessThan(500.Milliseconds());
	}

	[Fact(Skip="Temporarily disable until next Core update")]
	public async Task TestCancellation_FromTimeout_ShouldBeApplied()
	{
		Stopwatch stopwatch = new();
		using (IDisposable __ = Customize.aweXpect.Settings().TestCancellation
			       .Set(TestCancellation.FromTimeout(LowTimeout)))
		{
			stopwatch.Start();
			await That(cancellationToken => Task.Delay(5.Seconds(), cancellationToken))
				.Throws<TaskCanceledException>();
			stopwatch.Stop();
		}

		await That(stopwatch.Elapsed).IsLessThanOrEqualTo(1.Seconds());
		await That(stopwatch.Elapsed).IsGreaterThanOrEqualTo(LowTimeout).Within(50.Milliseconds());
	}

	[Fact(Skip="Temporarily disable until next Core update")]
	public async Task TestCancellation_None_ShouldBeApplied()
	{
		Stopwatch stopwatch = new();
		CancellationToken cancelledToken = new(true);
		using (IDisposable _ = Customize.aweXpect.Settings().TestCancellation
			       .Set(TestCancellation.FromCancellationToken(() => cancelledToken)))
		{
			using (IDisposable __ = Customize.aweXpect.Settings().TestCancellation
				       .Set(TestCancellation.None()))
			{
				stopwatch.Start();
				await That(cancellationToken => Task.Delay(LowTimeout, cancellationToken))
					.DoesNotThrow();
				stopwatch.Stop();
			}
		}

		await That(stopwatch.Elapsed).IsGreaterThanOrEqualTo(LowTimeout).Within(50.Milliseconds());
	}

	[Fact(Skip="Temporarily disable until next Core update")]
	public async Task TestCancellation_PerDefault_ShouldNotCancel()
	{
		Stopwatch stopwatch = new();
		stopwatch.Start();
		await That(cancellationToken => Task.Delay(LowTimeout, cancellationToken)).DoesNotThrow();
		stopwatch.Stop();

		await That(stopwatch.Elapsed).IsGreaterThanOrEqualTo(LowTimeout).Within(50.Milliseconds());
	}

	[Fact(Skip="Temporarily disable until next Core update")]
	public async Task WithCancellation_OverwritesTheCancellationToken()
	{
		TimeSpan delay = 5.Seconds();
		Stopwatch stopwatch = new();
		using CancellationTokenSource cts = new(3.Seconds());
		CancellationToken cancelledToken = new(true);
		using (IDisposable _ = Customize.aweXpect.Settings().TestCancellation
			       .Set(TestCancellation.FromCancellationToken(() => cts.Token)))
		{
			stopwatch.Start();
			await That(cancellationToken => Task.Delay(delay, cancellationToken))
				.Throws<TaskCanceledException>()
				.WithCancellation(cancelledToken);
			stopwatch.Stop();
		}

		await That(stopwatch.Elapsed).IsLessThanOrEqualTo(1.Seconds());
	}

	/// <summary>
	///     The minimal timeout for tests, that have to await this long.
	/// </summary>
	/// <remarks>
	///     It should be as low as possible to have fast tests.
	/// </remarks>
	private TimeSpan LowTimeout { get; } = TimeSpan.FromMilliseconds(100);
}
