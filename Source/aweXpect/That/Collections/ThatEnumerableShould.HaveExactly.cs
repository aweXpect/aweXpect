using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that exactly <paramref name="expected" /> items in the synchronous enumerable satisfy the
	///     <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>> HaveExactly<TItem>(
		this IThat<IEnumerable<TItem>> source,
		int expected,
		Action<IThat<TItem>> expectations)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new SyncCollectionConstraint<TItem>(it, EnumerableQuantifier.Exactly(expected), expectations)),
			source);

	/// <summary>
	///     Verifies that the synchronous enumerable has exactly <paramref name="expected" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>> HaveExactly<TItem>(
		this IThat<IEnumerable<TItem>> source,
		int expected)
		=> new(new AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new SyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.Exactly(expected))),
			source));
}
