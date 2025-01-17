using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatGuid
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<Guid, IExpectSubject<Guid>> Is(this IExpectSubject<Guid> source,
		Guid? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					it,
					$"be {Formatter.Format(expected)}",
					actual => actual.Equals(expected))),
			source);

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<Guid, IExpectSubject<Guid>> IsNot(this IExpectSubject<Guid> source,
		Guid? unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					it,
					$"not be {Formatter.Format(unexpected)}",
					actual => !actual.Equals(unexpected))),
			source);
}
