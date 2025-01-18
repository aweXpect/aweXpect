using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateTime
{
	/// <summary>
	///     Verifies that the kind of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTime, IExpectSubject<DateTime>> HasKind(this IExpectSubject<DateTime> source,
		DateTimeKind expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<DateTimeKind>(
					it,
					expected,
					(a, e) => a.Kind == e,
					$"have kind of {Formatter.Format(expected)}")),
			source);

	/// <summary>
	///     Verifies that the kind of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateTime, IExpectSubject<DateTime>> DoesNotHaveKind(
		this IExpectSubject<DateTime> source,
		DateTimeKind unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<DateTimeKind>(
					it,
					unexpected,
					(a, e) => a.Kind != e,
					$"not have kind of {Formatter.Format(unexpected)}")),
			source);
}
