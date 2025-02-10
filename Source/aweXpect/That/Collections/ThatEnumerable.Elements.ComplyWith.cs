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

public static partial class ThatEnumerable
{
	public partial class Elements<TItem>
	{
		/// <summary>
		///     …comply with the <paramref name="expectations" />.
		/// </summary>
		public ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
			ComplyWith(Action<IThat<TItem>> expectations)
		{
			ObjectEqualityOptions<TItem> options = new();
			return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
					=> new ComplyWithConstraint(it, _quantifier, expectations)),
				_subject,
				options);
		}

		private readonly struct ComplyWithConstraint : IAsyncContextConstraint<IEnumerable<TItem>?>
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
				IEnumerable<TItem>? actual,
				IEvaluationContext context,
				CancellationToken cancellationToken)
			{
				if (actual is null)
				{
					return new ConstraintResult.Failure<IEnumerable<TItem>?>(
						actual,
						$"{_itemExpectationBuilder} for {_quantifier} items",
						$"{_it} was <null>");
				}

				IEnumerable<TItem> materialized = context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
				bool cancelEarly = actual is not ICollection<TItem>;
				int matchingCount = 0;
				int notMatchingCount = 0;
				int? totalCount = null;

				foreach (TItem item in materialized)
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

					if (cancelEarly && _quantifier.IsDeterminable(matchingCount, notMatchingCount))
					{
						return _quantifier.GetResult(actual, _it, _itemExpectationBuilder.ToString(), matchingCount,
							notMatchingCount,
							totalCount, null,
							(expectation, quantifier) => $"{quantifier} for {expectation} items");
					}

					if (cancellationToken.IsCancellationRequested)
					{
						return new ConstraintResult.Failure<IEnumerable<TItem>>(
							actual, $"{_itemExpectationBuilder} for {_quantifier} items",
							"could not verify, because it was cancelled early");
					}
				}

				return _quantifier.GetResult(actual, _it, _itemExpectationBuilder.ToString(), matchingCount,
					notMatchingCount,
					matchingCount + notMatchingCount, null,
					(expectation, quantifier) => $"{quantifier} for {expectation} items");
			}
		}
	}
}
