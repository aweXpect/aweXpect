using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that at most <paramref name="maximum" /> items in the synchronous enumerable satisfy the <paramref name="expectations"/>.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>> AtMost<TItem>(
		this IThat<IEnumerable<TItem>> source,
		int maximum,
		Action<IThat<TItem>> expectations)
		=> new(source.ExpectationBuilder.AddConstraint(it
			=> new SyncCollectionConstraint<TItem>(it, EnumerableQuantifier.AtMost(maximum), expectations)), source);
}
