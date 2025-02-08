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
	public static AndOrResult<TEnum?, IThat<TEnum?>> HasValue<TEnum>(
		this IThat<TEnum?> source,
		long? expected)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new ValueConstraint<TEnum>(
					it,
					$"has value {Formatter.Format(expected)}",
					actual => actual != null &&
					          Convert.ToInt64(actual, CultureInfo.InvariantCulture)
					          == expected)),
			source);

	/// <summary>
	///     Verifies that the subject does not have the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<TEnum?, IThat<TEnum?>> DoesNotHaveValue<TEnum>(
		this IThat<TEnum?> source,
		long? unexpected)
		where TEnum : struct, Enum
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new ValueConstraint<TEnum>(
					it,
					$"does not have value {Formatter.Format(unexpected)}",
					actual => actual != null &&
					          Convert.ToInt64(actual.Value, CultureInfo.InvariantCulture) !=
					          unexpected)),
			source);
}
