using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnum
{
	/// <summary>
	///     Verifies that the subject has the <paramref name="expectedFlag" /> value.
	/// </summary>
	public static AndOrResult<TEnum, IExpectSubject<TEnum>> HasFlag<TEnum>(
		this IExpectSubject<TEnum> source,
		TEnum? expectedFlag)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint<TEnum>(
					it,
					$"have flag {Formatter.Format(expectedFlag)}",
					actual => expectedFlag != null && actual.HasFlag(expectedFlag))),
			source);

	/// <summary>
	///     Verifies that the subject does not have the <paramref name="unexpectedFlag" /> value.
	/// </summary>
	public static AndOrResult<TEnum, IExpectSubject<TEnum>> DoesNotHaveFlag<TEnum>(
		this IExpectSubject<TEnum> source,
		TEnum? unexpectedFlag)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint<TEnum>(
					it,
					$"not have flag {Formatter.Format(unexpectedFlag)}",
					actual => unexpectedFlag == null || !actual.HasFlag(unexpectedFlag))),
			source);
}
