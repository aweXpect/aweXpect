using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;
#if NET8_0_OR_GREATER
using System.Collections.Immutable;
#endif

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the collection is in ascending order.
	/// </summary>
	public static CollectionOrderResult<TItem, IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
		IsInAscendingOrder<TItem>(
			this IThat<IEnumerable<TItem>?> source)
	{
		CollectionOrderOptions<TItem> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<TItem, IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderConstraint<TItem, TItem>(
					expectationBuilder, it, grammars,
					x => x,
					SortOrder.Ascending,
					options,
					"")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is in ascending order.
	/// </summary>
	public static CollectionOrderResult<TMember, IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
		IsInAscendingOrder<
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
				=> new IsInOrderConstraint<TItem, TMember>(
					expectationBuilder, it, grammars,
					memberAccessor,
					SortOrder.Ascending,
					options,
					$" for {doNotPopulateThisValue.TrimCommonWhiteSpace()}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is in ascending order.
	/// </summary>
	public static CollectionOrderResult<object?, TEnumerable, IThat<TEnumerable?>>
		IsInAscendingOrder<TEnumerable>(this IThat<TEnumerable?> source)
		where TEnumerable : IEnumerable
	{
		CollectionOrderOptions<object?> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<object?, TEnumerable, IThat<TEnumerable?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderForEnumerableConstraint<TEnumerable, object?>(
					expectationBuilder, it, grammars,
					x => x,
					SortOrder.Ascending,
					options,
					"")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is in ascending order.
	/// </summary>
	public static CollectionOrderResult<TMember, TEnumerable, IThat<TEnumerable?>>
		IsInAscendingOrder<TEnumerable, TMember>(
			this IThat<TEnumerable?> source,
			Func<object?, TMember> memberAccessor,
			[CallerArgumentExpression("memberAccessor")]
			string doNotPopulateThisValue = "")
		where TEnumerable : IEnumerable
	{
		CollectionOrderOptions<TMember> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<TMember, TEnumerable, IThat<TEnumerable?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderForEnumerableConstraint<TEnumerable, TMember>(
					expectationBuilder, it, grammars,
					memberAccessor,
					SortOrder.Ascending,
					options,
					$" for {doNotPopulateThisValue.TrimCommonWhiteSpace()}")),
			source,
			options);
	}

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection is in ascending order.
	/// </summary>
	public static CollectionOrderResult<TItem, ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>>
		IsInAscendingOrder<TItem>(
			this IThat<ImmutableArray<TItem>> source)
	{
		CollectionOrderOptions<TItem> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<TItem, ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderForEnumerableConstraint<ImmutableArray<TItem>, TItem>(
					expectationBuilder, it, grammars,
					x => (TItem)x!,
					SortOrder.Ascending,
					options,
					"")),
			source,
			options);
	}
#endif

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection is in ascending order.
	/// </summary>
	public static CollectionOrderResult<TMember, ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>>
		IsInAscendingOrder<
			TItem, TMember>(
			this IThat<ImmutableArray<TItem>> source,
			Func<TItem, TMember> memberAccessor,
			[CallerArgumentExpression("memberAccessor")]
			string doNotPopulateThisValue = "")
	{
		CollectionOrderOptions<TMember> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<TMember, ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderConstraint<TItem, TMember>(
					expectationBuilder, it, grammars,
					memberAccessor,
					SortOrder.Ascending,
					options,
					$" for {doNotPopulateThisValue.TrimCommonWhiteSpace()}")),
			source,
			options);
	}
#endif

	/// <summary>
	///     Verifies that the collection is not in ascending order.
	/// </summary>
	public static CollectionOrderResult<TItem, IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
		IsNotInAscendingOrder<TItem>(
			this IThat<IEnumerable<TItem>?> source)
	{
		CollectionOrderOptions<TItem> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<TItem, IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderConstraint<TItem, TItem>(
					expectationBuilder, it, grammars,
					x => x,
					SortOrder.Ascending,
					options,
					"").Invert()),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is not in ascending order.
	/// </summary>
	public static CollectionOrderResult<TMember, IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
		IsNotInAscendingOrder<
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
				=> new IsInOrderConstraint<TItem, TMember>(
					expectationBuilder, it, grammars,
					memberAccessor,
					SortOrder.Ascending,
					options,
					$" for {doNotPopulateThisValue.TrimCommonWhiteSpace()}").Invert()),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is not in ascending order.
	/// </summary>
	public static CollectionOrderResult<object?, TEnumerable, IThat<TEnumerable?>>
		IsNotInAscendingOrder<TEnumerable>(this IThat<TEnumerable?> source)
		where TEnumerable : IEnumerable
	{
		CollectionOrderOptions<object?> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<object?, TEnumerable, IThat<TEnumerable?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderForEnumerableConstraint<TEnumerable, object?>(
					expectationBuilder, it, grammars,
					x => x,
					SortOrder.Ascending,
					options,
					"").Invert()),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is not in ascending order.
	/// </summary>
	public static CollectionOrderResult<TMember, TEnumerable, IThat<TEnumerable?>>
		IsNotInAscendingOrder<TEnumerable, TMember>(
			this IThat<TEnumerable?> source,
			Func<object?, TMember> memberAccessor,
			[CallerArgumentExpression("memberAccessor")]
			string doNotPopulateThisValue = "")
		where TEnumerable : IEnumerable
	{
		CollectionOrderOptions<TMember> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<TMember, TEnumerable, IThat<TEnumerable?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderForEnumerableConstraint<TEnumerable, TMember>(
					expectationBuilder, it, grammars,
					memberAccessor,
					SortOrder.Ascending,
					options,
					$" for {doNotPopulateThisValue.TrimCommonWhiteSpace()}").Invert()),
			source,
			options);
	}

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection is not in ascending order.
	/// </summary>
	public static CollectionOrderResult<TItem, ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>>
		IsNotInAscendingOrder<TItem>(this IThat<ImmutableArray<TItem>> source)
	{
		CollectionOrderOptions<TItem> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<TItem, ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderForEnumerableConstraint<ImmutableArray<TItem>, TItem>(
					expectationBuilder, it, grammars,
					x => (TItem)x!,
					SortOrder.Ascending,
					options,
					"").Invert()),
			source,
			options);
	}
#endif

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection is not in ascending order.
	/// </summary>
	public static CollectionOrderResult<TMember, ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>>
		IsNotInAscendingOrder<TItem, TMember>(
			this IThat<ImmutableArray<TItem>> source,
			Func<TItem, TMember> memberAccessor,
			[CallerArgumentExpression("memberAccessor")]
			string doNotPopulateThisValue = "")
	{
		CollectionOrderOptions<TMember> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<TMember, ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderForEnumerableConstraint<ImmutableArray<TItem>, TMember>(
					expectationBuilder, it, grammars,
					o => memberAccessor((TItem)o!),
					SortOrder.Ascending,
					options,
					$" for {doNotPopulateThisValue.TrimCommonWhiteSpace()}").Invert()),
			source,
			options);
	}
#endif
}
