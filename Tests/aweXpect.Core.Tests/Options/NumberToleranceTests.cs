using aweXpect.Options;

namespace aweXpect.Core.Tests.Options;

public class NumberToleranceTests
{
	[Fact]
	public async Task WhenToleranceIsNegative_ShouldThrowArgumentOutOfRangeException()
	{
		NumberTolerance<int> sut = new((_, _, _) => false);

		void Act() => sut.SetTolerance(-1);

		await That(Act).Throws<ArgumentOutOfRangeException>()
			.WithMessage("*Tolerance must be non-negative*").AsWildcard();
	}

	[Fact(Skip="Temporarily disable until next Core update")]
	public async Task WhenToleranceIsZero_ShouldNotThrow()
	{
		NumberTolerance<int> sut = new((_, _, _) => false);

		void Act() => sut.SetTolerance(0);

		await That(Act).DoesNotThrow();
		await That(sut.Tolerance).IsEqualTo(0);
	}
}
