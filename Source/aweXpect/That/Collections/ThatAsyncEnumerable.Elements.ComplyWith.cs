﻿#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	public partial class Elements<TItem>
	{
		/// <summary>
		///     …comply with the <paramref name="expectations" />.
		/// </summary>
		public ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
			ComplyWith(Action<IThat<TItem>> expectations)
		{
			ObjectEqualityOptions<TItem> options = new();
			return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
					=> new ComplyWithConstraint<TItem>(it, _quantifier, expectations)),
				_subject,
				options);
		}
	}

	private readonly struct ComplyWithConstraint<TItem> : IAsyncContextConstraint<IAsyncEnumerable<TItem>?>
	{
		private readonly string _it;
		private readonly EnumerableQuantifier _quantifier;
		private readonly ManualExpectationBuilder<TItem> _itemExpectationBuilder;

		public ComplyWithConstraint(string it,
			EnumerableQuantifier quantifier,
			Action<IThat<TItem>> expectations)
		{
			_it = it;
			_quantifier = quantifier;
			_itemExpectationBuilder = new ManualExpectationBuilder<TItem>();
			expectations.Invoke(new ThatSubject<TItem>(_itemExpectationBuilder));
		}

		public async Task<ConstraintResult> IsMetBy(
			IAsyncEnumerable<TItem>? actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>?>(
					actual,
					$"{_itemExpectationBuilder} for {_quantifier} {_quantifier.GetItemString()}",
					$"{_it} was <null>");
			}

			IAsyncEnumerable<TItem> materialized =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			int matchingCount = 0;
			int notMatchingCount = 0;
			int? totalCount = null;

			await foreach (TItem item in materialized.WithCancellation(cancellationToken))
			{
				ConstraintResult isMatch = await _itemExpectationBuilder.IsMetBy(item, context, cancellationToken);
				if (isMatch.Outcome == Outcome.Success)
				{
					matchingCount++;
				}
				else
				{
					notMatchingCount++;
				}

				if (_quantifier.IsDeterminable(matchingCount, notMatchingCount))
				{
					return _quantifier.GetResult(actual, _it, _itemExpectationBuilder.ToString(), matchingCount,
						notMatchingCount,
						totalCount, null);
				}
			}

			if (cancellationToken.IsCancellationRequested)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(
					actual,
					$"{_itemExpectationBuilder} for {_quantifier} {_quantifier.GetItemString()}",
					"could not verify, because it was cancelled early");
			}

			return _quantifier.GetResult(actual, _it, _itemExpectationBuilder.ToString(), matchingCount,
				notMatchingCount,
				matchingCount + notMatchingCount, null);
		}
	}
}
#endif
