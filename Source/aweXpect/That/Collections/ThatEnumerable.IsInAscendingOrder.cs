﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the collection is in ascending order.
	/// </summary>
	public static CollectionOrderResult<TItem, IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>> IsInAscendingOrder<TItem>(
		this IExpectSubject<IEnumerable<TItem>> source)
	{
		CollectionOrderOptions<TItem> options = new();
		return new CollectionOrderResult<TItem, IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new BeInOrderConstraint<TItem, TItem>(it, x => x, SortOrder.Ascending, options, "")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is in ascending order.
	/// </summary>
	public static CollectionOrderResult<TMember, IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>> IsInAscendingOrder<
		TItem, TMember>(
		this IExpectSubject<IEnumerable<TItem>> source,
		Func<TItem, TMember> memberAccessor,
		[CallerArgumentExpression("memberAccessor")]
		string doNotPopulateThisValue = "")
	{
		CollectionOrderOptions<TMember> options = new();
		return new CollectionOrderResult<TMember, IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new BeInOrderConstraint<TItem, TMember>(it, memberAccessor, SortOrder.Ascending, options,
					$" for {doNotPopulateThisValue}")),
			source,
			options);
	}
}