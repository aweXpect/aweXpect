#if NET8_0_OR_GREATER
using System;
using aweXpect.Core.Constraints;
using aweXpect.Customization;
using aweXpect.Options;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="DateOnly" /> values.
/// </summary>
public static partial class ThatNullableDateOnly
{
	private readonly struct ConditionConstraintWithTolerance(
		string it,
		DateOnly? expected,
		Func<DateOnly?, TimeTolerance, string> expectation,
		Func<DateOnly?, DateOnly?, TimeSpan, bool> condition,
		Func<DateOnly?, DateOnly?, string, string> failureMessageFactory,
		TimeTolerance tolerance)
		: IValueConstraint<DateOnly?>
	{
		public ConstraintResult IsMetBy(DateOnly? actual)
		{
			if (condition(actual, expected, tolerance.Tolerance
			                                ?? Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get()))
			{
				return new ConstraintResult.Success<DateOnly?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				failureMessageFactory(actual, expected, it));
		}

		public override string ToString()
			=> expectation(expected, tolerance);
	}
}
#endif
