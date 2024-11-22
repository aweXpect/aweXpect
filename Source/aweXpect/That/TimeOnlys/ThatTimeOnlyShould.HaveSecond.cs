#if NET6_0_OR_GREATER
using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatTimeOnlyShould
{
	/// <summary>
	///     Verifies that the second of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TimeOnly, IThat<TimeOnly>> HaveSecond(this IThat<TimeOnly> source,
		int? expected)
	{
		return new AndOrResult<TimeOnly, IThat<TimeOnly>>(source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					expected,
					(a, e) => a.Second == e,
					$"have second of {Formatter.Format(expected)}")),
			source);
	}

	/// <summary>
	///     Verifies that the second of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<TimeOnly, IThat<TimeOnly>> NotHaveSecond(
		this IThat<TimeOnly> source,
		int? unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => a.Second != e,
					$"not have second of {Formatter.Format(unexpected)}")),
			source);
}
#endif
