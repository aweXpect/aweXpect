#if NET8_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatTimeOnly
{
	/// <summary>
	///     Verifies that the subject is on or after the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeOnly, IThat<TimeOnly>> IsOnOrAfter(
		this IThat<TimeOnly> source,
		TimeOnly? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeOnly, IThat<TimeOnly>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ConditionConstraintWithTolerance(
					it,
					expected,
					(e, t) => $"is on or after {Formatter.Format(e)}{t}",
					(a, e, t) => a.Add(t) >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not on or after the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeOnly, IThat<TimeOnly>> IsNotOnOrAfter(
		this IThat<TimeOnly> source,
		TimeOnly? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeOnly, IThat<TimeOnly>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ConditionConstraintWithTolerance(
					it,
					unexpected,
					(u, t) => $"is not on or after {Formatter.Format(u)}{t}",
					(a, e, t) => a.Add(t.Negate()) < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
#endif
