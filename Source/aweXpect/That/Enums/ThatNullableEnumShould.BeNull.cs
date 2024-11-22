using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableEnumShould
{
	/// <summary>
	///     Verifies that the subject is <see langword="null" />.
	/// </summary>
	public static AndOrResult<TEnum?, IThat<TEnum?>> BeNull<TEnum>(
		this IThat<TEnum?> source)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					"be null",
					actual => actual == null)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" />.
	/// </summary>
	public static AndOrResult<TEnum?, IThat<TEnum?>> NotBeNull<TEnum>(
		this IThat<TEnum?> source)
		where TEnum : struct, Enum
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint<TEnum>(
					it,
					"not be null",
					actual => actual != null)),
			source);
}
