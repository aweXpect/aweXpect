using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="ObjectEqualityResult{TResult,TValue}" />, allows specifying
///     options on the <see cref="CollectionMatchOptions" />.
/// </summary>
public class CollectionBeResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	ObjectEqualityOptions options,
	CollectionMatchOptions collectionMatchOptions)
	: ObjectCollectionMatchResult<TType, TThat>(expectationBuilder, returnValue, options, collectionMatchOptions)
{
	private readonly CollectionMatchOptions _collectionMatchOptions = collectionMatchOptions;

	/// <summary>
	///     Verifies that all items in the subject are expected, but expected contains at least one more item.
	/// </summary>
	/// <remarks>
	///     This means, that the subject is a proper subset of the expected collection.
	/// </remarks>
	public ObjectCollectionMatchResult<TType, TThat> AndLess()
	{
		_collectionMatchOptions.SetEquivalenceRelation(CollectionMatchOptions.EquivalenceRelations.ProperSubset);
		return this;
	}

	/// <summary>
	///     Verifies that the subject contain all expected items and at least one additional item.
	/// </summary>
	/// <remarks>
	///     This means, that the subject is a proper superset of the expected collection.
	/// </remarks>
	public ObjectCollectionMatchResult<TType, TThat> AndMore()
	{
		_collectionMatchOptions.SetEquivalenceRelation(CollectionMatchOptions.EquivalenceRelations.ProperSuperset);
		return this;
	}

	/// <summary>
	///     Verifies that all items in the subject are expected, but expected could contain more items.
	/// </summary>
	/// <remarks>
	///     This means, that the subject is a subset of the expected collection.
	/// </remarks>
	public ObjectCollectionMatchResult<TType, TThat> OrLess()
	{
		_collectionMatchOptions.SetEquivalenceRelation(CollectionMatchOptions.EquivalenceRelations.Subset);
		return this;
	}

	/// <summary>
	///     Verifies that the subject contain all expected items, but can also contain more items.
	/// </summary>
	/// <remarks>
	///     This means, that the subject is a superset of the expected collection.
	/// </remarks>
	public ObjectCollectionMatchResult<TType, TThat> OrMore()
	{
		_collectionMatchOptions.SetEquivalenceRelation(CollectionMatchOptions.EquivalenceRelations.Superset);
		return this;
	}
}
