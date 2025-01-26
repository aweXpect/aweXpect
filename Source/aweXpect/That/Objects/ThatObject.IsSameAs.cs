using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatObject
{
	/// <summary>
	///     Expect the actual value to be the same as the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<T?, IThat<T?>> IsSameAs<T>(this IThat<T?> source,
		object? expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
		where T : class
		=> new(source.ThatIs().ExpectationBuilder
				.AddConstraint(it =>
					new BeSameAsConstraint<T>(it, expected, doNotPopulateThisValue)),
			source);

	/// <summary>
	///     Expect the actual value to not be the same as the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<T?, IThat<T?>> IsNotSameAs<T>(this IThat<T?> source,
		object? expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
		where T : class
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
