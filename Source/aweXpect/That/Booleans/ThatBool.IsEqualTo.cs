﻿using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatBool
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<bool, IThat<bool>> IsEqualTo(this IThat<bool> source,
		bool expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new IsEqualToConstraint(it, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<bool, IThat<bool>> IsNotEqualTo(this IThat<bool> source,
		bool unexpected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new IsNotEqualToConstraint(it, unexpected)),
			source);

	private readonly struct IsNotEqualToConstraint(string it, bool unexpected)
		: IValueConstraint<bool>
	{
		public ConstraintResult IsMetBy(bool actual)
		{
			if (!unexpected.Equals(actual))
			{
				return new ConstraintResult.Success<bool>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"is not {Formatter.Format(unexpected)}";
	}
}
