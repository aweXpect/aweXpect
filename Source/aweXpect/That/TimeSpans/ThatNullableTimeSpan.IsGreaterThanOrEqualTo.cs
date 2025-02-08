using System;
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
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>> IsGreaterThanOrEqualTo(
		this IThat<TimeSpan?> source,
		TimeSpan? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new IsGreaterThanOrEqualToConstraint(it, expected, tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not greater than or equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>> IsNotGreaterThanOrEqualTo(
		this IThat<TimeSpan?> source,
		TimeSpan? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new IsNotGreaterThanOrEqualToConstraint(it, unexpected, tolerance)),
			source,
			tolerance);
	}

	private readonly struct IsGreaterThanOrEqualToConstraint(
		string it,
		TimeSpan? expected,
		TimeTolerance tolerance)
		: IValueConstraint<TimeSpan?>
	{
		public ConstraintResult IsMetBy(TimeSpan? actual)
		{
			TimeSpan timeTolerance = tolerance.Tolerance
			                         ?? Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get();
			if (actual + timeTolerance >= expected)
			{
				return new ConstraintResult.Success<TimeSpan?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"is greater than or equal to {Formatter.Format(expected)}{tolerance}";
	}

	private readonly struct IsNotGreaterThanOrEqualToConstraint(
		string it,
		TimeSpan? unexpected,
		TimeTolerance tolerance)
		: IValueConstraint<TimeSpan?>
	{
		public ConstraintResult IsMetBy(TimeSpan? actual)
		{
			TimeSpan timeTolerance = tolerance.Tolerance
			                         ?? Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get();
			if (actual - timeTolerance < unexpected)
			{
				return new ConstraintResult.Success<TimeSpan?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"is not greater than or equal to {Formatter.Format(unexpected)}{tolerance}";
	}
}
