﻿using System;
using aweXpect.Core.Constraints;

namespace aweXpect;

/// <summary>
///     Expectations on <see langword="enum" />? values.
/// </summary>
public static partial class ThatNullableEnum
{
	private readonly struct ValueConstraint<TEnum>(
		string it,
		string expectation,
		Func<TEnum?, bool> successIf)
		: IValueConstraint<TEnum?>
		where TEnum : struct, Enum
	{
		public ConstraintResult IsMetBy(TEnum? actual)
		{
			if (successIf(actual))
			{
				return new ConstraintResult.Success<TEnum?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> expectation;
	}
}
