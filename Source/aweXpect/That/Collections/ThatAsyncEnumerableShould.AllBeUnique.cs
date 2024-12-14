#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that the collection only contains unique items.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>> AllBeUnique<TItem>(
		this IThat<IAsyncEnumerable<TItem>> source)
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new AllBeUniqueConstraint<TItem>(it, options)),
			source, options
		);
	}

	/// <summary>
	///     Verifies that the collection only contains unique items.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>> AllBeUnique<TItem,
		TMember>(
		this IThat<IAsyncEnumerable<TItem>> source,
		Func<TItem, TMember> memberAccessor,
		[CallerArgumentExpression("memberAccessor")]
		string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new AllBeUniqueWithPredicateConstraint<TItem, TMember>(it, memberAccessor, doNotPopulateThisValue,
					options)),
			source, options
		);
	}

	private readonly struct AllBeUniqueConstraint<TItem>(string it, ObjectEqualityOptions options)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			IAsyncEnumerable<TItem> materialized = context
				.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			List<TItem> checkedItems = new();
			List<TItem> duplicates = new();

			ObjectEqualityOptions o = options;
			await foreach (TItem item in materialized.WithCancellation(cancellationToken))
			{
				if (checkedItems.Any(compareWith =>
					    o.AreConsideredEqual(item, compareWith) &&
					    duplicates.All(x => !o.AreConsideredEqual(item, x))))
				{
					duplicates.Add(item);
				}

				checkedItems.Add(item);
			}

			if (duplicates.Any())
			{
				string failure = CollectionHelpers.CreateDuplicateFailureMessage(it, duplicates);
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(), failure);
			}

			return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(actual,
				ToString());
		}

		public override string ToString() => "only have unique items";
	}

	private readonly struct AllBeUniqueWithPredicateConstraint<TItem, TMember>(
		string it,
		Func<TItem, TMember> memberAccessor,
		string memberAccessorExpression,
		ObjectEqualityOptions options)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			IAsyncEnumerable<TItem> materialized = context
				.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			List<TMember> checkedItems = new();
			List<TMember> duplicates = new();

			ObjectEqualityOptions o = options;
			await foreach (TItem item in materialized.WithCancellation(cancellationToken))
			{
				TMember itemMember = memberAccessor(item);
				if (checkedItems.Any(compareWith =>
					    o.AreConsideredEqual(itemMember, compareWith) &&
					    duplicates.All(x => !o.AreConsideredEqual(itemMember, x))))
				{
					duplicates.Add(itemMember);
				}

				checkedItems.Add(itemMember);
			}

			if (duplicates.Any())
			{
				string failure = CollectionHelpers.CreateDuplicateFailureMessage(it, duplicates);
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(), failure);
			}

			return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(actual,
				ToString());
		}

		public override string ToString() => $"only have unique items for {memberAccessorExpression}";
	}
}
#endif
