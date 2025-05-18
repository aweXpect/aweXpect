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
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<TItem, IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderConstraint<TItem, TItem>(expectationBuilder, it, grammars,
					x => x,
					SortOrder.Descending,
					options,
					"")),
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
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<TMember, IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderConstraint<TItem, TMember>(expectationBuilder, it, grammars,
					memberAccessor,
					SortOrder.Descending,
					options,
					$" for {doNotPopulateThisValue.TrimCommonWhiteSpace()}")),
			source,
			options);
	}
}
