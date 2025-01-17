using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTime
{
	/// <summary>
	///     Verifies that the year of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTime?, IExpectSubject<DateTime?>> HasYear(this IExpectSubject<DateTime?> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<int?>(
						it,
						expected,
						(a, e) => a.HasValue && a.Value.Year == e,
						$"have year of {Formatter.Format(expected)}")),
			source);

	/// <summary>
	///     Verifies that the year of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateTime?, IExpectSubject<DateTime?>> DoesNotHaveYear(
		this IExpectSubject<DateTime?> source,
		int? unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => !a.HasValue || a.Value.Year != e,
					$"not have year of {Formatter.Format(unexpected)}")),
			source);
}
