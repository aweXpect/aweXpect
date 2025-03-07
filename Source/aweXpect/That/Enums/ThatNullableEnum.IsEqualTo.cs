﻿using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableEnum
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TEnum?, IThat<TEnum?>> IsEqualTo<TEnum>(this IThat<TEnum?> source,
		TEnum? expected)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new ValueConstraint<TEnum>(
					it,
					$"is {Formatter.Format(expected)}",
					actual => actual?.Equals(expected) ?? expected == null)),
			source);

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<TEnum?, IThat<TEnum?>> IsNotEqualTo<TEnum>(
		this IThat<TEnum?> source,
		TEnum? unexpected)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new ValueConstraint<TEnum>(
					it,
					$"is not {Formatter.Format(unexpected)}",
					actual => !actual?.Equals(unexpected) ?? unexpected != null)),
			source);
}
