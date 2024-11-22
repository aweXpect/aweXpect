using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatDateTimeShould
{
	/// <summary>
	///     Verifies that the second of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<DateTime, IThat<DateTime>> HaveSecond(this IThat<DateTime> source,
		int? expected)
	{
		return new AndOrResult<DateTime, IThat<DateTime>>(source.ExpectationBuilder.AddConstraint(it
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
	public static AndOrResult<DateTime, IThat<DateTime>> NotHaveSecond(
		this IThat<DateTime> source,
		int? unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => a.Second != e,
					$"not have second of {Formatter.Format(unexpected)}")),
			source);
}
