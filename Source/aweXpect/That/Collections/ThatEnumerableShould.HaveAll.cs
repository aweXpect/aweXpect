using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that all items in the collection satisfy the <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>> HaveAll<TItem>(
		this IThatShould<IEnumerable<TItem>> source,
		Action<IThatShould<TItem>> expectations)
		=> new(source.ExpectationBuilder.AddConstraint(it
			=> new SyncCollectionConstraint<TItem>(it, EnumerableQuantifier.All, expectations)), source);
	
	/// <summary>
	///     Verifies that all items in the collection satisfy the <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>> HasAll<TItem>(
		this IExpectSubject<IEnumerable<TItem>> source,
		Action<IExpectSubject<TItem>> expectations)
	{
		throw new NotImplementedException();
	}
}
