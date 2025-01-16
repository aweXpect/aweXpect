﻿using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="bool" />? values.
/// </summary>
public static partial class ThatNullableBool
{
	/// <summary>
	///     Start expectations for the current <see cref="bool" />? <paramref name="subject" />.
	/// </summary>
	public static IThatShould<bool?> Should(this IExpectSubject<bool?> subject)
		=> subject.Should(That.WithoutAction);

	private readonly struct BeValueConstraint(string it, bool? expected) : IValueConstraint<bool?>
	{
		public ConstraintResult IsMetBy(bool? actual)
		{
			if (expected.Equals(actual))
			{
				return new ConstraintResult.Success<bool?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"be {Formatter.Format(expected)}";
	}

	private readonly struct NotBeValueConstraint(string it, bool? unexpected)
		: IValueConstraint<bool?>
	{
		public ConstraintResult IsMetBy(bool? actual)
		{
			if (!unexpected.Equals(actual))
			{
				return new ConstraintResult.Success<bool?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"not be {Formatter.Format(unexpected)}";
	}
}