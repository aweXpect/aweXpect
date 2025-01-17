using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTimeOffset
{
	/// <summary>
	///     Verifies that the month of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset?, IExpectSubject<DateTimeOffset?>> HasMonth(
		this IExpectSubject<DateTimeOffset?> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<int?>(
					it,
					expected,
					(a, e) => a.HasValue && a.Value.Month == e,
					$"have month of {Formatter.Format(expected)}")),
			source);

	/// <summary>
	///     Verifies that the month of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset?, IExpectSubject<DateTimeOffset?>> DoesNotHaveMonth(
		this IExpectSubject<DateTimeOffset?> source,
		int? unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => !a.HasValue || a.Value.Month != e,
					$"not have month of {Formatter.Format(unexpected)}")),
			source);
}
