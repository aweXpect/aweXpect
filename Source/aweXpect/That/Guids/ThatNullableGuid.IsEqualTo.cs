﻿using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableGuid
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<Guid?, IThat<Guid?>> IsEqualTo(this IThat<Guid?> source,
		Guid? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new ValueConstraint(
					it,
					$"is {Formatter.Format(expected)}",
					actual => actual?.Equals(expected) ?? expected == null)),
			source);

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<Guid?, IThat<Guid?>> IsNotEqualTo(this IThat<Guid?> source,
		Guid? unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new ValueConstraint(
					it,
					$"is not {Formatter.Format(unexpected)}",
					actual => !actual?.Equals(unexpected) ?? unexpected != null)),
			source);
}
