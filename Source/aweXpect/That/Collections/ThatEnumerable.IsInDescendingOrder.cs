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
				=> new IsInOrderConstraint<TItem, TItem>(
					expectationBuilder, it, grammars,
					x => x,
					aweXpect.SortOrder.Descending,
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
				=> new IsInOrderConstraint<TItem, TMember>(
					expectationBuilder, it, grammars,
					memberAccessor,
					aweXpect.SortOrder.Descending,
					options,
					$" for {doNotPopulateThisValue.TrimCommonWhiteSpace()}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is in descending order.
	/// </summary>
	public static CollectionOrderResult<object?, TEnumerable, IThat<TEnumerable?>>
		IsInDescendingOrder<TEnumerable>(this IThat<TEnumerable?> source)
		where TEnumerable : IEnumerable
	{
		CollectionOrderOptions<object?> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<object?, TEnumerable, IThat<TEnumerable?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderForEnumerableConstraint<TEnumerable, object?>(
					expectationBuilder, it, grammars,
					x => x,
					aweXpect.SortOrder.Descending,
					options,
					"")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is in descending order.
	/// </summary>
	public static CollectionOrderResult<TMember, TEnumerable, IThat<TEnumerable?>>
		IsInDescendingOrder<TEnumerable, TMember>(
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
					aweXpect.SortOrder.Descending,
					options,
					$" for {doNotPopulateThisValue.TrimCommonWhiteSpace()}")),
			source,
			options);
	}

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection is in descending order.
	/// </summary>
	public static CollectionOrderResult<TItem, ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>>
		IsInDescendingOrder<TItem>(
			this IThat<ImmutableArray<TItem>> source)
	{
		CollectionOrderOptions<TItem> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<TItem, ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderForEnumerableConstraint<ImmutableArray<TItem>, TItem>(
					expectationBuilder, it, grammars,
					x => (TItem)x!,
					aweXpect.SortOrder.Descending,
					options,
					"")),
			source,
			options);
	}
#endif

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection is in descending order.
	/// </summary>
	public static CollectionOrderResult<TMember, ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>>
		IsInDescendingOrder<
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
					aweXpect.SortOrder.Descending,
					options,
					$" for {doNotPopulateThisValue.TrimCommonWhiteSpace()}")),
			source,
			options);
	}
#endif

	/// <summary>
	///     Verifies that the collection is not in descending order.
	/// </summary>
	public static CollectionOrderResult<TItem, IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
		IsNotInDescendingOrder<TItem>(
			this IThat<IEnumerable<TItem>?> source)
	{
		CollectionOrderOptions<TItem> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<TItem, IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderConstraint<TItem, TItem>(
					expectationBuilder, it, grammars,
					x => x,
					aweXpect.SortOrder.Descending,
					options,
					"").Invert()),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is not in descending order.
	/// </summary>
	public static CollectionOrderResult<TMember, IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
		IsNotInDescendingOrder<
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
					aweXpect.SortOrder.Descending,
					options,
					$" for {doNotPopulateThisValue.TrimCommonWhiteSpace()}").Invert()),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is not in descending order.
	/// </summary>
	public static CollectionOrderResult<object?, TEnumerable, IThat<TEnumerable?>>
		IsNotInDescendingOrder<TEnumerable>(this IThat<TEnumerable?> source)
		where TEnumerable : IEnumerable
	{
		CollectionOrderOptions<object?> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<object?, TEnumerable, IThat<TEnumerable?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderForEnumerableConstraint<TEnumerable, object?>(
					expectationBuilder, it, grammars,
					x => x,
					aweXpect.SortOrder.Descending,
					options,
					"").Invert()),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection is not in descending order.
	/// </summary>
	public static CollectionOrderResult<TMember, TEnumerable, IThat<TEnumerable?>>
		IsNotInDescendingOrder<TEnumerable, TMember>(
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
					aweXpect.SortOrder.Descending,
					options,
					$" for {doNotPopulateThisValue.TrimCommonWhiteSpace()}").Invert()),
			source,
			options);
	}

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection is not in descending order.
	/// </summary>
	public static CollectionOrderResult<TItem, ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>>
		IsNotInDescendingOrder<TItem>(this IThat<ImmutableArray<TItem>> source)
	{
		CollectionOrderOptions<TItem> options = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new CollectionOrderResult<TItem, ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new IsInOrderForEnumerableConstraint<ImmutableArray<TItem>, TItem>(
					expectationBuilder, it, grammars,
					x => (TItem)x!,
					aweXpect.SortOrder.Descending,
					options,
					"").Invert()),
			source,
			options);
	}
#endif

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection is not in descending order.
	/// </summary>
	public static CollectionOrderResult<TMember, ImmutableArray<TItem>, IThat<ImmutableArray<TItem>>>
		IsNotInDescendingOrder<TItem, TMember>(
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
					aweXpect.SortOrder.Descending,
					options,
					$" for {doNotPopulateThisValue.TrimCommonWhiteSpace()}").Invert()),
			source,
			options);
	}
#endif
}
