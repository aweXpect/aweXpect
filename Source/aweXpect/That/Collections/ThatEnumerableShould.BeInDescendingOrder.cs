using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that the collection is in descending order.
	/// </summary>
	public static CollectionOrderResult<TItem, IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>>
		BeInDescendingOrder<TItem>(this IThatShould<IEnumerable<TItem>> source)
	{
		CollectionOrderOptions<TItem> options = new();
		return new CollectionOrderResult<TItem, IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeInOrderConstraint<TItem, TItem>(it, x => x, SortOrder.Descending, options, "")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is in descending order.
	/// </summary>
	public static CollectionOrderResult<TMember, IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>>
		BeInDescendingOrder<TItem, TMember>(this IThatShould<IEnumerable<TItem>> source,
			Func<TItem, TMember> memberAccessor,
			[CallerArgumentExpression("memberAccessor")]
			string doNotPopulateThisValue = "")
	{
		CollectionOrderOptions<TMember> options = new();
		return new CollectionOrderResult<TMember, IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeInOrderConstraint<TItem, TMember>(it, memberAccessor, SortOrder.Descending, options,
					$" for {doNotPopulateThisValue}")),
			source,
			options);
	}
}
