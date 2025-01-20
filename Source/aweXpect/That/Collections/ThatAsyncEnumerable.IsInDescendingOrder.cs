﻿#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Verifies that the collection is in descending order.
	/// </summary>
	public static CollectionOrderResult<TItem, IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		IsInDescendingOrder<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source)
	{
		CollectionOrderOptions<TItem> options = new();
		return new CollectionOrderResult<TItem, IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new BeInOrderConstraint<TItem, TItem>(it, x => x, SortOrder.Descending, options, "")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is in descending order.
	/// </summary>
	public static CollectionOrderResult<TMember, IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		IsInDescendingOrder<
			TItem, TMember>(
			this IThat<IAsyncEnumerable<TItem>> source,
			Func<TItem, TMember> memberAccessor,
			[CallerArgumentExpression("memberAccessor")]
			string doNotPopulateThisValue = "")
	{
		CollectionOrderOptions<TMember> options = new();
		return new CollectionOrderResult<TMember, IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new BeInOrderConstraint<TItem, TMember>(it, memberAccessor, SortOrder.Descending, options,
					$" for {doNotPopulateThisValue}")),
			source,
			options);
	}
}
#endif
