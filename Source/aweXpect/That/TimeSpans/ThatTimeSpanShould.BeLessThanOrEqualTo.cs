using System;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatTimeSpanShould
{
	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeSpan, IThat<TimeSpan>> BeLessThanOrEqualTo(
		this IThat<TimeSpan> source,
		TimeSpan? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeSpan, IThat<TimeSpan>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeLessThanOrEqualToConstraint(it, expected, tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not less than or equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeSpan, IThat<TimeSpan>> NotBeLessThanOrEqualTo(
		this IThat<TimeSpan> source,
		TimeSpan? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeSpan, IThat<TimeSpan>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NotBeLessThanOrEqualToConstraint(it, unexpected, tolerance)),
			source,
			tolerance);
	}

	private readonly struct BeLessThanOrEqualToConstraint(
		string it,
		TimeSpan? expected,
		TimeTolerance tolerance)
		: IValueConstraint<TimeSpan>
	{
		public ConstraintResult IsMetBy(TimeSpan actual)
		{
			if (actual - (tolerance.Tolerance ?? TimeSpan.Zero) <= expected)
			{
				return new ConstraintResult.Success<TimeSpan>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"be less than or equal to {Formatter.Format(expected)}{tolerance}";
	}

	private readonly struct NotBeLessThanOrEqualToConstraint(
		string it,
		TimeSpan? unexpected,
		TimeTolerance tolerance)
		: IValueConstraint<TimeSpan>
	{
		public ConstraintResult IsMetBy(TimeSpan actual)
		{
			if (actual + (tolerance.Tolerance ?? TimeSpan.Zero) > unexpected)
			{
				return new ConstraintResult.Success<TimeSpan>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"not be less than or equal to {Formatter.Format(unexpected)}{tolerance}";
	}
}
