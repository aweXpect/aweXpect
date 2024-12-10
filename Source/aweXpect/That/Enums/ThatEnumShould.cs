﻿using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;

namespace aweXpect;

/// <summary>
///     Expectations on <see langword="enum" /> values.
/// </summary>
public static partial class ThatEnumShould
{
	/// <summary>
	///     Start expectations for the current <typeparamref name="TEnum" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<TEnum> Should<TEnum>(this IExpectSubject<TEnum> subject)
		where TEnum : struct, Enum
		=> subject.Should(That.WithoutAction);

	private readonly struct ValueConstraint<TEnum>(
		string it,
		string expectation,
		Func<TEnum, bool> successIf)
		: IValueConstraint<TEnum>
		where TEnum : struct, Enum
	{
		public ConstraintResult IsMetBy(TEnum actual)
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