using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateTimeOffset
{
	/// <summary>
	///     Verifies that the minute of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset, IExpectSubject<DateTimeOffset>> HasMinute(
		this IExpectSubject<DateTimeOffset> source,
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
	public static AndOrResult<DateTimeOffset, IExpectSubject<DateTimeOffset>> DoesNotHaveMinute(
		this IExpectSubject<DateTimeOffset> source,
		int? unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => a.Minute != e,
					$"not have minute of {Formatter.Format(unexpected)}")),
			source);
}
