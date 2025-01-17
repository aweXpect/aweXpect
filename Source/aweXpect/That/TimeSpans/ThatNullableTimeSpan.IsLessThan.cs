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
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeSpan?, IExpectSubject<TimeSpan?>> IsLessThan(
		this IExpectSubject<TimeSpan?> source,
		TimeSpan? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeSpan?, IExpectSubject<TimeSpan?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new BeLessThanConstraint(it, expected, tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not less than the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeSpan?, IExpectSubject<TimeSpan?>> IsNotLessThan(
		this IExpectSubject<TimeSpan?> source,
		TimeSpan? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeSpan?, IExpectSubject<TimeSpan?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NotBeLessThanConstraint(it, unexpected, tolerance)),
			source,
			tolerance);
	}

	private readonly struct BeLessThanConstraint(
		string it,
		TimeSpan? expected,
		TimeTolerance tolerance)
		: IValueConstraint<TimeSpan?>
	{
		public ConstraintResult IsMetBy(TimeSpan? actual)
		{
			TimeSpan timeTolerance = tolerance.Tolerance
			                         ?? Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get();
			if (actual - timeTolerance < expected)
			{
				return new ConstraintResult.Success<TimeSpan?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"be less than {Formatter.Format(expected)}{tolerance}";
	}

	private readonly struct NotBeLessThanConstraint(
		string it,
		TimeSpan? unexpected,
		TimeTolerance tolerance)
		: IValueConstraint<TimeSpan?>
	{
		public ConstraintResult IsMetBy(TimeSpan? actual)
		{
			TimeSpan timeTolerance = tolerance.Tolerance
			                         ?? Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get();
			if (actual + timeTolerance >= unexpected)
			{
				return new ConstraintResult.Success<TimeSpan?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"not be less than {Formatter.Format(unexpected)}{tolerance}";
	}
}
