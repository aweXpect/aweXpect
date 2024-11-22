using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatDateTimeOffsetShould
{
	/// <summary>
	///     Verifies that the hour of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset, IThat<DateTimeOffset>> HaveHour(
		this IThat<DateTimeOffset> source,
		int? expected)
	{
		return new AndOrResult<DateTimeOffset, IThat<DateTimeOffset>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					expected,
					(a, e) => a.Hour == e,
					$"have hour of {Formatter.Format(expected)}")),
			source);
	}

	/// <summary>
	///     Verifies that the hour of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset, IThat<DateTimeOffset>> NotHaveHour(
		this IThat<DateTimeOffset> source,
		int? unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => a.Hour != e,
					$"not have hour of {Formatter.Format(unexpected)}")),
			source);
}
