﻿#if NET8_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateOnly
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateOnly, IThat<DateOnly>> IsEqualTo(this IThat<DateOnly> source,
		DateOnly? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateOnly, IThat<DateOnly>>(source.ThatIs().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new ConditionConstraintWithTolerance(
						it,
						grammars,
						expected,
						(e, t) => $"is equal to {Formatter.Format(e)}{t.ToDayString()}",
						(a, e, t) => e != null &&
						             Math.Abs(a.DayNumber - e.Value.DayNumber) <= (int)t.TotalDays,
						(a, _, i) => $"{i} was {Formatter.Format(a)}",
						tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateOnly, IThat<DateOnly>> IsNotEqualTo(
		this IThat<DateOnly> source,
		DateOnly? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateOnly, IThat<DateOnly>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ConditionConstraintWithTolerance(
					it,
					grammars,
					unexpected,
					(e, t) => $"is not equal to {Formatter.Format(e)}{t.ToDayString()}",
					(a, u, t) => u == null ||
					             Math.Abs(a.DayNumber - u.Value.DayNumber) > (int)t.TotalDays,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
#endif
