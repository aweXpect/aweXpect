using aweXpect.Core;
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
	///     Returns the expectation text.
	/// </summary>
	public abstract string GetExpectation(string it, string? expectationExpression);

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
		string? verb);
}
