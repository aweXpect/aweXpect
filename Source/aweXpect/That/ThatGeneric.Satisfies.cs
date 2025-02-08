using System;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatGeneric
{
	/// <summary>
	///     Verifies the actual value to satisfy the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<T, IThat<T>> Satisfies<T>(this IThat<T> source,
		Func<T, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
		=> new(source.ThatIs().ExpectationBuilder
				.AddConstraint((it, form) =>
					new SatisfyConstraint<T>(it, predicate, doNotPopulateThisValue)),
			source);

	/// <summary>
	///     Verifies the actual value to not satisfy the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<T, IThat<T>> DoesNotSatisfy<T>(this IThat<T> source,
		Func<T, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
		=> new(source.ThatIs().ExpectationBuilder
				.AddConstraint((it, form) =>
					new NotSatisfyConstraint<T>(it, predicate, doNotPopulateThisValue)),
			source);

	private readonly struct SatisfyConstraint<T>(
		string it,
		Func<T, bool> predicate,
		string predicateExpression)
		: IValueConstraint<T>
	{
		public ConstraintResult IsMetBy(T actual)
		{
			if (predicate(actual))
			{
				return new ConstraintResult.Success<T>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"satisfy {predicateExpression}";
	}

	private readonly struct NotSatisfyConstraint<T>(
		string it,
		Func<T, bool> predicate,
		string predicateExpression)
		: IValueConstraint<T>
	{
		public ConstraintResult IsMetBy(T actual)
		{
			if (!predicate(actual))
			{
				return new ConstraintResult.Success<T>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"not satisfy {predicateExpression}";
	}
}
