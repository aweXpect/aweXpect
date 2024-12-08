using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that no items in the synchronous enumerable satisfy the <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>> HaveNone<TItem>(
		this IThat<IEnumerable<TItem>> source,
		Action<IThat<TItem>> expectations)
		=> new(source.ExpectationBuilder.AddConstraint(it
			=> new SyncCollectionConstraint<TItem>(it, EnumerableQuantifier.None, expectations)), source);
}
