#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that at least <paramref name="minimum" /> items in the asynchronous enumerable satisfy the
	///     <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>> HaveAtLeast<TItem>(
		this IThat<IAsyncEnumerable<TItem>> source,
		int minimum,
		Action<IThat<TItem>> expectations)
		=> new(source.ExpectationBuilder.AddConstraint(it
			=> new AsyncCollectionConstraint<TItem>(it, EnumerableQuantifier.AtLeast(minimum), expectations)), source);

	/// <summary>
	///     Verifies that the asynchronous enumerable has at least <paramref name="minimum" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>> HaveAtLeast<TItem>(
		this IThat<IAsyncEnumerable<TItem>> source,
		int minimum)
		=> new(new AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new AsyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.AtLeast(minimum))),
			source));
}
#endif
