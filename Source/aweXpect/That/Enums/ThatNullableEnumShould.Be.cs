using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableEnumShould
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TEnum?, IThat<TEnum?>> Be<TEnum>(this IThat<TEnum?> source,
		TEnum? expected)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					$"be {Formatter.Format(expected)}",
					actual => actual?.Equals(expected) ?? expected == null)),
			source);

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<TEnum?, IThat<TEnum?>> NotBe<TEnum>(
		this IThat<TEnum?> source,
		TEnum? unexpected)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					$"not be {Formatter.Format(unexpected)}",
					actual => !actual?.Equals(unexpected) ?? unexpected != null)),
			source);
}
