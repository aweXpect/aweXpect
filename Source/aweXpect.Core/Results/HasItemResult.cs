using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result for verifying that a collection has a matching item at a given index.
/// </summary>
/// <remarks>
///     <seealso cref="ExpectationResult{TType,TSelf}" />
/// </remarks>
public class HasItemResult<TCollection>(
	ExpectationBuilder expectationBuilder,
	IThat<TCollection> collection,
	CollectionIndexOptions collectionIndexOptions)
	: AndOrResult<TCollection, IThat<TCollection>>(expectationBuilder, collection)
{
	private readonly ExpectationBuilder _expectationBuilder = expectationBuilder;
	private readonly IThat<TCollection> _collection = collection;

	/// <summary>
	///     …at the given <paramref name="index" />.
	/// </summary>
	public HasItemResultAtIndex<TCollection> AtIndex(int index)
	{
		collectionIndexOptions.SetIndex(index);
		return new(_expectationBuilder, _collection, collectionIndexOptions);
	}
}
/// <summary>
///     The result for verifying that a collection has a matching item at a given index.
/// </summary>
/// <remarks>
///     <seealso cref="ExpectationResult{TType,TSelf}" />
/// </remarks>
public class HasItemResultAtIndex<TCollection>(
	ExpectationBuilder expectationBuilder,
	IThat<TCollection> collection,
	CollectionIndexOptions collectionIndexOptions)
	: AndOrResult<TCollection, IThat<TCollection>>(expectationBuilder, collection)
{
	/// <summary>
	///     …from end.
	/// </summary>
	public AndOrResult<TCollection, IThat<TCollection>> FromEnd()
	{
		collectionIndexOptions.SetFromEnd();
		return this;
	}
}
