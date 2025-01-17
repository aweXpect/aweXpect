#if NET8_0_OR_GREATER
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
public static partial class ThatAsyncEnumerable
{
	private readonly struct CollectionConstraint<TItem> : IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		private readonly string _it;
		private readonly EnumerableQuantifier _quantifier;
		private readonly Func<string> _expectationText;
		private readonly Func<TItem, bool> _predicate;
		private readonly string _verb;

		public CollectionConstraint(string it,
			EnumerableQuantifier quantifier,
			Func<string> expectationText,
			Func<TItem, bool> predicate,
			string verb)
		{
			_it = it;
			_quantifier = quantifier;
			_expectationText = expectationText;
			_predicate = predicate;
			_verb = verb;
		}

		public async Task<ConstraintResult> IsMetBy(
			IAsyncEnumerable<TItem> actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(
					actual!,
					_quantifier.GetExpectation(_it, _expectationText()),
					$"{_it} was <null>");
			}

			IAsyncEnumerable<TItem> materialized =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			int matchingCount = 0;
			int notMatchingCount = 0;
			int? totalCount = null;

			await foreach (TItem item in materialized.WithCancellation(cancellationToken))
			{
				if (_predicate(item))
				{
					matchingCount++;
				}
				else
				{
					notMatchingCount++;
				}

				if (_quantifier.IsDeterminable(matchingCount, notMatchingCount))
				{
					return _quantifier.GetResult(actual, _it, _expectationText(), matchingCount, notMatchingCount,
						totalCount, _verb);
				}
			}

			if (cancellationToken.IsCancellationRequested)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(
					actual, _quantifier.GetExpectation(_it, _expectationText()),
					"could not verify, because it was cancelled early");
			}

			return _quantifier.GetResult(actual, _it, _expectationText(), matchingCount, notMatchingCount,
				matchingCount + notMatchingCount, _verb);
		}
	}
	private readonly struct AsyncCollectionConstraint<TItem> : IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		private readonly string _it;
		private readonly EnumerableQuantifier _quantifier;
		private readonly ManualExpectationBuilder<TItem> _itemExpectationBuilder;

		public AsyncCollectionConstraint(string it,
			EnumerableQuantifier quantifier,
			Action<IExpectSubject<TItem>> expectations)
		{
			_it = it;
			_quantifier = quantifier;
			_itemExpectationBuilder = new ManualExpectationBuilder<TItem>();
			expectations.Invoke(new That.Subject<TItem>(_itemExpectationBuilder));
		}

		public async Task<ConstraintResult> IsMetBy(
			IAsyncEnumerable<TItem> actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(
					actual!,
					_quantifier.GetExpectation(_it, _itemExpectationBuilder.ToString()),
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
					return _quantifier.GetResult(actual, _it, _itemExpectationBuilder.ToString(), matchingCount, notMatchingCount,
						totalCount, null);
				}
			}

			if (cancellationToken.IsCancellationRequested)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(
					actual, _quantifier.GetExpectation(_it, _itemExpectationBuilder.ToString()),
					"could not verify, because it was cancelled early");
			}

			return _quantifier.GetResult(actual, _it, _itemExpectationBuilder.ToString(), matchingCount, notMatchingCount,
				matchingCount + notMatchingCount, null);
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
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(
					actual!,
					quantifier.GetExpectation(it, null),
					$"{it} was <null>");
			}

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
						totalCount, null);
				}
			}

			if (cancellationToken.IsCancellationRequested)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(
					actual, quantifier.GetExpectation(it, null),
					"could not verify, because it was cancelled early");
			}

			return quantifier.GetResult(actual, it, null, matchingCount, notMatchingCount,
				matchingCount + notMatchingCount, null);
		}
	}

	private readonly struct BeConstraint<TItem, TMatch>(
		string it,
		string expectedExpression,
		IEnumerable<TItem>? expected,
		IOptionsEquality<TMatch> options,
		CollectionMatchOptions matchOptions)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
		where TItem : TMatch
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual!, ToString(), $"{it} was <null>");
			}

			if (expected is null)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
					$"{it} cannot compare to <null>");
			}

			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			ICollectionMatcher<TItem, TMatch> matcher = matchOptions.GetCollectionMatcher<TItem, TMatch>(expected);
			int maximumNumber = Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get();
			
			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				if (matcher.Verify(it, item, options, maximumNumber, out string? failure))
				{
					return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
						failure ?? await TooManyDeviationsError(materializedEnumerable));
				}
			}

			if (matcher.VerifyComplete(it, options, maximumNumber, out string? lastFailure))
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
			int maximumNumberOfCollectionItems =
				Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get();
			await foreach (TItem item in materializedEnumerable)
			{
				if (count++ >= maximumNumberOfCollectionItems)
				{
					break;
				}

				sb.AppendLine();
				sb.Append("  ");
				Formatter.Format(sb, item);
				sb.Append(',');
			}

			if (count > maximumNumberOfCollectionItems)
			{
				sb.AppendLine();
				sb.Append("  …,");
			}

			sb.Length--;
			sb.AppendLine();
			sb.Append("] had more than ");
			sb.Append(2 * maximumNumberOfCollectionItems);
			sb.Append(" deviations compared to ");
			Formatter.Format(sb, expected?.Take(maximumNumberOfCollectionItems + 1),
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
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual!, ToString(), $"{it} was <null>");
			}

			IAsyncEnumerable<TItem> materialized = context
				.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);

			TMember previous = default!;
			int index = 0;
			int maximumNumberOfCollectionItems =
				Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get();
			IComparer<TMember> comparer = options.GetComparer();
			List<TItem> values = new(maximumNumberOfCollectionItems + 1);
			string? failureText = null;
			await foreach (TItem item in materialized.WithCancellation(cancellationToken))
			{
				if (values.Count <= maximumNumberOfCollectionItems)
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

				if (failureText != null && values.Count > maximumNumberOfCollectionItems)
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
