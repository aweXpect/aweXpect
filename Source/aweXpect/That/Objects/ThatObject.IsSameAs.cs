using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatObject
{
	/// <summary>
	///     Verifies the actual value to be the same as the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<T?, IThat<T?>> IsSameAs<T>(this IThat<T?> source,
		object? expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
		where T : class
		=> new(source.ThatIs().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsSameAsConstraint<T>(it, expected, doNotPopulateThisValue.TrimCommonWhiteSpace())),
			source);

	/// <summary>
	///     Verifies the actual value to not be the same as the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<T?, IThat<T?>> IsNotSameAs<T>(this IThat<T?> source,
		object? expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
		where T : class
		=> new(source.ThatIs().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsNotSameAsConstraint<T>(it, expected, doNotPopulateThisValue.TrimCommonWhiteSpace())),
			source);

	private readonly struct IsSameAsConstraint<T>(
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
			=> $"refers to {expectedExpression} {Formatter.Format(expected, FormattingOptions.MultipleLines)}";
	}

	private readonly struct IsNotSameAsConstraint<T>(
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
			=> $"does not refer to {expectedExpression} {Formatter.Format(unexpected, FormattingOptions.MultipleLines)}";
	}
}
