﻿using System;
using System.Globalization;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumShould
{
	/// <summary>
	///     Verifies that the subject has the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> HaveValue<TEnum>(
		this IThat<TEnum> source,
		long? expected)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					$"have value {Formatter.Format(expected)}",
					actual => Convert.ToInt64(actual, CultureInfo.InvariantCulture) == expected)),
			source);

	/// <summary>
	///     Verifies that the subject does not have the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> NotHaveValue<TEnum>(
		this IThat<TEnum> source,
		long? unexpected)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					$"not have value {Formatter.Format(unexpected)}",
					actual => Convert.ToInt64(actual, CultureInfo.InvariantCulture) != unexpected)),
			source);
}