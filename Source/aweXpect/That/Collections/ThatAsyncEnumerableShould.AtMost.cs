#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that at most <paramref name="maximum" /> items in the asynchronous enumerable satisfy the <paramref name="expectations"/>.
	/// </summary>
	public static AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>> AtMost<TItem>(
		this IThat<IAsyncEnumerable<TItem>> source,
		Times maximum,
		Action<IThat<TItem>> expectations)
		=> new(source.ExpectationBuilder.AddConstraint(it
			=> new AsyncCollectionConstraint<TItem>(it, EnumerableQuantifier.AtMost(maximum.Value), expectations)), source);
}
#endif
