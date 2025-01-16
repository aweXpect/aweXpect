using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableBool
{
	/// <summary>
	///     Verifies that the subject is <see langword="null" />.
	/// </summary>
	public static AndOrResult<bool?, IExpectSubject<bool?>> IsNull(this IExpectSubject<bool?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new BeValueConstraint(it, null)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" />.
	/// </summary>
	public static AndOrResult<bool?, IExpectSubject<bool?>> IsNotNull(this IExpectSubject<bool?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new NotBeValueConstraint(it, null)),
			source);
}
