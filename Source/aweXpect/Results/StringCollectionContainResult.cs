using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result for verifying that a string collection contains another collection.
/// </summary>
/// <remarks>
///     <seealso cref="StringCollectionMatchResult{TType,TThat}" />
/// </remarks>
public class StringCollectionContainResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	StringEqualityOptions options,
	CollectionMatchOptions collectionMatchOptions)
	: StringCollectionMatchResult<TType, TThat>(expectationBuilder, returnValue, options, collectionMatchOptions)
{
	private readonly CollectionMatchOptions _collectionMatchOptions = collectionMatchOptions;

	/// <summary>
	///     Verifies that the subject contain all expected items and at least one additional item.
	/// </summary>
	/// <remarks>
	///     This means, that the expected collection is a proper subset.
	/// </remarks>
	public StringCollectionMatchResult<TType, TThat> Properly()
	{
		_collectionMatchOptions.SetEquivalenceRelation(CollectionMatchOptions.EquivalenceRelations.ContainsProperly);
		return this;
	}
}
