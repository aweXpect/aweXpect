using System;
using System.Globalization;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableEnum
{
	/// <summary>
	///     Verifies that the subject has the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TEnum?, IExpectSubject<TEnum?>> HasValue<TEnum>(
		this IExpectSubject<TEnum?> source,
		long? expected)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint<TEnum>(
					it,
					$"have value {Formatter.Format(expected)}",
					actual => actual != null &&
					          Convert.ToInt64(actual, CultureInfo.InvariantCulture)
					          == expected)),
			source);

	/// <summary>
	///     Verifies that the subject does not have the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<TEnum?, IExpectSubject<TEnum?>> DoesNotHaveValue<TEnum>(
		this IExpectSubject<TEnum?> source,
		long? unexpected)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint<TEnum>(
					it,
					$"not have value {Formatter.Format(unexpected)}",
					actual => actual != null &&
					          Convert.ToInt64(actual.Value, CultureInfo.InvariantCulture) !=
					          unexpected)),
			source);
}
