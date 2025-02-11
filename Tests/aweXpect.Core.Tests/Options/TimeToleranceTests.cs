using aweXpect.Chronology;
using aweXpect.Options;

namespace aweXpect.Core.Tests.Options;

public class TimeToleranceTests
{
	[Fact]
	public async Task WhenToleranceIsNegative_ShouldThrowArgumentOutOfRangeException()
	{
		TimeTolerance sut = new();

		void Act() => sut.SetTolerance(-1.Seconds());

		await That(Act).Throws<ArgumentOutOfRangeException>()
			.WithMessage("*Tolerance must be non-negative*").AsWildcard();
	}

	[Fact(Skip="Temporarily disable until next Core update")]
	public async Task WhenToleranceIsZero_ShouldNotThrow()
	{
		TimeTolerance sut = new();

		void Act() => sut.SetTolerance(TimeSpan.Zero);

		await That(Act).DoesNotThrow();
		await That(sut.Tolerance).IsEqualTo(TimeSpan.Zero);
	}
}
