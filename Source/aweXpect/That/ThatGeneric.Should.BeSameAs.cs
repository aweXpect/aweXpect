using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatGeneric
{
	/// <summary>
	///     Expect the actual value to be the same as the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<T, IThatShould<T>> BeSameAs<T>(this IThatShould<T> source,
		object? expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
		=> new(source.ExpectationBuilder
				.AddConstraint(it =>
					new BeSameAsConstraint<T>(it, expected, doNotPopulateThisValue)),
			source);

	/// <summary>
	///     Expect the actual value to not be the same as the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<T, IThatShould<T>> NotBeSameAs<T>(this IThatShould<T> source,
		object? unexpected,
		[CallerArgumentExpression("unexpected")]
		string doNotPopulateThisValue = "")
		=> new(source.ExpectationBuilder
				.AddConstraint(it =>
					new NotBeSameAsConstraint<T>(it, unexpected, doNotPopulateThisValue)),
			source);
	
	
	
	/// <summary>
	///     Expect the actual value to be the same as the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<T, IExpectSubject<T>> IsSameAs<T>(this IExpectSubject<T> source,
		object? expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
		=> new(source.ThatIs().ExpectationBuilder
				.AddConstraint(it =>
					new BeSameAsConstraint<T>(it, expected, doNotPopulateThisValue)),
			source);
	
	/// <summary>
	///     Expect the actual value to not be the same as the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<T, IExpectSubject<T>> IsNotSameAs<T>(this IExpectSubject<T> source,
		object? expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
		=> new(source.ThatIs().ExpectationBuilder
				.AddConstraint(it =>
					new NotBeSameAsConstraint<T>(it, expected, doNotPopulateThisValue)),
			source);

	private readonly struct BeSameAsConstraint<T>(
		string it,
		object? expected,
		string expectedExpression)
		: IValueConstraint<T>
	{
		public ConstraintResult IsMetBy(T actual)
		{
			if (ReferenceEquals(actual, expected))
			{
				return new ConstraintResult.Success<T>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}");
		}

		public override string ToString()
			=> $"refer to {expectedExpression} {Formatter.Format(expected, FormattingOptions.MultipleLines)}";
	}

	private readonly struct NotBeSameAsConstraint<T>(
		string it,
		object? unexpected,
		string expectedExpression)
		: IValueConstraint<T>
	{
		public ConstraintResult IsMetBy(T actual)
		{
			if (!ReferenceEquals(actual, unexpected))
			{
				return new ConstraintResult.Success<T>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} did");
		}

		public override string ToString()
			=> $"not refer to {expectedExpression} {Formatter.Format(unexpected, FormattingOptions.MultipleLines)}";
	}
}
