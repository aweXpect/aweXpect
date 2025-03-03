using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatTimeSpan
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeSpan, IThat<TimeSpan>> IsEqualTo(
		this IThat<TimeSpan> source,
		TimeSpan? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeSpan, IThat<TimeSpan>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsConstraint(it, expected, tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeSpan, IThat<TimeSpan>> IsNotEqualTo(
		this IThat<TimeSpan> source,
		TimeSpan? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeSpan, IThat<TimeSpan>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsNotConstraint(it, unexpected, tolerance)),
			source,
			tolerance);
	}

	private readonly struct IsConstraint(string it, TimeSpan? expected, TimeTolerance tolerance)
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
			=> $"is equal to {Formatter.Format(expected)}{tolerance}";
	}

	private readonly struct IsNotConstraint(
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
			=> $"is not equal to {Formatter.Format(unexpected)}{tolerance}";
	}
}
