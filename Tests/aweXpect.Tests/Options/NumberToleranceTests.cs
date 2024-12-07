using aweXpect.Options;

namespace aweXpect.Tests.Options;

public class NumberToleranceTests
{
	[Fact]
	public async Task WhenToleranceIsNegative_ShouldThrowArgumentOutOfRangeException()
	{
		NumberTolerance<int> sut = new((_, _, _) => false);

		void Act() => sut.SetTolerance(-1);

		await That(Act).Should().Throw<ArgumentOutOfRangeException>()
			.WithMessage("*Tolerance must be non-negative*").AsWildcard();
	}

	[Fact]
	public async Task WhenToleranceIsZero_ShouldNotThrow()
	{
		NumberTolerance<int> sut = new((_, _, _) => false);

		void Act() => sut.SetTolerance(0);

		await That(Act).Should().NotThrow();
		await That(sut.Tolerance).Should().Be(0);
	}
}
