using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateTime
{
	/// <summary>
	///     Verifies that the minute of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTime, IExpectSubject<DateTime>> HasMinute(this IExpectSubject<DateTime> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<int?>(
					it,
					expected,
					(a, e) => a.Minute == e,
					$"have minute of {Formatter.Format(expected)}")),
			source);

	/// <summary>
	///     Verifies that the minute of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateTime, IExpectSubject<DateTime>> DoesNotHaveMinute(
		this IExpectSubject<DateTime> source,
		int? unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => a.Minute != e,
					$"not have minute of {Formatter.Format(unexpected)}")),
			source);
}
