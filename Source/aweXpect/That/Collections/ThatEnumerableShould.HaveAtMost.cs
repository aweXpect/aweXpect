using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that at most <paramref name="maximum" /> items in the collection satisfy the
	///     <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>> HaveAtMost<TItem>(
		this IThatShould<IEnumerable<TItem>> source,
		int maximum,
		Action<IThatShould<TItem>> expectations)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new SyncCollectionConstraint<TItem>(it, EnumerableQuantifier.AtMost(maximum), expectations)),
			source);

	/// <summary>
	///     Verifies that the collection has at most <paramref name="maximum" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>>> HaveAtMost<TItem>(
		this IThatShould<IEnumerable<TItem>> source,
		int maximum)
		=> new(new AndOrResult<IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new SyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.AtMost(maximum))),
			source));
}
