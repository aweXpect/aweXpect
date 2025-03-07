using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableBool
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<bool?, IThat<bool?>> IsEqualTo(this IThat<bool?> source,
		bool? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<bool?, IThat<bool?>> IsNotEqualTo(this IThat<bool?> source,
		bool? unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint(it, grammars, unexpected).Invert()),
			source);
}
