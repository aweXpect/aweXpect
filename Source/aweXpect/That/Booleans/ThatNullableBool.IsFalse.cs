using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableBool
{
	/// <summary>
	///     Verifies that the subject is <see langword="false" />.
	/// </summary>
	public static AndOrResult<bool?, IThat<bool?>> IsFalse(this IThat<bool?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form)
				=> new IsEqualToConstraint(it, false)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="false" />.
	/// </summary>
	public static AndOrResult<bool?, IThat<bool?>> IsNotFalse(this IThat<bool?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form)
				=> new IsNotEqualToConstraint(it, false)),
			source);
}
