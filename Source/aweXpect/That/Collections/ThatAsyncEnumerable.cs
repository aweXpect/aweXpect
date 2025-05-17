#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;
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
	private sealed class CollectionConstraint<TItem>
		: ConstraintResult.WithValue<IAsyncEnumerable<TItem>?>,
			IAsyncContextConstraint<IAsyncEnumerable<TItem>?>
	{
		private readonly ExpectationBuilder _expectationBuilder;
		private readonly Func<ExpectationGrammars, string> _expectationText;
		private readonly ExpectationGrammars _grammars;
		private readonly string _it;
		private readonly Func<TItem, bool> _predicate;
		private readonly EnumerableQuantifier _quantifier;
		private readonly string _verb;
		private LimitedCollection<TItem>? _items;
		private int _matchingCount;
		private LimitedCollection<TItem>? _matchingItems;
		private int _notMatchingCount;
		private LimitedCollection<TItem>? _notMatchingItems;
		private int? _totalCount;

		public CollectionConstraint(
			ExpectationBuilder expectationBuilder,
			string it,
			ExpectationGrammars grammars,
			EnumerableQuantifier quantifier,
			Func<ExpectationGrammars, string> expectationText,
			Func<TItem, bool> predicate,
			string verb) : base(grammars)
		{
			_expectationBuilder = expectationBuilder;
			_it = it;
			_grammars = grammars;
			_quantifier = quantifier;
			_expectationText = expectationText;
			_predicate = predicate;
			_verb = verb;
		}

		public async Task<ConstraintResult> IsMetBy(
			IAsyncEnumerable<TItem>? actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			IAsyncEnumerable<TItem> materialized =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			_matchingCount = 0;
			_notMatchingCount = 0;
			int maxItems = Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get() + 1;
			_items = new LimitedCollection<TItem>(maxItems);
			_matchingItems = new LimitedCollection<TItem>(maxItems);
			_notMatchingItems = new LimitedCollection<TItem>(maxItems);

			await foreach (TItem item in materialized.WithCancellation(cancellationToken))
			{
				if (_predicate(item))
				{
					_matchingCount++;
					_matchingItems.Add(item);
				}
				else
				{
					_notMatchingCount++;
					_notMatchingItems.Add(item);
				}

				_items.Add(item);

				if (_quantifier.IsDeterminable(_matchingCount, _notMatchingCount) && _items.IsReadOnly)
				{
					Outcome = _quantifier.GetOutcome(_matchingCount, _notMatchingCount, _totalCount);
					AppendContexts(true);
					AppendCollectionContext(_items, true);
					return this;
				}
			}

			if (cancellationToken.IsCancellationRequested)
			{
				Outcome = Outcome.Undecided;
				AppendCollectionContext(_items, true);
				return this;
			}

			_totalCount = _matchingCount + _notMatchingCount;
			Outcome = _quantifier.GetOutcome(_matchingCount, _notMatchingCount, _totalCount);
			AppendContexts(false);
			AppendCollectionContext(_items, false);
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_grammars.HasFlag(ExpectationGrammars.Nested))
			{
				stringBuilder.Append(_quantifier);
				stringBuilder.Append(' ');
				stringBuilder.Append(_expectationText(_grammars));
			}
			else
			{
				stringBuilder.Append(_expectationText(_grammars));
				stringBuilder.Append(" for ");
				stringBuilder.Append(_quantifier);
				stringBuilder.Append(' ');
				stringBuilder.Append(_quantifier.GetItemString());
			}
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (Actual is null)
			{
				stringBuilder.ItWasNull(_it);
			}
			else
			{
				_quantifier.AppendResult(stringBuilder, _grammars, _matchingCount, _notMatchingCount, _totalCount,
					_verb);
			}
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_grammars.HasFlag(ExpectationGrammars.Nested))
			{
				stringBuilder.Append("not ");
				stringBuilder.Append(_quantifier);
				stringBuilder.Append(' ');
				stringBuilder.Append(_expectationText(_grammars));
			}
			else
			{
				stringBuilder.Append(_expectationText(_grammars.Negate()));
				stringBuilder.Append(" for ");
				stringBuilder.Append(_quantifier);
				stringBuilder.Append(' ');
				stringBuilder.Append(_quantifier.GetItemString());
			}
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (Actual is null)
			{
				stringBuilder.ItWasNull(_it);
			}
			else
			{
				_quantifier.AppendResult(stringBuilder, _grammars.Negate(), _matchingCount, _notMatchingCount,
					_totalCount, _verb);
			}
		}

		private void AppendContexts(bool isIncomplete)
		{
			EnumerableQuantifier.QuantifierContext quantifierContext = _quantifier.GetQuantifierContext();
			if (quantifierContext.HasFlag(EnumerableQuantifier.QuantifierContext.MatchingItems))
			{
				_expectationBuilder.UpdateContexts(contexts => contexts
					.Add(new ResultContext("Matching items",
						AppendIsIncomplete(
							Formatter.Format(_matchingItems, typeof(TItem).GetFormattingOption()),
							isIncomplete),
						int.MaxValue)));
			}

			if (quantifierContext.HasFlag(EnumerableQuantifier.QuantifierContext.NotMatchingItems))
			{
				_expectationBuilder.UpdateContexts(contexts => contexts
					.Add(new ResultContext("Not matching items",
						AppendIsIncomplete(
							Formatter.Format(_notMatchingItems, typeof(TItem).GetFormattingOption()),
							isIncomplete),
						int.MaxValue)));
			}
		}

		private void AppendCollectionContext(IEnumerable<TItem> value, bool isIncomplete)
			=> _expectationBuilder.UpdateContexts(contexts
				=> contexts
					.Add(new ResultContext("Collection",
						AppendIsIncomplete(
							Formatter.Format(value, typeof(TItem).GetFormattingOption()),
							isIncomplete),
						1)));

		private static string AppendIsIncomplete(string formattedItems, bool isIncomplete)
		{
			if (!isIncomplete || formattedItems.Length < 3)
			{
				return formattedItems;
			}

			if (formattedItems.EndsWith("…]"))
			{
				return $"{formattedItems[..^2]}(… and maybe others)]";
			}

			if (formattedItems.EndsWith("…\r\n]"))
			{
				return $"""
				        {formattedItems[..^4]}(… and maybe others)
				        ]
				        """;
			}

			if (formattedItems.EndsWith("\r\n]"))
			{
				return $"""
				        {formattedItems[..^3]},
				          (… and maybe others)
				        ]
				        """;
			}

			return $"""
			        {formattedItems[..^1]}, (… and maybe others)]
			        """;
		}
	}

	private sealed class AsyncCollectionCountConstraint<TItem>
		: ConstraintResult.WithNotNullValue<IAsyncEnumerable<TItem>?>,
			IAsyncContextConstraint<IAsyncEnumerable<TItem>?>
	{
		private readonly ExpectationGrammars _grammars;
		private readonly EnumerableQuantifier _quantifier;
		private int _matchingCount;
		private int _notMatchingCount;
		private int? _totalCount;

		public AsyncCollectionCountConstraint(string it, ExpectationGrammars grammars, EnumerableQuantifier quantifier)
			: base(it, grammars)
		{
			_grammars = grammars;
			_quantifier = quantifier;
		}

		public async Task<ConstraintResult> IsMetBy(
			IAsyncEnumerable<TItem>? actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			IAsyncEnumerable<TItem> materialized =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			_matchingCount = 0;
			_notMatchingCount = 0;

			await foreach (TItem _ in materialized.WithCancellation(cancellationToken))
			{
				_matchingCount++;

				if (_quantifier.IsDeterminable(_matchingCount, _notMatchingCount))
				{
					Outcome = _quantifier.GetOutcome(_matchingCount, _notMatchingCount, _totalCount);
					return this;
				}
			}

			if (cancellationToken.IsCancellationRequested)
			{
				Outcome = Outcome.Undecided;
				return this;
			}

			_totalCount = _matchingCount + _notMatchingCount;
			Outcome = _quantifier.GetOutcome(_matchingCount, _notMatchingCount, _totalCount);
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("has ");
			stringBuilder.Append(_quantifier);
			stringBuilder.Append(' ');
			stringBuilder.Append(_quantifier.GetItemString());
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (Actual is null)
			{
				stringBuilder.ItWasNull(It);
			}
			else
			{
				_quantifier.AppendResult(stringBuilder, _grammars, _matchingCount, _notMatchingCount, _totalCount);
			}
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("does not have ");
			stringBuilder.Append(_quantifier);
			stringBuilder.Append(' ');
			stringBuilder.Append(_quantifier.GetItemString());
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (Actual is null)
			{
				stringBuilder.ItWasNull(It);
			}
			else
			{
				_quantifier.AppendResult(stringBuilder, _grammars.Negate(), _matchingCount, _notMatchingCount,
					_totalCount);
			}
		}
	}

	private sealed class IsEqualToConstraint<TItem, TMatch>(
		string it,
		ExpectationGrammars grammars,
		string expectedExpression,
		IEnumerable<TItem>? expected,
		IOptionsEquality<TMatch> options,
		CollectionMatchOptions matchOptions)
		: ConstraintResult.WithEqualToValue<IAsyncEnumerable<TItem>?>(it, grammars, expected is null),
			IAsyncContextConstraint<IAsyncEnumerable<TItem>?>
		where TItem : TMatch
	{
		private string? _failure;
		private List<TItem>? _items = [];

		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem>? actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			Actual = actual;
			if (actual is null || expected is null)
			{
				Outcome = actual is null && expected is null ? Outcome.Success : Outcome.Failure;
				return this;
			}

			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			ICollectionMatcher<TItem, TMatch> matcher = matchOptions.GetCollectionMatcher<TItem, TMatch>(expected);
			int maximumNumber = Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get();
			if (IsNegated)
			{
				_items = [];
			}

			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				if (_items?.Count < maximumNumber + 1)
				{
					_items.Add(item);
				}

				if (matcher.Verify(It, item, options, maximumNumber, out _failure))
				{
					_failure ??= await TooManyDeviationsError(materializedEnumerable);
					Outcome = Outcome.Failure;
					return this;
				}
			}

			if (matcher.VerifyComplete(It, options, maximumNumber, out _failure))
			{
				_failure ??= await TooManyDeviationsError(materializedEnumerable);
				Outcome = Outcome.Failure;
				return this;
			}

			Outcome = Outcome.Success;
			return this;
		}

		private async Task<string> TooManyDeviationsError(IAsyncEnumerable<TItem> materializedEnumerable)
		{
			StringBuilder sb = new();
			sb.Append(It);
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

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(matchOptions.GetExpectation(expectedExpression, Grammars));
			stringBuilder.Append(options);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (expected is null)
			{
				stringBuilder.Append(It).Append(" cannot compare to <null>");
			}
			else if (_failure is not null)
			{
				stringBuilder.Append(_failure);
			}
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalExpectation(stringBuilder, indentation);

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (expected is null)
			{
				stringBuilder.Append(It).Append(" cannot compare to <null>");
			}
			else
			{
				stringBuilder.Append(It).Append(" did in ");
				Formatter.Format(stringBuilder, _items, FormattingOptions.MultipleLines);
			}
		}
	}

	private sealed class IsInOrderConstraint<TItem, TMember>(
		string it,
		ExpectationGrammars grammars,
		Func<TItem, TMember> memberAccessor,
		SortOrder sortOrder,
		CollectionOrderOptions<TMember> options,
		string memberExpression)
		: ConstraintResult.WithNotNullValue<IAsyncEnumerable<TItem>?>(it, grammars),
			IAsyncContextConstraint<IAsyncEnumerable<TItem>?>
	{
		private string? _failureText;
		private List<TItem>? _values;

		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem>? actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			IAsyncEnumerable<TItem> materialized = context
				.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);

			TMember previous = default!;
			int index = 0;
			int maximumNumberOfCollectionItems =
				Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get();
			IComparer<TMember> comparer = options.GetComparer();
			_values = new List<TItem>(maximumNumberOfCollectionItems + 1);
			await foreach (TItem item in materialized.WithCancellation(cancellationToken))
			{
				if (_values.Count <= maximumNumberOfCollectionItems)
				{
					_values.Add(item);
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
					_failureText ??=
						$"{It} had {Formatter.Format(previous)} before {Formatter.Format(current)} which is not in {sortOrder.ToString().ToLower()} order in ";
				}

				if (_failureText != null && _values.Count > maximumNumberOfCollectionItems)
				{
					break;
				}

				previous = current;
			}

			Outcome = _failureText != null ? Outcome.Failure : Outcome.Success;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is in ").Append(sortOrder.ToString().ToLower()).Append(" order");
			stringBuilder.Append(options).Append(memberExpression);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(_failureText);
			Formatter.Format(stringBuilder, _values, FormattingOptions.MultipleLines);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not in ").Append(sortOrder.ToString().ToLower()).Append(" order");
			stringBuilder.Append(options).Append(memberExpression);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was in ");
			Formatter.Format(stringBuilder, _values, FormattingOptions.MultipleLines);
		}
	}
}
#endif
