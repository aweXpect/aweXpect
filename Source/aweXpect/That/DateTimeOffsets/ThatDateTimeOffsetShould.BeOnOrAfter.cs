using System;
using Testably.Expectations.Core;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatDateTimeOffsetShould
{
	/// <summary>
	///     Verifies that the subject is on or after the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTimeOffset, IThat<DateTimeOffset>> BeOnOrAfter(
		this IThat<DateTimeOffset> source,
		DateTimeOffset? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTimeOffset, IThat<DateTimeOffset>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new ConditionConstraint(
					it,
					expected,
					$"be on or after {Formatter.Format(expected)}",
					(a, e, t) => a + t >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not on or after the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTimeOffset, IThat<DateTimeOffset>> NotBeOnOrAfter(
		this IThat<DateTimeOffset> source,
		DateTimeOffset? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTimeOffset, IThat<DateTimeOffset>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new ConditionConstraint(
					it,
					unexpected,
					$"not be on or after {Formatter.Format(unexpected)}",
					(a, e, t) => a - t < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
