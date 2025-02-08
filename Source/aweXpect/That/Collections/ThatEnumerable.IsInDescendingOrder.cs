using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the collection is in descending order.
	/// </summary>
	public static CollectionOrderResult<TItem, IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
		IsInDescendingOrder<TItem>(
			this IThat<IEnumerable<TItem>?> source)
	{
		CollectionOrderOptions<TItem> options = new();
		return new CollectionOrderResult<TItem, IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new IsInOrderConstraint<TItem, TItem>(it, x => x, SortOrder.Descending, options, "")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is in descending order.
	/// </summary>
	public static CollectionOrderResult<TMember, IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
		IsInDescendingOrder<
			TItem, TMember>(
			this IThat<IEnumerable<TItem>?> source,
			Func<TItem, TMember> memberAccessor,
			[CallerArgumentExpression("memberAccessor")]
			string doNotPopulateThisValue = "")
	{
		CollectionOrderOptions<TMember> options = new();
		return new CollectionOrderResult<TMember, IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new IsInOrderConstraint<TItem, TMember>(it, memberAccessor, SortOrder.Descending, options,
					$" for {doNotPopulateThisValue}")),
			source,
			options);
	}
}
