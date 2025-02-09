using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableTimeSpan
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>> IsEqualTo(
		this IThat<TimeSpan?> source,
		TimeSpan? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new IsEqualToConstraint(it, expected, tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>> IsNotEqualTo(
		this IThat<TimeSpan?> source,
		TimeSpan? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new IsNotEqualToConstraint(it, unexpected, tolerance)),
			source,
			tolerance);
	}

	private readonly struct IsEqualToConstraint(string it, TimeSpan? expected, TimeTolerance tolerance)
		: IValueConstraint<TimeSpan?>
	{
		public ConstraintResult IsMetBy(TimeSpan? actual)
		{
			if (IsWithinTolerance(tolerance.Tolerance, actual - expected))
			{
				return new ConstraintResult.Success<TimeSpan?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"is {Formatter.Format(expected)}{tolerance}";
	}

	private readonly struct IsNotEqualToConstraint(
		string it,
		TimeSpan? unexpected,
		TimeTolerance tolerance)
		: IValueConstraint<TimeSpan?>
	{
		public ConstraintResult IsMetBy(TimeSpan? actual)
		{
			if (!IsWithinTolerance(tolerance.Tolerance, actual - unexpected))
			{
				return new ConstraintResult.Success<TimeSpan?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"is not {Formatter.Format(unexpected)}{tolerance}";
	}
}
