using aweXpect.Customization;
using aweXpect.Options;
using FluentAssertions.Extensions;

namespace aweXpect.Internal.Tests.Options;

public class RepeatedCheckOptionsTests
{
	[Fact]
	public async Task DefaultInterval_ShouldBe100Milliseconds()
	{
		TimeSpan result = RepeatedCheckOptions.DefaultInterval;

		await That(result).IsEqualTo(100.Milliseconds());
	}

	[Fact]
	public async Task Interval_ShouldBeReadOnlyOnceFromCustomization()
	{
		RepeatedCheckOptions sut = new();
		ICheckInterval interval1, interval2;
		using (Customize.aweXpect.Settings().DefaultCheckInterval.Set(103.Milliseconds()))
		{
			interval1 = sut.Interval;
		}

		using (Customize.aweXpect.Settings().DefaultCheckInterval.Set(107.Milliseconds()))
		{
			interval2 = sut.Interval;
		}

		await That(interval1.NextCheckInterval()).IsEqualTo(103.Milliseconds());
		await That(interval1.NextCheckInterval()).IsEqualTo(interval2.NextCheckInterval());
	}
}
