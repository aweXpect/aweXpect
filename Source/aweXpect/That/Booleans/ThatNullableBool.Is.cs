using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableBool
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<bool?, IExpectSubject<bool?>> Is(this IExpectSubject<bool?> source,
		bool? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new BeValueConstraint(it, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<bool?, IExpectSubject<bool?>> IsNot(this IExpectSubject<bool?> source,
		bool? unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new NotBeValueConstraint(it, unexpected)),
			source);
}
