using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatDateTimeOffsetShould
{
	/// <summary>
	///     Verifies that the offset of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset, IThat<DateTimeOffset>> HaveOffset(
		this IThat<DateTimeOffset> source,
		TimeSpan expected)
	{
		return new AndOrResult<DateTimeOffset, IThat<DateTimeOffset>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<TimeSpan>(
					it,
					expected,
					(a, e) => a.Offset == e,
					$"have offset of {Formatter.Format(expected)}")),
			source);
	}

	/// <summary>
	///     Verifies that the offset of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset, IThat<DateTimeOffset>> NotHaveOffset(
		this IThat<DateTimeOffset> source,
		TimeSpan unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<TimeSpan>(
					it,
					unexpected,
					(a, e) => a.Offset != e,
					$"not have offset of {Formatter.Format(unexpected)}")),
			source);
}
