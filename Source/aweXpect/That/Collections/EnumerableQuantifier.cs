using System;
using aweXpect.Core.Constraints;

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


	private string GenerateExpectation(
		string quantifierExpectation,
		string? expectationExpression,
		Func<string, string?, string>? expectationGenerator = null)
	{
		if (expectationGenerator is not null)
		{
			return expectationGenerator(quantifierExpectation, expectationExpression);
		}

		if (expectationExpression is null)
		{
			return quantifierExpectation;
		}

		return $"{expectationExpression} for {quantifierExpectation} {(IsSingle() ? "item" : "items")}";
	}
}
