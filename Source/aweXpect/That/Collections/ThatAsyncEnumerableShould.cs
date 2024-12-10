#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="IAsyncEnumerable{T}" />.
/// </summary>
public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Start delegate expectations on the current async enumerable of <typeparamref name="TItem" /> values.
	/// </summary>
	public static IThat<IAsyncEnumerable<TItem>> Should<TItem>(
		this IExpectSubject<IAsyncEnumerable<TItem>> subject)
		=> subject.Should(That.WithoutAction);

	private readonly struct AsyncCollectionConstraint<TItem> : IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		private readonly string _it;
		private readonly EnumerableQuantifier _quantifier;
		private readonly ManualExpectationBuilder<TItem> _itemExpectationBuilder;

		public AsyncCollectionConstraint(string it,
			EnumerableQuantifier quantifier,
			Action<IThat<TItem>> expectations)
		{
			_it = it;
			_quantifier = quantifier;
			_itemExpectationBuilder = new ManualExpectationBuilder<TItem>();
			expectations.Invoke(new That.Subject<TItem>(_itemExpectationBuilder).Should(_ => { }));
		}

		public async Task<ConstraintResult> IsMetBy(
			IAsyncEnumerable<TItem> actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			IAsyncEnumerable<TItem> materialized =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			int matchingCount = 0;
			int notMatchingCount = 0;
			int? totalCount = null;

			await foreach (TItem item in materialized.WithCancellation(cancellationToken))
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

				if (_quantifier.IsDeterminable(matchingCount, notMatchingCount))
				{
					return _quantifier.GetResult(actual, _it, _itemExpectationBuilder, matchingCount, notMatchingCount,
						totalCount);
				}
			}

			if (cancellationToken.IsCancellationRequested)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(
					actual, _quantifier.GetExpectation(_it, _itemExpectationBuilder),
					"could not verify, because it was cancelled early");
			}

			return _quantifier.GetResult(actual, _it, _itemExpectationBuilder, matchingCount, notMatchingCount,
				matchingCount + notMatchingCount);
		}
	}

	private readonly struct AsyncCollectionCountConstraint<TItem>(
		string it,
		EnumerableQuantifier quantifier) : IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		public async Task<ConstraintResult> IsMetBy(
			IAsyncEnumerable<TItem> actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			IAsyncEnumerable<TItem> materialized =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			int matchingCount = 0;
			int notMatchingCount = 0;
			int? totalCount = null;

			await foreach (TItem _ in materialized.WithCancellation(cancellationToken))
			{
				matchingCount++;

				if (quantifier.IsDeterminable(matchingCount, notMatchingCount))
				{
					return quantifier.GetResult(actual, it, null, matchingCount, notMatchingCount,
						totalCount);
				}
			}

			if (cancellationToken.IsCancellationRequested)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(
					actual, quantifier.GetExpectation(it, null),
					"could not verify, because it was cancelled early");
			}

			return quantifier.GetResult(actual, it, null, matchingCount, notMatchingCount,
				matchingCount + notMatchingCount);
		}
	}
}

#endif
