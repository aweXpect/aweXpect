#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that exactly <paramref name="expected" /> items in the collection satisfy the
	///     <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>> HaveExactly<TItem>(
		this IThat<IAsyncEnumerable<TItem>> source,
		int expected,
		Action<IThat<TItem>> expectations)
		=> new(source.ExpectationBuilder.AddConstraint(it
			=> new AsyncCollectionConstraint<TItem>(it, EnumerableQuantifier.Exactly(expected), expectations)), source);

	/// <summary>
	///     Verifies that the collection has exactly <paramref name="expected" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>> HaveExactly<TItem>(
		this IThat<IAsyncEnumerable<TItem>> source,
		int expected)
		=> new(new AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new AsyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.Exactly(expected))),
			source));
}
#endif
