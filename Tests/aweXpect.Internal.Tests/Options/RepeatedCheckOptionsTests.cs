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
}
