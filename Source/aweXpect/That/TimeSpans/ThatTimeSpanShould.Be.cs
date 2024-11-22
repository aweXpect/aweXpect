using System;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatTimeSpanShould
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeSpan, IThat<TimeSpan>> Be(
		this IThat<TimeSpan> source,
		TimeSpan? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeSpan, IThat<TimeSpan>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeConstraint(it, expected, tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeSpan, IThat<TimeSpan>> NotBe(
		this IThat<TimeSpan> source,
		TimeSpan? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeSpan, IThat<TimeSpan>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NotBeConstraint(it, unexpected, tolerance)),
			source,
			tolerance);
	}

	private readonly struct BeConstraint(string it, TimeSpan? expected, TimeTolerance tolerance)
		: IValueConstraint<TimeSpan>
	{
		public ConstraintResult IsMetBy(TimeSpan actual)
		{
			if (expected is not null &&
			    IsWithinTolerance(tolerance.Tolerance, actual - expected.Value))
			{
				return new ConstraintResult.Success<TimeSpan>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"be {Formatter.Format(expected)}{tolerance}";
	}

	private readonly struct NotBeConstraint(
		string it,
		TimeSpan? unexpected,
		TimeTolerance tolerance)
		: IValueConstraint<TimeSpan>
	{
		public ConstraintResult IsMetBy(TimeSpan actual)
		{
			if (unexpected is null ||
			    !IsWithinTolerance(tolerance.Tolerance, actual - unexpected.Value))
			{
				return new ConstraintResult.Success<TimeSpan>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"not be {Formatter.Format(unexpected)}{tolerance}";
	}
}
