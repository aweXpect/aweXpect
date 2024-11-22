using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatEnumShould
{
	/// <summary>
	///     Verifies that the subject is defined inside the <typeparamref name="TEnum" />.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> BeDefined<TEnum>(
		this IThat<TEnum> source)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					"be defined",
					actual => Enum.IsDefined(typeof(TEnum), actual))),
			source);

	/// <summary>
	///     Verifies that the subject is not defined inside the <typeparamref name="TEnum" />.
	/// </summary>
	public static AndOrResult<TEnum, IThat<TEnum>> NotBeDefined<TEnum>(
		this IThat<TEnum> source)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					"not be defined",
					actual => !Enum.IsDefined(typeof(TEnum), actual))),
			source);
}
