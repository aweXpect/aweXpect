using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Equivalency;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result for verifying that a collection has a specific item at a given index.
/// </summary>
/// <remarks>
///     <seealso cref="HasItemResult{TCollection}" />
/// </remarks>
public class HasItemObjectResult<TCollection, TItem>(
	ExpectationBuilder expectationBuilder,
	IThat<TCollection> collection,
	CollectionIndexOptions collectionIndexOptions,
	ObjectEqualityOptions<TItem> options)
	: HasItemObjectResult<TCollection, TItem,
		HasItemObjectResult<TCollection, TItem>>(
		expectationBuilder,
		collection,
		collectionIndexOptions,
		options);


/// <summary>
///     The result for verifying that a collection has a specific item at a given index.
/// </summary>
/// <remarks>
///     <seealso cref="HasItemResult{TCollection}" />
/// </remarks>
public class HasItemObjectResult<TCollection, TItem, TSelf>(
	ExpectationBuilder expectationBuilder,
	IThat<TCollection> collection,
	CollectionIndexOptions collectionIndexOptions,
	ObjectEqualityOptions<TItem> options)
	: HasItemResult<TCollection>(expectationBuilder, collection, collectionIndexOptions)
	where TSelf : HasItemObjectResult<TCollection, TItem, TSelf>
{
	/// <summary>
	///     Use equivalency to compare objects.
	/// </summary>
	public TSelf Equivalent(Func<EquivalencyOptions, EquivalencyOptions>? optionsCallback = null)
	{
		options.Equivalent(EquivalencyOptionsExtensions.FromCallback(optionsCallback));
		return (TSelf)this;
	}

	/// <summary>
	///     Uses the provided <paramref name="comparer" /> for comparing <see langword="object" />s.
	/// </summary>
	public TSelf Using(IEqualityComparer<object> comparer)
	{
		options.Using(comparer);
		return (TSelf)this;
	}
}
