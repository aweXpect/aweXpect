using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTimeOffset
{
	/// <summary>
	///     Verifies that the subject is after the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>> IsAfter(
		this IThat<DateTimeOffset?> source,
		DateTimeOffset? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new ConditionConstraint(
					it,
					expected,
					$"is after {Formatter.Format(expected)}",
					(a, e, t) => a + t > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not after the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>> IsNotAfter(
		this IThat<DateTimeOffset?> source,
		DateTimeOffset? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new ConditionConstraint(
					it,
					unexpected,
					$"is not after {Formatter.Format(unexpected)}",
					(a, e, t) => a - t <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
