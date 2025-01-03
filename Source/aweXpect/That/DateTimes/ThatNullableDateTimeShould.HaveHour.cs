﻿using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTimeShould
{
	/// <summary>
	///     Verifies that the hour of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTime?, IThat<DateTime?>> HaveHour(this IThat<DateTime?> source,
		int? expected)
		=> new(source.ExpectationBuilder.AddConstraint(
				it
					=> new PropertyConstraint<int?>(
						it,
						expected,
						(a, e) => a.HasValue && a.Value.Hour == e,
						$"have hour of {Formatter.Format(expected)}")),
			source);

	/// <summary>
	///     Verifies that the hour of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateTime?, IThat<DateTime?>> NotHaveHour(
		this IThat<DateTime?> source,
		int? unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => !a.HasValue || a.Value.Hour != e,
					$"not have hour of {Formatter.Format(unexpected)}")),
			source);
}
