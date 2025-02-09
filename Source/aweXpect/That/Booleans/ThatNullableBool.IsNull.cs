using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableBool
{
	/// <summary>
	///     Verifies that the subject is <see langword="null" />.
	/// </summary>
	public static AndOrResult<bool?, IThat<bool?>> IsNull(this IThat<bool?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new IsEqualToConstraint(it, null)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" />.
	/// </summary>
	public static AndOrResult<bool?, IThat<bool?>> IsNotNull(this IThat<bool?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new IsNotEqualToConstraint(it, null)),
			source);
}
