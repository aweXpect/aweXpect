using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableEnum
{
	/// <summary>
	///     Verifies that the subject is <see langword="null" />.
	/// </summary>
	public static AndOrResult<TEnum?, IExpectSubject<TEnum?>> IsNull<TEnum>(
		this IExpectSubject<TEnum?> source)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint<TEnum>(
					it,
					"be null",
					actual => actual == null)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" />.
	/// </summary>
	public static AndOrResult<TEnum?, IExpectSubject<TEnum?>> IsNotNull<TEnum>(
		this IExpectSubject<TEnum?> source)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint<TEnum>(
					it,
					"not be null",
					actual => actual != null)),
			source);
}
