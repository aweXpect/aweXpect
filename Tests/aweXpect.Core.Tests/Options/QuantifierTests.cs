using aweXpect.Options;

namespace aweXpect.Core.Tests.Options;

public class QuantifierTests
{
	[Theory]
	[InlineData(-1, true)]
	[InlineData(0, false)]
	[InlineData(1, false)]
	public async Task AtLeast_WhenMinimumIsNegative_ShouldThrowArgumentOutOfRangeException(
		int minimum, bool expectThrow)
	{
		Quantifier sut = new();

		void Act() => sut.AtLeast(minimum);

		await That(Act).Throws<ArgumentOutOfRangeException>().OnlyIf(expectThrow)
			.WithMessage("*The parameter 'minimum' must be non-negative*").AsWildcard();
	}

	[Theory]
	[InlineData(-1, true)]
	[InlineData(0, false)]
	[InlineData(1, false)]
	public async Task AtMost_WhenMaximumIsNegative_ShouldThrowArgumentOutOfRangeException(
		int maximum, bool expectThrow)
	{
		Quantifier sut = new();

		void Act() => sut.AtMost(maximum);

		await That(Act).Throws<ArgumentOutOfRangeException>().OnlyIf(expectThrow)
			.WithMessage("*The parameter 'maximum' must be non-negative*").AsWildcard();
	}

	[Theory]
	[InlineData(2, 1, true)]
	[InlineData(1, 1, false)]
	[InlineData(1, 2, false)]
	public async Task Between_WhenMaximumIsLessThanMinimum_ShouldThrowArgumentException(
		int minimum, int maximum, bool expectThrow)
	{
		Quantifier sut = new();

		void Act() => sut.Between(minimum, maximum);

		await That(Act).Throws<ArgumentException>().OnlyIf(expectThrow)
			.WithMessage("*The parameter 'maximum' must be greater than or equal to 'minimum'*").AsWildcard();
	}

	[Theory]
	[InlineData(-1, true)]
	[InlineData(0, false)]
	[InlineData(1, false)]
	public async Task Between_WhenMaximumIsNegative_ShouldThrowArgumentOutOfRangeException(
		int maximum, bool expectThrow)
	{
		Quantifier sut = new();

		void Act() => sut.Between(0, maximum);

		await That(Act).Throws<ArgumentOutOfRangeException>().OnlyIf(expectThrow)
			.WithMessage("*The parameter 'maximum' must be non-negative*").AsWildcard();
	}

	[Theory]
	[InlineData(-1, true)]
	[InlineData(0, false)]
	[InlineData(1, false)]
	public async Task Between_WhenMinimumIsNegative_ShouldThrowArgumentOutOfRangeException(
		int minimum, bool expectThrow)
	{
		Quantifier sut = new();

		void Act() => sut.Between(minimum, 1);

		await That(Act).Throws<ArgumentOutOfRangeException>().OnlyIf(expectThrow)
			.WithMessage("*The parameter 'minimum' must be non-negative*").AsWildcard();
	}

	[Theory]
	[InlineData(-1, true)]
	[InlineData(0, false)]
	[InlineData(1, false)]
	public async Task Exactly_WhenExpectedIsNegative_ShouldThrowArgumentOutOfRangeException(
		int expected, bool expectThrow)
	{
		Quantifier sut = new();

		void Act() => sut.Exactly(expected);

		await That(Act).Throws<ArgumentOutOfRangeException>().OnlyIf(expectThrow)
			.WithMessage("*The parameter 'expected' must be non-negative*").AsWildcard();
	}
}
