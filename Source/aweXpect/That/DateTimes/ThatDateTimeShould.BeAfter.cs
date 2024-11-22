using System;
using Testably.Expectations.Core;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatDateTimeShould
{
	/// <summary>
	///     Verifies that the subject is after the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTime, IThat<DateTime>> BeAfter(
		this IThat<DateTime> source,
		DateTime? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTime, IThat<DateTime>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new ConditionConstraint(
					it,
					expected,
					$"be after {Formatter.Format(expected)}",
					(a, e, t) => a + t > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not after the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTime, IThat<DateTime>> NotBeAfter(
		this IThat<DateTime> source,
		DateTime? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTime, IThat<DateTime>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new ConditionConstraint(
					it,
					unexpected,
					$"not be after {Formatter.Format(unexpected)}",
					(a, e, t) => a - t <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
