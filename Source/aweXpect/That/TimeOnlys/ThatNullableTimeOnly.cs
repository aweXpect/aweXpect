﻿#if NET8_0_OR_GREATER
using System;
using aweXpect.Core.Constraints;
using aweXpect.Customization;
using aweXpect.Options;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="TimeOnly" />? values.
/// </summary>
public static partial class ThatNullableTimeOnly
{
	private readonly struct ConditionConstraintWithTolerance(
		string it,
		TimeOnly? expected,
		Func<TimeOnly?, TimeTolerance, string> expectation,
		Func<TimeOnly?, TimeOnly?, TimeSpan, bool> condition,
		Func<TimeOnly?, TimeOnly?, string, string> failureMessageFactory,
		TimeTolerance tolerance)
		: IValueConstraint<TimeOnly?>
	{
		public ConstraintResult IsMetBy(TimeOnly? actual)
		{
			if (condition(actual, expected, tolerance.Tolerance
			                                ?? Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get()))
			{
				return new ConstraintResult.Success<TimeOnly?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				failureMessageFactory(actual, expected, it));
		}

		public override string ToString()
			=> expectation(expected, tolerance);
	}
}
#endif
