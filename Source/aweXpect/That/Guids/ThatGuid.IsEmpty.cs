using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatGuid
{
	/// <summary>
	///     Verifies that the subject is empty.
	/// </summary>
	public static AndOrResult<Guid, IExpectSubject<Guid>> IsEmpty(this IExpectSubject<Guid> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					it,
					"be empty",
					actual => actual == Guid.Empty)),
			source);

	/// <summary>
	///     Verifies that the subject is not empty.
	/// </summary>
	public static AndOrResult<Guid, IExpectSubject<Guid>> IsNotEmpty(this IExpectSubject<Guid> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					it,
					"not be empty",
					actual => actual != Guid.Empty)),
			source);
}
