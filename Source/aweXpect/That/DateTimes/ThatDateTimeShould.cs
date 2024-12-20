﻿using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="DateTime" /> values.
/// </summary>
public static partial class ThatDateTimeShould
{
	/// <summary>
	///     Start expectations for current <see cref="DateTime" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<DateTime> Should(this IExpectSubject<DateTime> subject)
		=> subject.Should(That.WithoutAction);

	private static bool IsWithinTolerance(TimeSpan? tolerance, TimeSpan difference)
	{
		if (tolerance == null)
		{
			return difference == TimeSpan.Zero;
		}

		return difference <= tolerance.Value &&
		       difference >= tolerance.Value.Negate();
	}

	private readonly struct PropertyConstraint<T>(
		string it,
		T expected,
		Func<DateTime, T, bool> condition,
		string expectation) : IValueConstraint<DateTime>
	{
		public ConstraintResult IsMetBy(DateTime actual)
		{
			if (condition(actual, expected))
			{
				return new ConstraintResult.Success<DateTime>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> expectation;
	}

	private readonly struct ConditionConstraint(
		string it,
		DateTime? expected,
		string expectation,
		Func<DateTime, DateTime, TimeSpan, bool> condition,
		Func<DateTime, DateTime?, string, string> failureMessageFactory,
		TimeTolerance tolerance) : IValueConstraint<DateTime>
	{
		public ConstraintResult IsMetBy(DateTime actual)
		{
			if (expected is null)
			{
				return new ConstraintResult.Failure(ToString(),
					failureMessageFactory(actual, expected, it));
			}

			if (condition(actual, expected.Value, tolerance.Tolerance ?? TimeSpan.Zero))
			{
				return new ConstraintResult.Success<DateTime>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				failureMessageFactory(actual, expected.Value, it));
		}

		public override string ToString()
			=> expectation + tolerance;
	}
}
