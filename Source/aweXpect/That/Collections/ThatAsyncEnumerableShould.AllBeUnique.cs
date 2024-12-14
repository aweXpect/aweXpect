#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>> AllBeUnique<TItem, TMember>(
		this IThat<IAsyncEnumerable<TItem>> source,
		Func<TItem, TMember> memberAccessor,
		[CallerArgumentExpression("memberAccessor")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new AllBeUniqueWithPredicateConstraint<TItem, TMember>(it, memberAccessor, doNotPopulateThisValue, options)),
			source, options
		);
	}

	private readonly struct AllBeUniqueConstraint<TItem>(string it, ObjectEqualityOptions options)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context, CancellationToken cancellationToken)
		{
			IAsyncEnumerable<TItem> materialized = context
				.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			List<TItem> checkedItems = new();
			List<TItem> duplicates = new();

			ObjectEqualityOptions o = options;
			await foreach (TItem item in materialized.WithCancellation(cancellationToken))
			{
				foreach (TItem compareWith in checkedItems)
				{
					if (options.AreConsideredEqual(item, compareWith))
					{
						if (duplicates.All(x => !o.AreConsideredEqual(item, x)))
						{
							duplicates.Add(item);
						}
					}
				}

				checkedItems.Add(item);
			}

			if (duplicates.Any())
			{
				StringBuilder sb = new();
				sb.Append(it).Append(" contained ");
				if (duplicates.Count == 1)
				{
					sb.Append("1 duplicate:");
				}
				else
				{
					sb.Append(duplicates.Count).Append(" duplicates:");
				}

				foreach (TItem duplicate in duplicates)
				{
					sb.AppendLine();
					sb.Append("  ");
					Formatter.Format(sb, duplicate);
					sb.Append(',');
				}

				sb.Length--;

				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(), sb.ToString());
			}

			return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(actual,
				ToString());
		}

		public override string ToString() => "only have unique items";
	}

	private readonly struct AllBeUniqueWithPredicateConstraint<TItem, TMember>(string it, Func<TItem, TMember> memberAccessor, string memberAccessorExpression, ObjectEqualityOptions options)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context, CancellationToken cancellationToken)
		{
			IAsyncEnumerable<TItem> materialized = context
				.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			List<TMember> checkedItems = new();
			List<TMember> duplicates = new();

			ObjectEqualityOptions o = options;
			await foreach (TItem item in materialized.WithCancellation(cancellationToken))
			{
				var itemMember = memberAccessor(item);
				foreach (TMember compareWith in checkedItems)
				{
					if (options.AreConsideredEqual(itemMember, compareWith))
					{
						if (duplicates.All(x => !o.AreConsideredEqual(item, x)))
						{
							duplicates.Add(itemMember);
						}
					}
				}

				checkedItems.Add(itemMember);
			}

			if (duplicates.Any())
			{
				StringBuilder sb = new();
				sb.Append(it).Append(" contained ");
				if (duplicates.Count == 1)
				{
					sb.Append("1 duplicate:");
				}
				else
				{
					sb.Append(duplicates.Count).Append(" duplicates:");
				}

				foreach (TMember duplicate in duplicates)
				{
					sb.AppendLine();
					sb.Append("  ");
					Formatter.Format(sb, duplicate);
					sb.Append(',');
				}

				sb.Length--;

				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(), sb.ToString());
			}

			return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(actual,
				ToString());
		}

		public override string ToString() => $"only have unique items for {memberAccessorExpression}";
	}
}
#endif
