#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that at most <paramref name="maximum" /> items in the collection satisfy the
	///     <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>> HaveAtMost<TItem>(
		this IThat<IAsyncEnumerable<TItem>> source,
		int maximum,
		Action<IThat<TItem>> expectations)
		=> new(source.ExpectationBuilder.AddConstraint(it
			=> new AsyncCollectionConstraint<TItem>(it, EnumerableQuantifier.AtMost(maximum), expectations)), source);

	/// <summary>
	///     Verifies that the collection has at most <paramref name="maximum" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>> HaveAtMost<TItem>(
		this IThat<IAsyncEnumerable<TItem>> source,
		int maximum)
		=> new(new AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new AsyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.AtMost(maximum))),
			source));
}
#endif
