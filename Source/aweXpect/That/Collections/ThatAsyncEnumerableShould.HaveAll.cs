﻿#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that all items in the collection satisfy the <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<IAsyncEnumerable<TItem>, IThatShould<IAsyncEnumerable<TItem>>> HaveAll<TItem>(
		this IThatShould<IAsyncEnumerable<TItem>> source,
		Action<IThatShould<TItem>> expectations)
		=> new(source.ExpectationBuilder.AddConstraint(it
			=> new AsyncCollectionConstraint<TItem>(it, EnumerableQuantifier.All, expectations)), source);
}
#endif
