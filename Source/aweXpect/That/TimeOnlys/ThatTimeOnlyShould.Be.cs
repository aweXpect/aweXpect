#if NET6_0_OR_GREATER
using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatTimeOnlyShould
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TimeOnly, IThat<TimeOnly>> Be(this IThat<TimeOnly> source,
		TimeOnly expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ConditionConstraint(
					it,
					expected,
					(a, e) => a.Equals(e),
					$"be {Formatter.Format(expected)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<TimeOnly, IThat<TimeOnly>> NotBe(
		this IThat<TimeOnly> source,
		TimeOnly unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ConditionConstraint(
					it,
					unexpected,
					(a, e) => !a.Equals(e),
					$"not be {Formatter.Format(unexpected)}")),
			source);
}
#endif
