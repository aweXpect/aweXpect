using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnum
{
	/// <summary>
	///     Verifies that the subject is defined inside the <typeparamref name="TEnum" />.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> IsDefined<TEnum>(
		this IThat<TEnum> source)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint<TEnum>(
					it,
					"be defined",
					actual => Enum.IsDefined(typeof(TEnum), actual))),
			source);

	/// <summary>
	///     Verifies that the subject is not defined inside the <typeparamref name="TEnum" />.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> IsNotDefined<TEnum>(
		this IThat<TEnum> source)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint<TEnum>(
					it,
					"not be defined",
					actual => !Enum.IsDefined(typeof(TEnum), actual))),
			source);
}
