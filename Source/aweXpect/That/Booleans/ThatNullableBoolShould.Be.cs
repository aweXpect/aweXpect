using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableBoolShould
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<bool?, IThatShould<bool?>> Be(this IThatShould<bool?> source,
		bool? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new BeValueConstraint(it, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<bool?, IThatShould<bool?>> NotBe(this IThatShould<bool?> source,
		bool? unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NotBeValueConstraint(it, unexpected)),
			source);
}
