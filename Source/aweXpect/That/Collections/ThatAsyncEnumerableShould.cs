#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Customization;
using aweXpect.Helpers;
using aweXpect.Options;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="IAsyncEnumerable{TItem}" />.
/// </summary>
public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Start expectations on the collection of <typeparamref name="TItem" /> values.
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

	private readonly struct BeConstraint<TItem, TMatch>(
		string it,
		string expectedExpression,
		IEnumerable<TItem> expected,
		IOptionsEquality<TMatch> options,
		CollectionMatchOptions matchOptions)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
		where TItem : TMatch
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			ICollectionMatcher<TItem, TMatch> matcher = matchOptions.GetCollectionMatcher<TItem, TMatch>(expected);
			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				if (matcher.Verify(it, item, options, out string? failure))
				{
					return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
						failure ?? await TooManyDeviationsError(materializedEnumerable));
				}
			}

			if (matcher.VerifyComplete(it, options, out string? lastFailure))
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
					lastFailure ?? await TooManyDeviationsError(materializedEnumerable));
			}

			return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(materializedEnumerable,
				ToString());
		}

		private async Task<string> TooManyDeviationsError(IAsyncEnumerable<TItem> materializedEnumerable)
		{
			StringBuilder sb = new();
			sb.Append(it);
			sb.Append(" was completely different: [");
			int count = 0;
			await foreach (TItem item in materializedEnumerable)
			{
				if (count++ >= Customize.Formatting.MaximumNumberOfCollectionItems)
				{
					break;
				}

				sb.AppendLine();
				sb.Append("  ");
				Formatter.Format(sb, item);
				sb.Append(',');
			}

			if (count > Customize.Formatting.MaximumNumberOfCollectionItems)
			{
				sb.AppendLine();
				sb.Append("  …,");
			}

			sb.Length--;
			sb.AppendLine();
			sb.Append("] had more than ");
			sb.Append(2 * Customize.Formatting.MaximumNumberOfCollectionItems);
			sb.Append(" deviations compared to ");
			Formatter.Format(sb, expected.Take(Customize.Formatting.MaximumNumberOfCollectionItems + 1),
				FormattingOptions.MultipleLines);
			return sb.ToString();
		}

		public override string ToString()
			=> matchOptions.GetExpectation(expectedExpression);
	}

	private readonly struct BeInOrderConstraint<TItem, TMember>(
		string it,
		Func<TItem, TMember> memberAccessor,
		SortOrder sortOrder,
		CollectionOrderOptions<TMember> options,
		string memberExpression)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			IAsyncEnumerable<TItem> materialized = context
				.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);

			TMember previous = default!;
			int index = 0;
			IComparer<TMember> comparer = options.GetComparer();
			List<TItem> values = new(Customize.Formatting.MaximumNumberOfCollectionItems + 1);
			string? failureText = null;
			await foreach (TItem item in materialized.WithCancellation(cancellationToken))
			{
				if (values.Count <= Customize.Formatting.MaximumNumberOfCollectionItems)
				{
					values.Add(item);
				}

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
					failureText ??=
						$"{it} had {Formatter.Format(previous)} before {Formatter.Format(current)} which is not in {sortOrder.ToString().ToLower()} order in ";
				}

				if (failureText != null && values.Count > Customize.Formatting.MaximumNumberOfCollectionItems)
				{
					break;
				}

				previous = current;
			}

			if (failureText != null)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
					failureText + Formatter.Format(values, FormattingOptions.MultipleLines));
			}

			return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(actual, ToString());
		}

		public override string ToString()
			=> $"be in {sortOrder.ToString().ToLower()} order{options}{memberExpression}";
	}
}

#endif
