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
		await That(Customize.aweXpect.Settings().DefaultSignalerTimeout.Get()).Should().Be(30000.Milliseconds());
		using (IDisposable __ = Customize.aweXpect.Settings().DefaultSignalerTimeout.Set(10.Milliseconds()))
		{
			_ = Task.Delay(1000.Milliseconds()).ContinueWith(_ => signaler.Signal());
			SignalerResult result = signaler.Wait();
			await That(result.IsSuccess).Should().BeFalse();
			await That(Customize.aweXpect.Settings().DefaultSignalerTimeout.Get()).Should().Be(10.Milliseconds());
		}

		{
			_ = Task.Delay(200.Milliseconds()).ContinueWith(_ => signaler.Signal());
			SignalerResult result = signaler.Wait();
			await That(result.IsSuccess).Should().BeTrue();
			await That(Customize.aweXpect.Settings().DefaultSignalerTimeout.Get()).Should().Be(30000.Milliseconds());
		}
	}

	[Fact]
	public async Task DefaultTimeComparisonTimeout_ShouldBeUsedInTimeComparisons()
	{
		DateTime time = DateTime.UtcNow;
		DateTime otherTime = time.AddMilliseconds(10);
		await That(Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get()).Should().Be(TimeSpan.Zero);
		async Task Act() => await That(time).Should().Be(otherTime);
		await That(Act).Does().ThrowException();
		using (IDisposable __ = Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Set(10.Milliseconds()))
		{
			await That(Act).Does().NotThrow();
		}

		await That(Act).Does().ThrowException();
	}
}
