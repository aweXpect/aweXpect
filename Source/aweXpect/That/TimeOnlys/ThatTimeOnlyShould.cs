﻿#if NET8_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="TimeOnly" /> values.
/// </summary>
public static partial class ThatTimeOnlyShould
{
	/// <summary>
	///     Start expectations for current <see cref="TimeOnly" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<TimeOnly> Should(this IExpectSubject<TimeOnly> subject)
		=> subject.Should(That.WithoutAction);

	private readonly struct PropertyConstraint<T>(
		string it,
		T expected,
		Func<TimeOnly, T, bool> condition,
		string expectation) : IValueConstraint<TimeOnly>
	{
		public ConstraintResult IsMetBy(TimeOnly actual)
		{
			if (condition(actual, expected))
			{
				return new ConstraintResult.Success<TimeOnly>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> expectation;
	}

	private readonly struct ConditionConstraintWithTolerance(
		string it,
		TimeOnly? expected,
		Func<TimeOnly?, TimeTolerance, string> expectation,
		Func<TimeOnly, TimeOnly?, TimeSpan, bool> condition,
		Func<TimeOnly, TimeOnly?, string, string> failureMessageFactory,
		TimeTolerance tolerance)
		: IValueConstraint<TimeOnly>
	{
		public ConstraintResult IsMetBy(TimeOnly actual)
		{
			if (condition(actual, expected, tolerance.Tolerance ?? TimeSpan.Zero))
			{
				return new ConstraintResult.Success<TimeOnly>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				failureMessageFactory(actual, expected, it));
		}

		public override string ToString()
			=> expectation(expected, tolerance);
	}
}
#endif
