using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableGuidShould
{
	/// <summary>
	///     Verifies that the subject is empty.
	/// </summary>
	public static AndOrResult<Guid?, IThatShould<Guid?>> BeEmpty(this IThatShould<Guid?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					it,
					"be empty",
					actual => actual != null && actual == Guid.Empty)),
			source);

	/// <summary>
	///     Verifies that the subject is not empty.
	/// </summary>
	public static AndOrResult<Guid?, IThatShould<Guid?>> NotBeEmpty(this IThatShould<Guid?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					it,
					"not be empty",
					actual => actual != null && actual != Guid.Empty)),
			source);
}
