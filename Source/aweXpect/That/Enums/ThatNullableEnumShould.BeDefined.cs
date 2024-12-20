﻿using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableEnumShould
{
	/// <summary>
	///     Verifies that the subject is defined inside the <typeparamref name="TEnum" />.
	/// </summary>
	public static AndOrResult<TEnum?, IThat<TEnum?>> BeDefined<TEnum>(
		this IThat<TEnum?> source)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					"be defined",
					actual => actual != null && Enum.IsDefined(typeof(TEnum), actual.Value))),
			source);

	/// <summary>
	///     Verifies that the subject is not defined inside the <typeparamref name="TEnum" />.
	/// </summary>
	public static AndOrResult<TEnum?, IThat<TEnum?>> NotBeDefined<TEnum>(
		this IThat<TEnum?> source)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					"not be defined",
					actual => actual != null && !Enum.IsDefined(typeof(TEnum), actual.Value))),
			source);
}
