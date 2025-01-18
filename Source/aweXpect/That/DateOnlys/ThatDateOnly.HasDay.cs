#if NET8_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateOnly
{
	/// <summary>
	///     Verifies that the day of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateOnly, IExpectSubject<DateOnly>> HasDay(this IExpectSubject<DateOnly> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<int?>(
					it,
					expected,
					(a, e) => a.Day == e,
					$"have day of {Formatter.Format(expected)}")),
			source);

	/// <summary>
	///     Verifies that the day of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateOnly, IExpectSubject<DateOnly>> DoesNotHaveDay(
		this IExpectSubject<DateOnly> source,
		int? unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => a.Day != e,
					$"not have day of {Formatter.Format(unexpected)}")),
			source);
}
#endif
