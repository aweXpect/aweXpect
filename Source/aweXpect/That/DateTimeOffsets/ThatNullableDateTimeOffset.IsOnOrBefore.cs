﻿using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTimeOffset
{
	/// <summary>
	///     Verifies that the subject is on or before the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>> IsOnOrBefore(
		this IThat<DateTimeOffset?> source,
		DateTimeOffset? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ConditionConstraint(
					it,
					grammars,
					expected,
					$"is on or before {Formatter.Format(expected)}",
					(a, e, t) => a - t <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not on or before the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>> IsNotOnOrBefore(
		this IThat<DateTimeOffset?> source,
		DateTimeOffset? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ConditionConstraint(
					it,
					grammars,
					unexpected,
					$"is not on or before {Formatter.Format(unexpected)}",
					(a, e, t) => a + t > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
