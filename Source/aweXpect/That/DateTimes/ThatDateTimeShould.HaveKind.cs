using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateTimeShould
{
	/// <summary>
	///     Verifies that the kind of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTime, IThatShould<DateTime>> HaveKind(this IThatShould<DateTime> source,
		DateTimeKind expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<DateTimeKind>(
					it,
					expected,
					(a, e) => a.Kind == e,
					$"have kind of {Formatter.Format(expected)}")),
			source);

	/// <summary>
	///     Verifies that the kind of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateTime, IThatShould<DateTime>> NotHaveKind(
		this IThatShould<DateTime> source,
		DateTimeKind unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<DateTimeKind>(
					it,
					unexpected,
					(a, e) => a.Kind != e,
					$"not have kind of {Formatter.Format(unexpected)}")),
			source);
}
