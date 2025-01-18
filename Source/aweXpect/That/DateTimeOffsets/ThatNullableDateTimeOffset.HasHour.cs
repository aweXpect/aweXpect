using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTimeOffset
{
	/// <summary>
	///     Verifies that the hour of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset?, IExpectSubject<DateTimeOffset?>> HasHour(
		this IExpectSubject<DateTimeOffset?> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<int?>(
					it,
					expected,
					(a, e) => a.HasValue && a.Value.Hour == e,
					$"have hour of {Formatter.Format(expected)}")),
			source);

	/// <summary>
	///     Verifies that the hour of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset?, IExpectSubject<DateTimeOffset?>> DoesNotHaveHour(
		this IExpectSubject<DateTimeOffset?> source,
		int? unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => !a.HasValue || a.Value.Hour != e,
					$"not have hour of {Formatter.Format(unexpected)}")),
			source);
}
