using System;
using aweXpect.Core.Constraints;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="object" /> values.
/// </summary>
public static partial class ThatObject
{
	private readonly struct GenericConstraint<T>(
		string it,
		T expected,
		string expectation,
		Func<T, T, bool> condition,
		Func<T, T, string, string> failureMessageFactory)
		: IValueConstraint<T>
	{
		public ConstraintResult IsMetBy(T actual)
		{
			if (condition(actual, expected))
			{
				return new ConstraintResult.Success<T>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				failureMessageFactory(actual, expected, it));
		}

		public override string ToString()
			=> expectation;
	}
}
