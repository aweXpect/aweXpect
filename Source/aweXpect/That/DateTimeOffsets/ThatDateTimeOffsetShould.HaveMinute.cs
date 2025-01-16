﻿using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateTimeOffsetShould
{
	/// <summary>
	///     Verifies that the minute of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset, IThatShould<DateTimeOffset>> HaveMinute(
		this IThatShould<DateTimeOffset> source,
		int? expected)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					expected,
					(a, e) => a.Minute == e,
					$"have minute of {Formatter.Format(expected)}")),
			source);

	/// <summary>
	///     Verifies that the minute of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateTimeOffset, IThatShould<DateTimeOffset>> NotHaveMinute(
		this IThatShould<DateTimeOffset> source,
		int? unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => a.Minute != e,
					$"not have minute of {Formatter.Format(unexpected)}")),
			source);
}
