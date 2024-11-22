using System;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableTimeSpanShould
{
	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>> BeGreaterThanOrEqualTo(
		this IThat<TimeSpan?> source,
		TimeSpan? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeGreaterThanOrEqualToConstraint(it, expected, tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not greater than or equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>> NotBeGreaterThanOrEqualTo(
		this IThat<TimeSpan?> source,
		TimeSpan? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeSpan?, IThat<TimeSpan?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NotBeGreaterThanOrEqualToConstraint(it, unexpected, tolerance)),
			source,
			tolerance);
	}

	private readonly struct BeGreaterThanOrEqualToConstraint(
		string it,
		TimeSpan? expected,
		TimeTolerance tolerance)
		: IValueConstraint<TimeSpan?>
	{
		public ConstraintResult IsMetBy(TimeSpan? actual)
		{
			if (actual + (tolerance.Tolerance ?? TimeSpan.Zero) >= expected)
			{
				return new ConstraintResult.Success<TimeSpan?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"be greater than or equal to {Formatter.Format(expected)}{tolerance}";
	}

	private readonly struct NotBeGreaterThanOrEqualToConstraint(
		string it,
		TimeSpan? unexpected,
		TimeTolerance tolerance)
		: IValueConstraint<TimeSpan?>
	{
		public ConstraintResult IsMetBy(TimeSpan? actual)
		{
			if (actual - (tolerance.Tolerance ?? TimeSpan.Zero) < unexpected)
			{
				return new ConstraintResult.Success<TimeSpan?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"not be greater than or equal to {Formatter.Format(unexpected)}{tolerance}";
	}
}
