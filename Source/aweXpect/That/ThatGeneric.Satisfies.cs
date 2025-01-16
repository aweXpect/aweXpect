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
	///     Expect the actual value to satisfy the <paramref name="predicate" />.
	/// </summary>
	[Obsolete("TODO")]
	public static AndOrResult<T, IThat<T>> Satisfy<T>(this IThat<T> source,
		Func<T, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
		=> new(source.ExpectationBuilder
				.AddConstraint(it =>
					new SatisfyConstraint<T>(it, predicate, doNotPopulateThisValue)),
			source);

	
	/// <summary>
	///     Expect the actual value to satisfy the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<T, IExpectSubject<T>> Satisfies<T>(this IExpectSubject<T> source,
		Func<T, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
		=> new(source.ThatIs().ExpectationBuilder
				.AddConstraint(it =>
					new SatisfyConstraint<T>(it, predicate, doNotPopulateThisValue)),
			source);

	/// <summary>
	///     Expect the actual value to not satisfy the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<T, IExpectSubject<T>> DoesNotSatisfy<T>(this IExpectSubject<T> source,
		Func<T, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
		=> new(source.ThatIs().ExpectationBuilder
				.AddConstraint(it =>
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
