using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Customization;
using aweXpect.Helpers;
using aweXpect.Options;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="DateTimeOffset" /> values.
/// </summary>
public static partial class ThatDateTimeOffsetShould
{
	/// <summary>
	///     Start expectations for current <see cref="DateTimeOffset" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<DateTimeOffset> Should(this IExpectSubject<DateTimeOffset> subject)
		=> subject.Should(That.WithoutAction);

	private static bool IsWithinTolerance(TimeSpan? tolerance, TimeSpan difference)
	{
		if (tolerance == null)
		{
			return difference == Customize.aweXpect.Settings().DefaultTimeComparisonTimeout.Get();
		}

		return difference <= tolerance.Value &&
		       difference >= tolerance.Value.Negate();
	}

	private readonly struct PropertyConstraint<T>(
		string it,
		T expected,
		Func<DateTimeOffset, T, bool> condition,
		string expectation) : IValueConstraint<DateTimeOffset>
	{
		public ConstraintResult IsMetBy(DateTimeOffset actual)
		{
			if (condition(actual, expected))
			{
				return new ConstraintResult.Success<DateTimeOffset>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> expectation;
	}

	private readonly struct ConditionConstraint(
		string it,
		DateTimeOffset? expected,
		string expectation,
		Func<DateTimeOffset, DateTimeOffset, TimeSpan, bool> condition,
		Func<DateTimeOffset, DateTimeOffset?, string, string> failureMessageFactory,
		TimeTolerance tolerance) : IValueConstraint<DateTimeOffset>
	{
		public ConstraintResult IsMetBy(DateTimeOffset actual)
		{
			if (expected is null)
			{
				return new ConstraintResult.Failure(ToString(),
					failureMessageFactory(actual, expected, it));
			}

			if (condition(actual, expected.Value, tolerance.Tolerance
			                                      ?? Customize.aweXpect.Settings().DefaultTimeComparisonTimeout.Get()))
			{
				return new ConstraintResult.Success<DateTimeOffset>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				failureMessageFactory(actual, expected.Value, it));
		}

		public override string ToString()
			=> expectation + tolerance;
	}
}
