using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnum
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> Is<TEnum>(this IThat<TEnum> source,
		TEnum? expected)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint<TEnum>(
					it,
					$"be {Formatter.Format(expected)}",
					actual => actual.Equals(expected))),
			source);

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> IsNot<TEnum>(this IThat<TEnum> source,
		TEnum? unexpected)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint<TEnum>(
					it,
					$"not be {Formatter.Format(unexpected)}",
					actual => !actual.Equals(unexpected))),
			source);
}
