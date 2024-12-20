using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="StringEqualityResult{TResult,TValue}" />, allows specifying
///     options on the <see cref="CollectionMatchOptions" />.
/// </summary>
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
