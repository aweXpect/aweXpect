﻿using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Customization;
using aweXpect.Options;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="DateTimeOffset" /> values.
/// </summary>
public static partial class ThatDateTimeOffset
{
	private static bool IsWithinTolerance(TimeSpan? tolerance, TimeSpan difference)
	{
		tolerance ??= Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get();

		return difference <= tolerance.Value &&
		       difference >= tolerance.Value.Negate();
	}

	private sealed class ConditionConstraint(
		string it,
		ExpectationGrammars grammars,
		DateTimeOffset? expected,
		string expectation,
		Func<DateTimeOffset, DateTimeOffset, TimeSpan, bool> condition,
		Func<DateTimeOffset, DateTimeOffset?, string, string> failureMessageFactory,
		TimeTolerance tolerance)
		: ConstraintResult.WithValue<DateTimeOffset>(grammars),
			IValueConstraint<DateTimeOffset>
	{
		public ConstraintResult IsMetBy(DateTimeOffset actual)
		{
			Actual = actual;
			Outcome = expected != null && condition(actual, expected.Value, tolerance.Tolerance
			                                                                ?? Customize.aweXpect.Settings()
				                                                                .DefaultTimeComparisonTolerance.Get())
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(expectation).Append(tolerance);

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(failureMessageFactory(Actual, expected, it));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();
	}
}
