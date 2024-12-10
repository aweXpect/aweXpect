﻿using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTimeShould
{
	/// <summary>
	///     Verifies that the day of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTime?, IThat<DateTime?>> HaveDay(this IThat<DateTime?> source,
		int? expected)
	{
		return new AndOrResult<DateTime?, IThat<DateTime?>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new PropertyConstraint<int?>(
						it,
						expected,
						(a, e) => a.HasValue && a.Value.Day == e,
						$"have day of {Formatter.Format(expected)}")),
			source);
	}

	/// <summary>
	///     Verifies that the day of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateTime?, IThat<DateTime?>> NotHaveDay(
		this IThat<DateTime?> source,
		int? unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => !a.HasValue || a.Value.Day != e,
					$"not have day of {Formatter.Format(unexpected)}")),
			source);
}