#if NET6_0_OR_GREATER
using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableDateOnlyShould
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateOnly?, IThat<DateOnly?>> Be(this IThat<DateOnly?> source,
		DateOnly? expected)
	{
		return new AndOrResult<DateOnly?, IThat<DateOnly?>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new ConditionConstraint(
						it,
						expected,
						(a, e) => a.Equals(e),
						$"be {Formatter.Format(expected)}")),
			source);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<DateOnly?, IThat<DateOnly?>> NotBe(
		this IThat<DateOnly?> source,
		DateOnly? unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ConditionConstraint(
					it,
					unexpected,
					(a, e) => !a.Equals(e),
					$"not be {Formatter.Format(unexpected)}")),
			source);
}
#endif
