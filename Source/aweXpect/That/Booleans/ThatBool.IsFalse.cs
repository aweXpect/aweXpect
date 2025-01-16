using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatBool
{
	/// <summary>
	///     Verifies that the subject is <see langword="false" />.
	/// </summary>
	[Obsolete("TODO")]
	public static AndOrResult<bool, IThatShould<bool>> BeFalse(this IThatShould<bool> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new BeValueConstraint(it, false)),
			source);
	
	/// <summary>
	///     Verifies that the subject is <see langword="false" />.
	/// </summary>
	public static AndOrResult<bool, IExpectSubject<bool>> IsFalse(this IExpectSubject<bool> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new BeValueConstraint(it, false)),
			source);
}
