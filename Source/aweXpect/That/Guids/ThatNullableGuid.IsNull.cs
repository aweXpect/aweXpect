using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableGuid
{
	/// <summary>
	///     Verifies that the subject is <see langword="null" />.
	/// </summary>
	public static AndOrResult<Guid?, IExpectSubject<Guid?>> IsNull(this IExpectSubject<Guid?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					it,
					"be null",
					actual => actual == null)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" />.
	/// </summary>
	public static AndOrResult<Guid?, IExpectSubject<Guid?>> IsNotNull(this IExpectSubject<Guid?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					it,
					"not be null",
					actual => actual != null)),
			source);
}
