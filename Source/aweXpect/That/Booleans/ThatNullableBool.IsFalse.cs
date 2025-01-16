﻿using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableBool
{
	/// <summary>
	///     Verifies that the subject is <see langword="false" />.
	/// </summary>
	public static AndOrResult<bool?, IExpectSubject<bool?>> IsFalse(this IExpectSubject<bool?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new BeValueConstraint(it, false)),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="false" />.
	/// </summary>
	public static AndOrResult<bool?, IExpectSubject<bool?>> IsNotFalse(this IExpectSubject<bool?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new NotBeValueConstraint(it, false)),
			source);
}
