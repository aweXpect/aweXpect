using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableBool
{
	/// <summary>
	///     Verifies that the subject is <see langword="true" />.
	/// </summary>
	public static AndOrResult<bool?, IExpectSubject<bool?>> IsTrue(this IExpectSubject<bool?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new BeValueConstraint(it, true)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="true" />.
	/// </summary>
	public static AndOrResult<bool?, IExpectSubject<bool?>> IsNotTrue(this IExpectSubject<bool?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new NotBeValueConstraint(it, true)),
			source);
}
