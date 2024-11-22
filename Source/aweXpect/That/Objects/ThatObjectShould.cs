using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="object" /> values.
/// </summary>
public static partial class ThatObjectShould
{
	/// <summary>
	///     Start expectations for the current <see cref="object" />? <paramref name="subject" />.
	/// </summary>
	public static IThat<object?> Should(this IExpectSubject<object?> subject)
		=> subject.Should(That.WithoutAction);

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
