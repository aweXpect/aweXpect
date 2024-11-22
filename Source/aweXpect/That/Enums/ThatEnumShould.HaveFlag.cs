using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatEnumShould
{
	/// <summary>
	///     Verifies that the subject has the <paramref name="expectedFlag" /> value.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> HaveFlag<TEnum>(
		this IThat<TEnum> source,
		TEnum? expectedFlag)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					$"have flag {Formatter.Format(expectedFlag)}",
					actual => expectedFlag != null && actual.HasFlag(expectedFlag))),
			source);

	/// <summary>
	///     Verifies that the subject does not have the <paramref name="unexpectedFlag" /> value.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> NotHaveFlag<TEnum>(
		this IThat<TEnum> source,
		TEnum? unexpectedFlag)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					$"not have flag {Formatter.Format(unexpectedFlag)}",
					actual => unexpectedFlag == null || !actual.HasFlag(unexpectedFlag))),
			source);
}
