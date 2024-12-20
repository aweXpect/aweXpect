using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="ObjectEqualityResult{TResult,TValue}" />, allows specifying
///     options on the <see cref="CollectionMatchOptions" />.
/// </summary>
public class ObjectCollectionBeContainedInResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	ObjectEqualityOptions options,
	CollectionMatchOptions collectionMatchOptions)
	: ObjectCollectionMatchResult<TType, TThat>(expectationBuilder, returnValue, options, collectionMatchOptions)
{
	private readonly CollectionMatchOptions _collectionMatchOptions = collectionMatchOptions;

	/// <summary>
	///     Verifies that the subject is contained in the expected items which has at least one additional item.
	/// </summary>
	/// <remarks>
	///     This means, that the expected collection is a proper superset.
	/// </remarks>
	public ObjectCollectionMatchResult<TType, TThat> Properly()
	{
		_collectionMatchOptions.SetEquivalenceRelation(CollectionMatchOptions.EquivalenceRelations.IsContainedInProperly);
		return this;
	}
}
