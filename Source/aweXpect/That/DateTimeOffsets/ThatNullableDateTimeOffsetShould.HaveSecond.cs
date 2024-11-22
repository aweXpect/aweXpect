using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableDateTimeOffsetShould
{
	/// <summary>
	///     Verifies that the second of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset?, IThat<DateTimeOffset?>> HaveSecond(
		this IThat<DateTimeOffset?> source,
		int? expected)
	{
		return new AndOrResult<DateTimeOffset?, IThat<DateTimeOffset?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					expected,
					(a, e) => a.HasValue && a.Value.Second == e,
					$"have second of {Formatter.Format(expected)}")),
			source);
	}

	/// <summary>
	///     Verifies that the second of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset?, IThat<DateTimeOffset?>> NotHaveSecond(
		this IThat<DateTimeOffset?> source,
		int? unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => !a.HasValue || a.Value.Second != e,
					$"not have second of {Formatter.Format(unexpected)}")),
			source);
}
