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

// ReSharper disable PossibleMultipleEnumeration

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
				_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
					=> new ComplyWithConstraint(it, grammars, _quantifier, expectations)),
				_subject,
				options);
		}

		private sealed class ComplyWithConstraint
			: ConstraintResult.WithNotNullValue<IEnumerable<TItem>?>,
				IAsyncContextConstraint<IEnumerable<TItem>?>
		{
			private readonly ManualExpectationBuilder<TItem> _itemExpectationBuilder;
			private readonly EnumerableQuantifier _quantifier;
			private int _matchingCount;
			private int _notMatchingCount;
			private int? _totalCount;

			public ComplyWithConstraint(string it, ExpectationGrammars grammars,
				EnumerableQuantifier quantifier,
				Action<IThat<TItem>> expectations)
				: base(it, grammars)
			{
				_quantifier = quantifier;
				_itemExpectationBuilder = new ManualExpectationBuilder<TItem>();
				expectations.Invoke(new ThatSubject<TItem>(_itemExpectationBuilder));
			}

			public async Task<ConstraintResult> IsMetBy(
				IEnumerable<TItem>? actual,
				IEvaluationContext context,
				CancellationToken cancellationToken)
			{
				Actual = actual;
				if (actual is null)
				{
					Outcome = Outcome.Failure;
					return this;
				}

				IEnumerable<TItem> materialized = context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
				bool cancelEarly = actual is not ICollection<TItem>;
				_matchingCount = 0;
				_notMatchingCount = 0;

				foreach (TItem item in materialized)
				{
					ConstraintResult isMatch = await _itemExpectationBuilder.IsMetBy(item, context, cancellationToken);
					if (isMatch.Outcome == Outcome.Success)
					{
						_matchingCount++;
					}
					else
					{
						_notMatchingCount++;
					}

					if (cancelEarly && _quantifier.IsDeterminable(_matchingCount, _notMatchingCount))
					{
						Outcome = _quantifier.GetOutcome(_matchingCount, _notMatchingCount, _totalCount);
						return this;
					}

					if (cancellationToken.IsCancellationRequested)
					{
						Outcome = Outcome.Undecided;
						return this;
					}
				}

				_totalCount = _matchingCount + _notMatchingCount;
				Outcome = _quantifier.GetOutcome(_matchingCount, _notMatchingCount, _totalCount);
				return this;
			}

			protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			{
				_itemExpectationBuilder.AppendExpectation(stringBuilder, indentation);
				stringBuilder.Append(" for ");
				stringBuilder.Append(_quantifier);
				stringBuilder.Append(" items");
			}

			protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
				=> _quantifier.AppendResult(stringBuilder, Grammars, _matchingCount, _notMatchingCount, _totalCount);

			protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			{
				stringBuilder.Append(_quantifier);
				stringBuilder.Append(" for ");
				_itemExpectationBuilder.AppendExpectation(stringBuilder, indentation);
				stringBuilder.Append(" items");
			}

			protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
				=> _quantifier.AppendResult(stringBuilder, Grammars.Negate(), _matchingCount, _notMatchingCount,
					_totalCount);
		}
	}
}
