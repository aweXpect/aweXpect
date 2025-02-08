using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableBool
{
	/// <summary>
	///     Verifies that the subject is <see langword="true" />.
	/// </summary>
	public static AndOrResult<bool?, IThat<bool?>> IsTrue(this IThat<bool?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form)
				=> new IsEqualToConstraint(it, true)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="true" />.
	/// </summary>
	public static AndOrResult<bool?, IThat<bool?>> IsNotTrue(this IThat<bool?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form)
				=> new IsNotEqualToConstraint(it, true)),
			source);
}
