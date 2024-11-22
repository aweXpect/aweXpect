using System;
using Testably.Expectations.Core;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableDateTimeOffsetShould
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>> Be(
		this IThat<DateTimeOffset?> source,
		DateTimeOffset? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>>(source
				.ExpectationBuilder
				.AddConstraint(it => new ConditionConstraint(
					it,
					expected,
					$"be {Formatter.Format(expected)}{tolerance}",
					(a, e, t) => IsWithinTolerance(t, a - e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>> NotBe(
		this IThat<DateTimeOffset?> source,
		DateTimeOffset? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTimeOffset?, IThat<DateTimeOffset?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new ConditionConstraint(
					it,
					unexpected,
					$"not be {Formatter.Format(unexpected)}{tolerance}",
					(a, e, t) => !IsWithinTolerance(t, a - e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
