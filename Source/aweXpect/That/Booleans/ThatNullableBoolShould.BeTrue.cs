using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableBoolShould
{
	/// <summary>
	///     Verifies that the subject is <see langword="true" />.
	/// </summary>
	public static AndOrResult<bool?, IThatShould<bool?>> BeTrue(this IThatShould<bool?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new BeValueConstraint(it, true)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="true" />.
	/// </summary>
	public static AndOrResult<bool?, IThatShould<bool?>> NotBeTrue(this IThatShould<bool?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NotBeValueConstraint(it, true)),
			source);
}
