using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateTime
{
	/// <summary>
	///     Verifies that the day of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTime, IExpectSubject<DateTime>> HasDay(this IExpectSubject<DateTime> source,
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
	public static AndOrResult<DateTime, IExpectSubject<DateTime>> DoesNotHaveDay(
		this IExpectSubject<DateTime> source,
		int? unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => a.Day != e,
					$"not have day of {Formatter.Format(unexpected)}")),
			source);
}
