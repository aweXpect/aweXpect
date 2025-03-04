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
					new IsSameAsConstraint<T>(it, grammars, expected, doNotPopulateThisValue.TrimCommonWhiteSpace())),
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
					new IsNotSameAsConstraint<T>(it, grammars, expected,
						doNotPopulateThisValue.TrimCommonWhiteSpace())),
			source);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static string IsSameAsExpectation(bool isNegated, string expectedExpression, object? expected)
		=> isNegated
			? $"does not refer to {expectedExpression} {Formatter.Format(expected, FormattingOptions.MultipleLines)}"
			: $"refers to {expectedExpression} {Formatter.Format(expected, FormattingOptions.MultipleLines)}";

	private readonly struct IsSameAsConstraint<T>(
		string it,
		ExpectationGrammars grammars,
		object? expected,
		string expectedExpression)
		: IValueConstraint<T>
	{
		public ConstraintResult IsMetBy(T actual)
			=> ConstraintResult.FromOutcome(
				ReferenceEquals(actual, expected) ? Outcome.Success :  Outcome.Failure,
				actual, expected, it, grammars,
				ToString,
				(i,_,a,_) => $"{i} was {Formatter.Format(a, FormattingOptions.MultipleLines)}",
				(i,_,_,_) => $"{i} did");

		public override string ToString()
			=> IsSameAsExpectation(grammars.HasFlag(ExpectationGrammars.Negated), expectedExpression, expected);
	}

	private readonly struct IsNotSameAsConstraint<T>(
		string it,
		ExpectationGrammars grammars,
		object? unexpected,
		string expectedExpression)
		: IValueConstraint<T>
	{
		public ConstraintResult IsMetBy(T actual)
			=> ConstraintResult.FromOutcome(
				ReferenceEquals(actual, unexpected) ? Outcome.Failure : Outcome.Success,
				actual, unexpected, it, grammars,
				ToString,
				(i,_,_,_) => $"{i} did",
				(i,_,a,_) => $"{i} was {Formatter.Format(a, FormattingOptions.MultipleLines)}");

		public override string ToString()
			=> IsSameAsExpectation(!grammars.HasFlag(ExpectationGrammars.Negated), expectedExpression, unexpected);
	}
}
