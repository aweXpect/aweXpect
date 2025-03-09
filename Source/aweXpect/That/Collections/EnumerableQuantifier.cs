using System;
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
	///     Returns true, if the quantifier should be treated as containing a single item.
	/// </summary>
	/// <remarks>
	///     This means, that the expectation text can be written in singular.
	/// </remarks>
	public abstract bool IsSingle();

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
		int? totalCount,
		string? verb = null);
}
