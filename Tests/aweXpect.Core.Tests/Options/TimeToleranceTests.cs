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

		await That(Act).Does().Throw<ArgumentOutOfRangeException>()
			.WithMessage("*Tolerance must be non-negative*").AsWildcard();
	}

	[Fact]
	public async Task WhenToleranceIsZero_ShouldNotThrow()
	{
		TimeTolerance sut = new();

		void Act() => sut.SetTolerance(TimeSpan.Zero);

		await That(Act).Does().NotThrow();
		await That(sut.Tolerance).Is(TimeSpan.Zero);
	}
}
