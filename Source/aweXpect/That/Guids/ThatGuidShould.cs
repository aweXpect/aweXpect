﻿using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="Guid" /> values.
/// </summary>
public static partial class ThatGuidShould
{
	/// <summary>
	///     Start expectations for current <see cref="Guid" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<Guid> Should(this IExpectSubject<Guid> subject)
		=> subject.Should(That.WithoutAction);

	private readonly struct ValueConstraint(
		string it,
		string expectation,
		Func<Guid, bool> successIf)
		: IValueConstraint<Guid>
	{
		public ConstraintResult IsMetBy(Guid actual)
		{
			if (successIf(actual))
			{
				return new ConstraintResult.Success<Guid?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> expectation;
	}
}