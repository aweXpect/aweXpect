using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result for verifying that a collection is contained in another collection.
/// </summary>
/// <remarks>
///     <seealso cref="ObjectCollectionMatchResult{TType,TThat,TItem}" />
/// </remarks>
public class CollectionBeContainedInResult<TType, TThat, TItem>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	CollectionMatchOptions collectionMatchOptions)
	: CollectionMatchResult<TType, TThat, TItem>(expectationBuilder, returnValue, collectionMatchOptions)
{
	private readonly CollectionMatchOptions _collectionMatchOptions = collectionMatchOptions;

	/// <summary>
	///     Verifies that the subject is contained in the expected items which has at least one additional item.
	/// </summary>
	/// <remarks>
	///     This means, that the expected collection is a proper superset.
	/// </remarks>
	public CollectionMatchResult<TType, TThat, TItem> Properly()
	{
		_collectionMatchOptions.SetEquivalenceRelation(
			CollectionMatchOptions.EquivalenceRelations.IsContainedInProperly);
		return this;
	}
}
