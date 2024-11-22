using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableEnumShould
{
	/// <summary>
	///     Verifies that the subject has the <paramref name="expectedFlag" /> value.
	/// </summary>
	public static AndOrResult<TEnum?, IThat<TEnum?>> HaveFlag<TEnum>(
		this IThat<TEnum?> source,
		TEnum? expectedFlag)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					$"have flag {Formatter.Format(expectedFlag)}",
					actual => HasFlag(actual, expectedFlag))),
			source);

	/// <summary>
	///     Verifies that the subject does not have the <paramref name="unexpectedFlag" /> value.
	/// </summary>
	public static AndOrResult<TEnum?, IThat<TEnum?>> NotHaveFlag<TEnum>(
		this IThat<TEnum?> source,
		TEnum? unexpectedFlag)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					$"not have flag {Formatter.Format(unexpectedFlag)}",
					actual => !HasFlag(actual, unexpectedFlag))),
			source);

	private static bool HasFlag<TEnum>(TEnum? actual, TEnum? expectedFlag)
		where TEnum : struct, Enum
		=> (actual == null && expectedFlag == null) ||
		   (actual != null && expectedFlag != null &&
		    actual.Value.HasFlag(expectedFlag));
}
