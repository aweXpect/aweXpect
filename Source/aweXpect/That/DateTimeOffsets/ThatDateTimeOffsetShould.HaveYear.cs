using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatDateTimeOffsetShould
{
	/// <summary>
	///     Verifies that the year of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset, IThat<DateTimeOffset>> HaveYear(
		this IThat<DateTimeOffset> source,
		int? expected)
	{
		return new AndOrResult<DateTimeOffset, IThat<DateTimeOffset>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					expected,
					(a, e) => a.Year == e,
					$"have year of {Formatter.Format(expected)}")),
			source);
	}

	/// <summary>
	///     Verifies that the year of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset, IThat<DateTimeOffset>> NotHaveYear(
		this IThat<DateTimeOffset> source,
		int? unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => a.Year != e,
					$"not have year of {Formatter.Format(unexpected)}")),
			source);
}
