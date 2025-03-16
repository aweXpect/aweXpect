using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateTime
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTime, IThat<DateTime>> IsEqualTo(
		this IThat<DateTime> source,
		DateTime? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTime, IThat<DateTime>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ConditionConstraint(
					it,
					grammars,
					expected,
					$"is equal to {Formatter.Format(expected)}{tolerance}",
					(a, e, t) => AreKindCompatible(a.Kind, e.Kind) && IsWithinTolerance(t, a - e),
					(a, e, i) => AreKindCompatible(a.Kind, e?.Kind)
						? $"{i} was {Formatter.Format(a)}"
						: $"{i} differed in the Kind property",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTime, IThat<DateTime>> IsNotEqualTo(
		this IThat<DateTime> source,
		DateTime? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTime, IThat<DateTime>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ConditionConstraint(
					it,
					grammars,
					unexpected,
					$"is not equal to {Formatter.Format(unexpected)}{tolerance}",
					(a, e, t) => !AreKindCompatible(a.Kind, e.Kind) || !IsWithinTolerance(t, a - e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	private static bool AreKindCompatible(DateTimeKind actualKind, DateTimeKind? expectedKind)
	{
		if (actualKind == DateTimeKind.Unspecified || expectedKind == DateTimeKind.Unspecified)
		{
			return true;
		}

		return actualKind == expectedKind;
	}
}
