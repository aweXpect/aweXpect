using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTimeOffsetShould
{
	/// <summary>
	///     Verifies that the day of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset?, IThat<DateTimeOffset?>> HaveDay(
		this IThat<DateTimeOffset?> source,
		int? expected)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					expected,
					(a, e) => a.HasValue && a.Value.Day == e,
					$"have day of {Formatter.Format(expected)}")),
			source);

	/// <summary>
	///     Verifies that the day of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset?, IThat<DateTimeOffset?>> NotHaveDay(
		this IThat<DateTimeOffset?> source,
		int? unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => !a.HasValue || a.Value.Day != e,
					$"not have day of {Formatter.Format(unexpected)}")),
			source);
}
