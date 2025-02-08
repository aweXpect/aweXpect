using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableGuid
{
	/// <summary>
	///     Verifies that the subject is empty.
	/// </summary>
	public static AndOrResult<Guid?, IThat<Guid?>> IsEmpty(this IThat<Guid?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					it,
					"is empty",
					actual => actual != null && actual == Guid.Empty)),
			source);

	/// <summary>
	///     Verifies that the subject is not empty.
	/// </summary>
	public static AndOrResult<Guid?, IThat<Guid?>> IsNotEmpty(this IThat<Guid?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					it,
					"is not empty",
					actual => actual != null && actual != Guid.Empty)),
			source);
}
