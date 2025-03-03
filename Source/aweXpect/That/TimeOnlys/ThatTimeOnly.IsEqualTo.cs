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
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeOnly, IThat<TimeOnly>> IsEqualTo(this IThat<TimeOnly> source,
		TimeOnly? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeOnly, IThat<TimeOnly>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ConditionConstraintWithTolerance(
					it,
					expected,
					(e, t) => $"is equal to {Formatter.Format(e)}{t}",
					(a, e, t) => e != null &&
					             Math.Abs(a.Ticks - e.Value.Ticks) <= t.Ticks,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeOnly, IThat<TimeOnly>> IsNotEqualTo(
		this IThat<TimeOnly> source,
		TimeOnly? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeOnly, IThat<TimeOnly>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ConditionConstraintWithTolerance(
					it,
					unexpected,
					(e, t) => $"is not equal to {Formatter.Format(e)}{t}",
					(a, u, t) => u == null ||
					             Math.Abs(a.Ticks - u.Value.Ticks) > t.Ticks,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
#endif
