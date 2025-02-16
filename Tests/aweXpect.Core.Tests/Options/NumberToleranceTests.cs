using aweXpect.Options;

namespace aweXpect.Core.Tests.Options;

public class NumberToleranceTests
{
	[Fact]
	public async Task IsWithinTolerance_WhenBothAreNull_ShouldReturnTrue()
	{
		NumberTolerance<int> sut = new((a, b, t) => Math.Abs(a - b) <= t);

		bool result = sut.IsWithinTolerance(null, null);

		await That(result).IsTrue();
	}

	[Theory]
	[InlineData(null, 1)]
	[InlineData(-3, null)]
	public async Task IsWithinTolerance_WhenOneIsNull_ShouldReturnFalse(int? actual, int? expected)
	{
		NumberTolerance<int> sut = new((a, b, t) => Math.Abs(a - b) <= t);

		bool result = sut.IsWithinTolerance(actual, expected);

		await That(result).IsFalse();
	}

	[Theory]
	[InlineData(1, 2, 1, true)]
	[InlineData(1, 3, 1, false)]
	public async Task IsWithinTolerance_WhenValuesAreNotNull_ShouldApplyTolerance(
		int actual, int expected, int tolerance, bool expectedResult)
	{
		NumberTolerance<int> sut = new((a, b, t) => Math.Abs(a - b) <= t);
		sut.SetTolerance(tolerance);

		bool result = sut.IsWithinTolerance(actual, expected);

		await That(result).IsEqualTo(expectedResult);
	}

	[Fact]
	public async Task WhenToleranceIsNegative_ShouldThrowArgumentOutOfRangeException()
	{
		NumberTolerance<int> sut = new((_, _, _) => false);

		void Act() => sut.SetTolerance(-1);

		await That(Act).Throws<ArgumentOutOfRangeException>()
			.WithMessage("*Tolerance must be non-negative*").AsWildcard();
	}

	[Fact]
	public async Task WhenToleranceIsZero_ShouldNotThrow()
	{
		NumberTolerance<int> sut = new((_, _, _) => false);

		void Act() => sut.SetTolerance(0);

		await That(Act).DoesNotThrow();
		await That(sut.Tolerance).IsEqualTo(0);
	}
}
