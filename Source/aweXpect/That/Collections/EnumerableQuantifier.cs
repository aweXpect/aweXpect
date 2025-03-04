using System;
using System.Text;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;

namespace aweXpect;

/// <summary>
///     Quantifier for evaluating collections.
/// </summary>
public abstract partial class EnumerableQuantifier
{
	/// <summary>
	///     Checks for each iteration, if the result is determinable by the <paramref name="matchingCount" /> and
	///     <paramref name="notMatchingCount" />.
	/// </summary>
	public abstract bool IsDeterminable(int matchingCount, int notMatchingCount);

	/// <summary>
	///     Returns true, if the quantifier should be treated as containing a single item.
	/// </summary>
	/// <remarks>
	///     This means, that the expectation text can be written in singular.
	/// </remarks>
	public abstract bool IsSingle();

	/// <summary>
	///     Returns the result.
	/// </summary>
	public abstract ConstraintResult GetResult<TEnumerable>(
		TEnumerable actual,
		string it,
		string? expectationExpression,
		int matchingCount,
		int notMatchingCount,
		int? totalCount,
		string? verb,
		Func<string, string?, string>? expectationGenerator = null);

	/// <summary>
	///     Returns the outcome.
	/// </summary>
	public abstract Outcome GetOutcome(
		int matchingCount,
		int notMatchingCount,
		int? totalCount);

	/// <summary>
	///     Appends the result text to the <paramref name="stringBuilder" />.
	/// </summary>
	public abstract void AppendResult(StringBuilder stringBuilder,
		ExpectationGrammars grammars,
		int matchingCount,
		int notMatchingCount,
		int? totalCount);


	private string GenerateExpectation(string quantifierExpectation,
		string? expectationExpression,
		Func<string, string?, string>? expectationGenerator,
		ExpectationGrammars expectationGrammars)
	{
		if (expectationGenerator is not null)
		{
			return expectationGenerator(quantifierExpectation, expectationExpression);
		}

		if (expectationExpression is null)
		{
			return quantifierExpectation;
		}

		if (expectationGrammars.HasFlag(ExpectationGrammars.Nested))
		{
			return $"{quantifierExpectation} {expectationExpression}";
		}

		return $"{expectationExpression} for {quantifierExpectation} {this.GetItemString()}";
	}

	/// <summary>
	///     The collection result could not be evaluated.
	/// </summary>
	private sealed class UndecidedResult<T> : ConstraintResult
	{
		private readonly string _expectationText;
		private readonly string _resultText;

		/// <summary>
		///     Initializes a new instance of <see cref="UndecidedResult{T}" />.
		/// </summary>
		public UndecidedResult(
			T value,
			string expectationText,
			string resultText,
			FurtherProcessingStrategy furtherProcessingStrategy = FurtherProcessingStrategy.Continue)
			: base(Outcome.Undecided, furtherProcessingStrategy)
		{
			_ = value;
			_expectationText = expectationText;
			_resultText = resultText;
		}

		/// <inheritdoc />
		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(_expectationText.Indent(indentation, false));

		/// <inheritdoc />
		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(_resultText.Indent(indentation, false));
	}
}
