using System;
using Testably.Expectations.Core;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableDateTimeShould
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTime?, IThat<DateTime?>> Be(
		this IThat<DateTime?> source,
		DateTime? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTime?, IThat<DateTime?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new ConditionConstraint(
					it,
					expected,
					$"be {Formatter.Format(expected)}{tolerance}",
					(a, e, t) => a?.Kind == e?.Kind && IsWithinTolerance(t, a - e),
					(a, e, i) => a?.Kind == e?.Kind
						? $"{i} was {Formatter.Format(a)}"
						: $"{i} differed in the Kind property",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTime?, IThat<DateTime?>> NotBe(
		this IThat<DateTime?> source,
		DateTime? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTime?, IThat<DateTime?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new ConditionConstraint(
					it,
					unexpected,
					$"not be {Formatter.Format(unexpected)}{tolerance}",
					(a, e, t) => a?.Kind != e?.Kind || !IsWithinTolerance(t, a - e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
