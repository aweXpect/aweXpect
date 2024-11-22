using System;
using Testably.Expectations.Core;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableDateTimeOffsetShould
{
	/// <summary>
	///     Verifies that the subject is before the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>> BeBefore(
		this IThat<DateTimeOffset?> source,
		DateTimeOffset? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new ConditionConstraint(
					it,
					expected,
					$"be before {Formatter.Format(expected)}",
					(a, e, t) => a - t < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not before the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>> NotBeBefore(
		this IThat<DateTimeOffset?> source,
		DateTimeOffset? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new ConditionConstraint(
					it,
					unexpected,
					$"not be before {Formatter.Format(unexpected)}",
					(a, e, t) => a + t >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
