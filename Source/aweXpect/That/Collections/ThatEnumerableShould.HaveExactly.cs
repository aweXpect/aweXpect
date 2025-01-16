using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that exactly <paramref name="expected" /> items in the collection satisfy the
	///     <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>> HaveExactly<TItem>(
		this IThatShould<IEnumerable<TItem>> source,
		int expected,
		Action<IThatShould<TItem>> expectations)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new SyncCollectionConstraint<TItem>(it, EnumerableQuantifier.Exactly(expected), expectations)),
			source);

	/// <summary>
	///     Verifies that the collection has exactly <paramref name="expected" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>>> HaveExactly<TItem>(
		this IThatShould<IEnumerable<TItem>> source,
		int expected)
		=> new(new AndOrResult<IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new SyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.Exactly(expected))),
			source));
}
