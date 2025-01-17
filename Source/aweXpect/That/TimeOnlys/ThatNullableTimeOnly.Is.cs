﻿#if NET8_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableTimeOnly
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeOnly?, IExpectSubject<TimeOnly?>> Is(this IExpectSubject<TimeOnly?> source,
		TimeOnly? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeOnly?, IExpectSubject<TimeOnly?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ConditionConstraintWithTolerance(
					it,
					expected,
					(e, t) => $"be {Formatter.Format(e)}{t}",
					(a, e, t) => (a == null && e == null) ||
					             (a != null && e != null &&
					              Math.Abs(a.Value.Ticks - e.Value.Ticks) <= t.Ticks),
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeOnly?, IExpectSubject<TimeOnly?>> IsNot(
		this IExpectSubject<TimeOnly?> source,
		TimeOnly? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeOnly?, IExpectSubject<TimeOnly?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ConditionConstraintWithTolerance(
					it,
					unexpected,
					(e, t) => $"not be {Formatter.Format(e)}{t}",
					(a, u, t) => a == null != (u == null) ||
					             (a != null && u != null &&
					              Math.Abs(a.Value.Ticks - u.Value.Ticks) > t.Ticks),
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
#endif