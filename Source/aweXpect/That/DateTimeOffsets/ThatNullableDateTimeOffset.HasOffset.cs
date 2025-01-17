using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTimeOffset
{
	/// <summary>
	///     Verifies that the offset of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset?, IExpectSubject<DateTimeOffset?>> HasOffset(
		this IExpectSubject<DateTimeOffset?> source,
		TimeSpan expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<TimeSpan>(
					it,
					expected,
					(a, e) => a.HasValue && a.Value.Offset == e,
					$"have offset of {Formatter.Format(expected)}")),
			source);

	/// <summary>
	///     Verifies that the offset of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset?, IExpectSubject<DateTimeOffset?>> DoesNotHaveOffset(
		this IExpectSubject<DateTimeOffset?> source,
		TimeSpan unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<TimeSpan>(
					it,
					unexpected,
					(a, e) => !a.HasValue || a.Value.Offset != e,
					$"not have offset of {Formatter.Format(unexpected)}")),
			source);
}
