using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTime
{
	/// <summary>
	///     Verifies that the subject is before the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTime?, IExpectSubject<DateTime?>> IsBefore(
		this IExpectSubject<DateTime?> source,
		DateTime? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTime?, IExpectSubject<DateTime?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ConditionConstraint(
					it,
					expected,
					$"be before {Formatter.Format(expected)}",
					(a, e, t) => a - t < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not before the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTime?, IExpectSubject<DateTime?>> IsNotBefore(
		this IExpectSubject<DateTime?> source,
		DateTime? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTime?, IExpectSubject<DateTime?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ConditionConstraint(
					it,
					unexpected,
					$"not be before {Formatter.Format(unexpected)}",
					(a, e, t) => a + t >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
