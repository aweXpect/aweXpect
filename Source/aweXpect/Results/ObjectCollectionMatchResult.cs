﻿using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result for verifying that a collection matches another collection.
/// </summary>
/// <remarks>
///     <seealso cref="ObjectEqualityResult{TType,TThat,TSelf}" />
/// </remarks>
public class ObjectCollectionMatchResult<TType, TThat, TElement>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	ObjectEqualityOptions<TElement> options,
	CollectionMatchOptions collectionMatchOptions)
	: ObjectCollectionMatchResult<TType, TThat, TElement,
		ObjectCollectionMatchResult<TType, TThat, TElement>>(
		expectationBuilder,
		returnValue,
		options,
		collectionMatchOptions);

/// <summary>
///     The result for verifying that a collection matches another collection.
/// </summary>
/// <remarks>
///     <seealso cref="ObjectEqualityResult{TType,TThat,TSelf}" />
/// </remarks>
public class ObjectCollectionMatchResult<TType, TThat, TElement, TSelf>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	ObjectEqualityOptions<TElement> options,
	CollectionMatchOptions collectionMatchOptions)
	: ObjectEqualityResult<TType, TThat, TElement, TSelf>(expectationBuilder, returnValue, options)
	where TSelf : ObjectCollectionMatchResult<TType, TThat, TElement, TSelf>
{
	/// <summary>
	///     Ignores the order in the subject and expected values.
	/// </summary>
	public TSelf InAnyOrder()
	{
		collectionMatchOptions.InAnyOrder();
		return (TSelf)this;
	}

	/// <summary>
	///     Ignores duplicates in both collections.
	/// </summary>
	public TSelf IgnoringDuplicates()
	{
		collectionMatchOptions.IgnoringDuplicates();
		return (TSelf)this;
	}
}
