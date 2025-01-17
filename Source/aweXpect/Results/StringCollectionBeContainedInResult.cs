using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result for verifying that a string collection is contained in another collection.
/// </summary>
/// <remarks>
///     <seealso cref="StringCollectionMatchResult{TType,TThat}" />
/// </remarks>
public class StringCollectionBeContainedInResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	StringEqualityOptions options,
	CollectionMatchOptions collectionMatchOptions)
	: StringCollectionMatchResult<TType, TThat>(expectationBuilder, returnValue, options, collectionMatchOptions)
{
	private readonly CollectionMatchOptions _collectionMatchOptions = collectionMatchOptions;

	/// <summary>
	///     Verifies that the subject is contained in the expected items which has at least one additional item.
	/// </summary>
	/// <remarks>
	///     This means, that the expected collection is a proper superset.
	/// </remarks>
	public StringCollectionMatchResult<TType, TThat> Properly()
	{
		_collectionMatchOptions.SetEquivalenceRelation(
			CollectionMatchOptions.EquivalenceRelations.IsContainedInProperly);
		return this;
	}
}
