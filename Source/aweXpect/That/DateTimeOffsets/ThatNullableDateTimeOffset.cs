using System;
using aweXpect.Core.Constraints;
using aweXpect.Customization;
using aweXpect.Options;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="DateTimeOffset" />? values.
/// </summary>
public static partial class ThatNullableDateTimeOffset
{
	private static bool IsWithinTolerance(TimeSpan? tolerance, TimeSpan? difference)
	{
		tolerance ??= Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get();

		return difference <= tolerance.Value &&
		       difference >= tolerance.Value.Negate();
	}

	private readonly struct ConditionConstraint(
		string it,
		DateTimeOffset? expected,
		string expectation,
		Func<DateTimeOffset?, DateTimeOffset?, TimeSpan, bool> condition,
		Func<DateTimeOffset?, DateTimeOffset?, string, string> failureMessageFactory,
		TimeTolerance tolerance) : IValueConstraint<DateTimeOffset?>
	{
		public ConstraintResult IsMetBy(DateTimeOffset? actual)
		{
			if (condition(actual, expected, tolerance.Tolerance
			                                ?? Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get()))
			{
				return new ConstraintResult.Success<DateTimeOffset?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				failureMessageFactory(actual, expected, it));
		}

		public override string ToString()
			=> expectation + tolerance;
	}
}
