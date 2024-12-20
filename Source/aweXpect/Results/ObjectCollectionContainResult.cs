using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result for verifying that a collection contains another collection.
/// </summary>
/// <remarks>
///     <seealso cref="ObjectCollectionMatchResult{TType,TThat}" />
/// </remarks>
public class ObjectCollectionContainResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	ObjectEqualityOptions options,
	CollectionMatchOptions collectionMatchOptions)
	: ObjectCollectionMatchResult<TType, TThat>(expectationBuilder, returnValue, options, collectionMatchOptions)
{
	private readonly CollectionMatchOptions _collectionMatchOptions = collectionMatchOptions;

	/// <summary>
	///     Verifies that the subject contain all expected items and at least one additional item.
	/// </summary>
	/// <remarks>
	///     This means, that the expected collection is a proper subset.
	/// </remarks>
	public ObjectCollectionMatchResult<TType, TThat> Properly()
	{
		_collectionMatchOptions.SetEquivalenceRelation(CollectionMatchOptions.EquivalenceRelations.ContainsProperly);
		return this;
	}
}
