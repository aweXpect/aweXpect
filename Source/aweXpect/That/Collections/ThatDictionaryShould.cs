using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Options;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="IDictionary{TKey, TValue}" />..
/// </summary>
public static partial class ThatDictionaryShould
{
	/// <summary>
	///     Start expectations on the current dictionary.
	/// </summary>
	public static IThat<IDictionary<TKey, TValue>> Should<TKey, TValue>(
		this IExpectSubject<IDictionary<TKey, TValue>> subject)
		=> subject.Should(That.WithoutAction);

	private readonly struct SyncCollectionConstraint<TItem> : IAsyncContextConstraint<IEnumerable<TItem>>
	{
		private readonly string _it;
		private readonly EnumerableQuantifier _quantifier;
		private readonly ManualExpectationBuilder<TItem> _itemExpectationBuilder;

		public SyncCollectionConstraint(string it,
			EnumerableQuantifier quantifier,
			Action<IThat<TItem>> expectations)
		{
			_it = it;
			_quantifier = quantifier;
			_itemExpectationBuilder = new ManualExpectationBuilder<TItem>();
			expectations.Invoke(new That.Subject<TItem>(_itemExpectationBuilder).Should(_ => { }));
		}

		public async Task<ConstraintResult> IsMetBy(
			IEnumerable<TItem> actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			IEnumerable<TItem> materialized = context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			bool cancelEarly = actual is not ICollection<TItem>;
			int matchingCount = 0;
			int notMatchingCount = 0;
			int? totalCount = null;

			foreach (TItem item in materialized)
			{
				ConstraintResult isMatch = await _itemExpectationBuilder.IsMetBy(item, context, cancellationToken);
				if (isMatch is ConstraintResult.Success)
				{
					matchingCount++;
				}
				else
				{
					notMatchingCount++;
				}

				if (cancelEarly && _quantifier.IsDeterminable(matchingCount, notMatchingCount))
				{
					return _quantifier.GetResult(actual, _it, _itemExpectationBuilder, matchingCount, notMatchingCount,
						totalCount);
				}

				if (cancellationToken.IsCancellationRequested)
				{
					return new ConstraintResult.Failure<IEnumerable<TItem>>(
						actual, _quantifier.GetExpectation(_it, _itemExpectationBuilder),
						"could not verify, because it was cancelled early");
				}
			}

			return _quantifier.GetResult(actual, _it, _itemExpectationBuilder, matchingCount, notMatchingCount,
				matchingCount + notMatchingCount);
		}
	}

	private readonly struct SyncCollectionCountConstraint<TItem> : IAsyncContextConstraint<IEnumerable<TItem>>
	{
		private readonly string _it;
		private readonly EnumerableQuantifier _quantifier;

		public SyncCollectionCountConstraint(string it,
			EnumerableQuantifier quantifier)
		{
			_it = it;
			_quantifier = quantifier;
		}

		public Task<ConstraintResult> IsMetBy(
			IEnumerable<TItem> actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			int matchingCount = 0;
			int notMatchingCount = 0;
			int? totalCount = null;

			if (actual is ICollection<TItem> collectionOfT)
			{
				matchingCount = collectionOfT.Count;
				totalCount = matchingCount;
				return Task.FromResult(_quantifier.GetResult(
					actual, _it, null, matchingCount, notMatchingCount, totalCount));
			}

			IEnumerable<TItem> materialized =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);

			foreach (TItem _ in materialized)
			{
				matchingCount++;

				if (_quantifier.IsDeterminable(matchingCount, notMatchingCount))
				{
					return Task.FromResult(_quantifier.GetResult(actual, _it, null, matchingCount, notMatchingCount,
						totalCount));
				}

				if (cancellationToken.IsCancellationRequested)
				{
					return Task.FromResult<ConstraintResult>(new ConstraintResult.Failure<IEnumerable<TItem>>(
						actual, _quantifier.GetExpectation(_it, null),
						"could not verify, because it was cancelled early"));
				}
			}

			totalCount = matchingCount + notMatchingCount;
			return Task.FromResult(_quantifier.GetResult(actual, _it, null, matchingCount, notMatchingCount,
				totalCount));
		}
	}

	private readonly struct BeInOrderConstraint<TItem, TMember>(
		string it,
		Func<TItem, TMember> memberAccessor,
		SortOrder sortOrder,
		CollectionOrderOptions<TMember> options,
		string memberExpression)
		: IContextConstraint<IEnumerable<TItem>>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem> actual, IEvaluationContext context)
		{
			IEnumerable<TItem> materialized = context
				.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);

			TMember previous = default!;
			int index = 0;
			IComparer<TMember> comparer = options.GetComparer();
			foreach (TItem item in materialized)
			{
				TMember current = memberAccessor(item);
				if (index++ == 0)
				{
					previous = current;
					continue;
				}

				int comparisonResult = comparer.Compare(previous, current);
				if ((comparisonResult > 0 && sortOrder == SortOrder.Ascending) ||
				    (comparisonResult < 0 && sortOrder == SortOrder.Descending))
				{
					return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
						$"{it} had {Formatter.Format(previous)} before {Formatter.Format(current)} which is not in {sortOrder.ToString().ToLower()} order in {Formatter.Format(materialized, FormattingOptions.MultipleLines)}");
				}

				previous = current;
			}

			return new ConstraintResult.Success<IEnumerable<TItem>>(actual,
				ToString());
		}

		public override string ToString()
			=> $"be in {sortOrder.ToString().ToLower()} order{options}{memberExpression}";
	}
}
