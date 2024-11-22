#if NET6_0_OR_GREATER
using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableDateOnlyShould
{
	/// <summary>
	///     Verifies that the month of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateOnly?, IThat<DateOnly?>> HaveMonth(this IThat<DateOnly?> source,
		int? expected)
	{
		return new AndOrResult<DateOnly?, IThat<DateOnly?>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new PropertyConstraint<int?>(
						it,
						expected,
						(a, e) => a.HasValue && a.Value.Month == e,
						$"have month of {Formatter.Format(expected)}")),
			source);
	}

	/// <summary>
	///     Verifies that the month of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateOnly?, IThat<DateOnly?>> NotHaveMonth(
		this IThat<DateOnly?> source,
		int? unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => !a.HasValue || a.Value.Month != e,
					$"not have month of {Formatter.Format(unexpected)}")),
			source);
}
#endif
