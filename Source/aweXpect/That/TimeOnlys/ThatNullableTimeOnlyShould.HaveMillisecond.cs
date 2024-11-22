﻿#if NET6_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableTimeOnlyShould
{
	/// <summary>
	///     Verifies that the millisecond of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TimeOnly?, IThat<TimeOnly?>> HaveMillisecond(
		this IThat<TimeOnly?> source,
		int? expected)
	{
		return new AndOrResult<TimeOnly?, IThat<TimeOnly?>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new PropertyConstraint<int?>(
						it,
						expected,
						(a, e) => a.HasValue && a.Value.Millisecond == e,
						$"have millisecond of {Formatter.Format(expected)}")),
			source);
	}

	/// <summary>
	///     Verifies that the millisecond of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<TimeOnly?, IThat<TimeOnly?>> NotHaveMillisecond(
		this IThat<TimeOnly?> source,
		int? unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => !a.HasValue || a.Value.Millisecond != e,
					$"not have millisecond of {Formatter.Format(unexpected)}")),
			source);
}
#endif
