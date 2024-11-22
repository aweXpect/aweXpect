#if NET6_0_OR_GREATER
using System;
using Testably.Expectations.Core;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableTimeOnlyShould
{
	/// <summary>
	///     Verifies that the subject is after the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>> BeAfter(
		this IThat<TimeOnly?> source,
		TimeOnly? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new ConditionConstraintWithTolerance(
					it,
					expected,
					(e, t) => $"be after {Formatter.Format(e)}{t}",
					(a, e, t) => a?.Add(t) > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not after the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>> NotBeAfter(
		this IThat<TimeOnly?> source,
		TimeOnly? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<TimeOnly?, IThat<TimeOnly?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new ConditionConstraintWithTolerance(
					it,
					unexpected,
					(u, t) => $"not be after {Formatter.Format(u)}{t}",
					(a, e, t) => a?.Add(t.Negate()) <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}
}
#endif
