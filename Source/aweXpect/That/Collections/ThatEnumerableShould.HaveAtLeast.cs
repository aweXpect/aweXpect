﻿using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that at least <paramref name="minimum" /> items in the collection satisfy the
	///     <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>> HaveAtLeast<TItem>(
		this IThat<IEnumerable<TItem>> source,
		int minimum,
		Action<IThat<TItem>> expectations)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new SyncCollectionConstraint<TItem>(it, EnumerableQuantifier.AtLeast(minimum), expectations)),
			source);

	/// <summary>
	///     Verifies that the collection has at least <paramref name="minimum" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>> HaveAtLeast<TItem>(
		this IThat<IEnumerable<TItem>> source,
		int minimum)
		=> new(new AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new SyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.AtLeast(minimum))),
			source));
}
