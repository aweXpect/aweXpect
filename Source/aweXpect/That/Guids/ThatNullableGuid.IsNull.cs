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
	public static AndOrResult<Guid?, IThat<Guid?>> IsNull(this IThat<Guid?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ValueConstraint(
					it,
					"is null",
					actual => actual == null)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" />.
	/// </summary>
	public static AndOrResult<Guid?, IThat<Guid?>> IsNotNull(this IThat<Guid?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ValueConstraint(
					it,
					"is not null",
					actual => actual != null)),
			source);
}
