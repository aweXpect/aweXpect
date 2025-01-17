#if NET8_0_OR_GREATER
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
	///     Verifies that the collection is in ascending order.
	/// </summary>
	public static CollectionOrderResult<TItem, IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>
		IsInAscendingOrder<TItem>(this IExpectSubject<IAsyncEnumerable<TItem>> source)
	{
		CollectionOrderOptions<TItem> options = new();
		return new CollectionOrderResult<TItem, IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new BeInOrderConstraint<TItem, TItem>(it, x => x, SortOrder.Ascending, options, "")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is in ascending order.
	/// </summary>
	public static CollectionOrderResult<TMember, IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>
		IsInAscendingOrder<TItem, TMember>(this IExpectSubject<IAsyncEnumerable<TItem>> source,
			Func<TItem, TMember> memberAccessor,
			[CallerArgumentExpression("memberAccessor")]
			string doNotPopulateThisValue = "")
	{
		CollectionOrderOptions<TMember> options = new();
		return new CollectionOrderResult<TMember, IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new BeInOrderConstraint<TItem, TMember>(it, memberAccessor, SortOrder.Ascending, options,
					$" for {doNotPopulateThisValue}")),
			source,
			options);
	}
}
#endif
