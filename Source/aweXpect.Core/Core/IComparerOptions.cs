using System.Collections.Generic;
using aweXpect.Results;

namespace aweXpect.Core;

/// <summary>
///     Additional options for an <see cref="IEqualityComparer{T}" /> in an
///     <see cref="ObjectEqualityResult{TType,TThat}" />
/// </summary>
public interface IComparerOptions
{
	/// <summary>
	///     Returns the expectation string, e.g. <c>be equal to {expectedExpression}</c>.
	/// </summary>
	string GetExpectation(string expectedExpression);

	/// <summary>
	///     Returns the extended failure when comparing <paramref name="actual" /> and <paramref name="expected" /> fails.
	/// </summary>
	string GetExtendedFailure(string it, object? actual, object? expected);
}
