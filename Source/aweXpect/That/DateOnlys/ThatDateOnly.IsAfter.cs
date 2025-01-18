#if NET8_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateOnly
{
	/// <summary>
	///     Verifies that the subject is after the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateOnly, IExpectSubject<DateOnly>> IsAfter(
		this IExpectSubject<DateOnly> source,
		DateOnly? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateOnly, IExpectSubject<DateOnly>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ConditionConstraintWithTolerance(
					it,
					expected,
					(e, t) => $"be after {Formatter.Format(e)}{t.ToDayString()}",
					(a, e, t) => a.AddDays((int)t.TotalDays) > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not after the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateOnly, IExpectSubject<DateOnly>> IsNotAfter(
		this IExpectSubject<DateOnly> source,
		DateOnly? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateOnly, IExpectSubject<DateOnly>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ConditionConstraintWithTolerance(
					it,
					unexpected,
					(u, t) => $"not be after {Formatter.Format(u)}{t.ToDayString()}",
					(a, e, t) => a.AddDays(-1 * (int)t.TotalDays) <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
#endif
