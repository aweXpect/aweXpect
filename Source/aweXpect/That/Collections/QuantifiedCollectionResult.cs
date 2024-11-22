using Testably.Expectations.Core;

namespace Testably.Expectations;

/// <summary>
///     A quantifiable result matching items against the expected <see cref="Quantity" />.
/// </summary>
public class QuantifiedCollectionResult<TResult>(
	TResult result,
	ExpectationBuilder expectationBuilder,
	CollectionQuantifier quantity)
{
	/// <summary>
	///     The expectation builder.
	/// </summary>
	public ExpectationBuilder ExpectationBuilder { get; } = expectationBuilder;

	/// <summary>
	///     The quantifier.
	/// </summary>
	public CollectionQuantifier Quantity => quantity;

	/// <summary>
	///     The return value of the expectation.
	/// </summary>
	public TResult Result => result;
}
