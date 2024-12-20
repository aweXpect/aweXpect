﻿using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result for verifying that a string collection matches another collection.
/// </summary>
/// <remarks>
///     <seealso cref="StringEqualityTypeResult{TType,TThat,TSelf}" />
/// </remarks>
public class StringCollectionMatchResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	StringEqualityOptions options,
	CollectionMatchOptions collectionMatchOptions)
	: StringCollectionMatchResult<TType, TThat,
		StringCollectionMatchResult<TType, TThat>>(
		expectationBuilder,
		returnValue,
		options,
		collectionMatchOptions);

/// <summary>
///     The result for verifying that a string collection matches another collection.
/// </summary>
/// <remarks>
///     <seealso cref="StringEqualityTypeResult{TType,TThat,TSelf}" />
/// </remarks>
public class StringCollectionMatchResult<TType, TThat, TSelf>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	StringEqualityOptions options,
	CollectionMatchOptions collectionMatchOptions)
	: StringEqualityTypeResult<TType, TThat, TSelf>(expectationBuilder, returnValue, options)
	where TSelf : StringCollectionMatchResult<TType, TThat, TSelf>
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
