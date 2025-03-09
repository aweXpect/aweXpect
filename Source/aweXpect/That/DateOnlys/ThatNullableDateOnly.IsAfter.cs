#if NET8_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateOnly
{
	/// <summary>
	///     Verifies that the subject is after the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateOnly?, IThat<DateOnly?>> IsAfter(
		this IThat<DateOnly?> source,
		DateOnly? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateOnly?, IThat<DateOnly?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ConditionConstraintWithTolerance(
					it,
					grammars,
					expected,
					(e, t) => $"is after {Formatter.Format(e)}{t.ToDayString()}",
					(a, e, t) => a?.AddDays((int)t.TotalDays) > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not after the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateOnly?, IThat<DateOnly?>> IsNotAfter(
		this IThat<DateOnly?> source,
		DateOnly? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateOnly?, IThat<DateOnly?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ConditionConstraintWithTolerance(
					it,
					grammars,
					unexpected,
					(u, t) => $"is not after {Formatter.Format(u)}{t.ToDayString()}",
					(a, e, t) => a?.AddDays(-1 * (int)t.TotalDays) <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
#endif
