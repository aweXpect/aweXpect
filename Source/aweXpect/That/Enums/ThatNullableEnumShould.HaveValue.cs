using System;
using System.Globalization;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableEnumShould
{
	/// <summary>
	///     Verifies that the subject has the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TEnum?, IThat<TEnum?>> HaveValue<TEnum>(
		this IThat<TEnum?> source,
		long? expected)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					$"have value {Formatter.Format(expected)}",
					actual => actual != null &&
					          Convert.ToInt64(actual, CultureInfo.InvariantCulture)
					          == expected)),
			source);

	/// <summary>
	///     Verifies that the subject does not have the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<TEnum?, IThat<TEnum?>> NotHaveValue<TEnum>(
		this IThat<TEnum?> source,
		long? unexpected)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					$"not have value {Formatter.Format(unexpected)}",
					actual => actual != null &&
					          Convert.ToInt64(actual.Value, CultureInfo.InvariantCulture) !=
					          unexpected)),
			source);
}
