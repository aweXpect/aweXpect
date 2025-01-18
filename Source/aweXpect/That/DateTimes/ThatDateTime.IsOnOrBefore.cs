using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateTime
{
	/// <summary>
	///     Verifies that the subject is on or before the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTime, IExpectSubject<DateTime>> IsOnOrBefore(
		this IExpectSubject<DateTime> source,
		DateTime? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTime, IExpectSubject<DateTime>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ConditionConstraint(
					it,
					expected,
					$"be on or before {Formatter.Format(expected)}",
					(a, e, t) => a - t <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not on or before the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTime, IExpectSubject<DateTime>> IsNotOnOrBefore(
		this IExpectSubject<DateTime> source,
		DateTime? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTime, IExpectSubject<DateTime>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ConditionConstraint(
					it,
					unexpected,
					$"not be on or before {Formatter.Format(unexpected)}",
					(a, e, t) => a + t > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
