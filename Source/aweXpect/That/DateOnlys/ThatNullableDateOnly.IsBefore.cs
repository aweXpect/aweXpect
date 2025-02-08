﻿#if NET8_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateOnly
{
	/// <summary>
	///     Verifies that the subject is before the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateOnly?, IThat<DateOnly?>> IsBefore(
		this IThat<DateOnly?> source,
		DateOnly? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateOnly?, IThat<DateOnly?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new ConditionConstraintWithTolerance(
					it,
					expected,
					(e, t) => $"is before {Formatter.Format(e)}{t.ToDayString()}",
					(a, e, t) => a?.AddDays(-1 * (int)t.TotalDays) < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not before the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateOnly?, IThat<DateOnly?>> IsNotBefore(
		this IThat<DateOnly?> source,
		DateOnly? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateOnly?, IThat<DateOnly?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new ConditionConstraintWithTolerance(
					it,
					unexpected,
					(u, t) => $"is not before {Formatter.Format(u)}{t.ToDayString()}",
					(a, e, t) => a?.AddDays((int)t.TotalDays) >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
#endif
