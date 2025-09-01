﻿using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Customization;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableTimeSpan
{
	/// <summary>
	///     Verifies that the subject is between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>>, TimeSpan?> IsBetween(
		this IThat<TimeSpan?> source,
		TimeSpan? minimum)
	{
		TimeTolerance tolerance = new();
		return new BetweenResult<TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>>, TimeSpan?>(maximum
			=> new TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>>(
				source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
					new IsBetweenConstraint(it, grammars, minimum, maximum, tolerance)),
				source,
				tolerance));
	}

	/// <summary>
	///     Verifies that the subject is not between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>>, TimeSpan?> IsNotBetween(
		this IThat<TimeSpan?> source,
		TimeSpan? minimum)
	{
		TimeTolerance tolerance = new();
		return new BetweenResult<TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>>, TimeSpan?>(maximum
			=> new TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>>(
				source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
					new IsBetweenConstraint(it, grammars, minimum, maximum, tolerance).Invert()),
				source,
				tolerance));
	}

	private sealed class IsBetweenConstraint(
		string it,
		ExpectationGrammars grammars,
		TimeSpan? minimum,
		TimeSpan? maximum,
		TimeTolerance tolerance)
		: ConstraintResult.WithNotNullValue<TimeSpan?>(it, grammars),
			IValueConstraint<TimeSpan?>
	{
		public ConstraintResult IsMetBy(TimeSpan? actual)
		{
			Actual = actual;
			if (actual is null && minimum is null && maximum is null)
			{
				Outcome = Outcome.Success;
			}
			else if (actual is null || minimum is null || maximum is null)
			{
				Outcome = IsNegated ? Outcome.Success : Outcome.Failure;
			}
			else
			{
				TimeSpan timeTolerance = tolerance.Tolerance
				                         ?? Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get();
				if (IsNegated)
				{
					timeTolerance = timeTolerance.Negate();
				}

				Outcome = actual.Value.Add(timeTolerance) >= minimum &&
				          actual.Value.Add(timeTolerance.Negate()) <= maximum
					? Outcome.Success
					: Outcome.Failure;
			}

			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is between ");
			Formatter.Format(stringBuilder, minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, maximum);
			stringBuilder.Append(tolerance);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not between ");
			Formatter.Format(stringBuilder, minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, maximum);
			stringBuilder.Append(tolerance);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
}
