using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableBoolShould
{
	/// <summary>
	///     Verifies that the subject is <see langword="false" />.
	/// </summary>
	public static AndOrResult<bool?, IThat<bool?>> BeFalse(this IThat<bool?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new BeValueConstraint(it, false)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="false" />.
	/// </summary>
	public static AndOrResult<bool?, IThat<bool?>> NotBeFalse(this IThat<bool?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NotBeValueConstraint(it, false)),
			source);
}
