using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTime
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTime?, IExpectSubject<DateTime?>> Is(
		this IExpectSubject<DateTime?> source,
		DateTime? expected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTime?, IExpectSubject<DateTime?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ConditionConstraint(
					it,
					expected,
					$"be {Formatter.Format(expected)}{tolerance}",
					(a, e, t) => AreKindCompatible(a?.Kind, e?.Kind) && IsWithinTolerance(t, a - e),
					(a, e, i) => AreKindCompatible(a?.Kind, e?.Kind)
						? $"{i} was {Formatter.Format(a)}"
						: $"{i} differed in the Kind property",
					tolerance)),
			source,
			tolerance);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static TimeToleranceResult<DateTime?, IExpectSubject<DateTime?>> IsNot(
		this IExpectSubject<DateTime?> source,
		DateTime? unexpected)
	{
		TimeTolerance tolerance = new();
		return new TimeToleranceResult<DateTime?, IExpectSubject<DateTime?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ConditionConstraint(
					it,
					unexpected,
					$"not be {Formatter.Format(unexpected)}{tolerance}",
					(a, e, t) => !AreKindCompatible(a?.Kind, e?.Kind) || !IsWithinTolerance(t, a - e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}",
					tolerance)),
			source,
			tolerance);
	}

	private static bool AreKindCompatible(DateTimeKind? actualKind, DateTimeKind? expectedKind)
	{
		if (actualKind == DateTimeKind.Unspecified || expectedKind == DateTimeKind.Unspecified)
		{
			return true;
		}

		return actualKind == expectedKind;
	}
}
