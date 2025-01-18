#if NET8_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableTimeOnly
{
	/// <summary>
	///     Verifies that the second of the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TimeOnly?, IExpectSubject<TimeOnly?>> HasSecond(this IExpectSubject<TimeOnly?> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<int?>(
					it,
					expected,
					(a, e) => a.HasValue && a.Value.Second == e,
					$"have second of {Formatter.Format(expected)}")),
			source);

	/// <summary>
	///     Verifies that the second of the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<TimeOnly?, IExpectSubject<TimeOnly?>> DoesNotHaveSecond(
		this IExpectSubject<TimeOnly?> source,
		int? unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new PropertyConstraint<int?>(
					it,
					unexpected,
					(a, e) => !a.HasValue || a.Value.Second != e,
					$"not have second of {Formatter.Format(unexpected)}")),
			source);
}
#endif
