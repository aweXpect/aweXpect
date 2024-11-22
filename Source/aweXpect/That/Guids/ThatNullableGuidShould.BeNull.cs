using System;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableGuidShould
{
	/// <summary>
	///     Verifies that the subject is <see langword="null" />.
	/// </summary>
	public static AndOrResult<Guid?, IThat<Guid?>> BeNull(this IThat<Guid?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					it,
					"be null",
					actual => actual == null)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" />.
	/// </summary>
	public static AndOrResult<Guid?, IThat<Guid?>> NotBeNull(this IThat<Guid?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					it,
					"not be null",
					actual => actual != null)),
			source);
}
