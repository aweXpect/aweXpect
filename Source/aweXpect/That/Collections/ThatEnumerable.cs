﻿using System;
using System.Collections.Generic;
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
///     Expectations on <see cref="IEnumerable{TItem}" />.
/// </summary>
public static partial class ThatEnumerable
{
	private readonly struct IsConstraint<TItem, TMatch>(
		string it,
		string expectedExpression,
		IEnumerable<TItem>? expected,
		IOptionsEquality<TMatch> options,
		CollectionMatchOptions matchOptions)
		: IContextConstraint<IEnumerable<TItem>?>
		where TItem : TMatch
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<IEnumerable<TItem>?>(actual, ToString(), $"{it} was <null>");
			}

			if (expected is null)
			{
				return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
					$"{it} cannot compare to <null>");
			}

			IEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			ICollectionMatcher<TItem, TMatch> matcher = matchOptions.GetCollectionMatcher<TItem, TMatch>(expected);
			int maximumNumber = Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get();

			foreach (TItem item in materializedEnumerable)
			{
				if (matcher.Verify(it, item, options, maximumNumber, out string? failure))
				{
					return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
						failure ?? TooManyDeviationsError(materializedEnumerable));
				}
			}

			if (matcher.VerifyComplete(it, options, maximumNumber, out string? lastFailure))
			{
				return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
					lastFailure ?? TooManyDeviationsError(materializedEnumerable));
			}

			return new ConstraintResult.Success<IEnumerable<TItem>>(materializedEnumerable,
				ToString());
		}

		private string TooManyDeviationsError(IEnumerable<TItem> materializedEnumerable)
			=> $"{it} was completely different: {Formatter.Format(materializedEnumerable, FormattingOptions.MultipleLines)} had more than {2 * Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get()} deviations compared to {Formatter.Format(expected, FormattingOptions.MultipleLines)}";

		public override string ToString()
			=> $"{matchOptions.GetExpectation(expectedExpression)}{options}";
	}

	private readonly struct CollectionConstraint<TItem> : IAsyncContextConstraint<IEnumerable<TItem>?>
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

		public Task<ConstraintResult> IsMetBy(
			IEnumerable<TItem>? actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			if (actual is null)
			{
				return Task.FromResult<ConstraintResult>(new ConstraintResult.Failure<IEnumerable<TItem>?>(
					actual,
					$"{_expectationText()} for {_quantifier} {_quantifier.GetItemString()}",
					$"{_it} was <null>"));
			}

			IEnumerable<TItem> materialized = context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			bool cancelEarly = actual is not ICollection<TItem>;
			int matchingCount = 0;
			int notMatchingCount = 0;
			int? totalCount = null;

			foreach (TItem item in materialized)
			{
				if (_predicate(item))
				{
					matchingCount++;
				}
				else
				{
					notMatchingCount++;
				}

				if (cancelEarly && _quantifier.IsDeterminable(matchingCount, notMatchingCount))
				{
					return Task.FromResult(_quantifier.GetResult(
						actual,
						_it,
						_expectationText(),
						matchingCount,
						notMatchingCount,
						totalCount,
						_verb));
				}

				if (cancellationToken.IsCancellationRequested)
				{
					return Task.FromResult<ConstraintResult>(new ConstraintResult.Failure<IEnumerable<TItem>>(
						actual,
						$"{_expectationText()} for {_quantifier} {_quantifier.GetItemString()}",
						"could not verify, because it was cancelled early"));
				}
			}

			return Task.FromResult(_quantifier.GetResult(
				actual,
				_it,
				_expectationText(),
				matchingCount,
				notMatchingCount,
				matchingCount + notMatchingCount,
				_verb));
		}
	}

	private readonly struct SyncCollectionCountConstraint<TItem> : IAsyncContextConstraint<IEnumerable<TItem>?>
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
			IEnumerable<TItem>? actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			string expectationText = ToString();
			if (actual is null)
			{
				return Task.FromResult<ConstraintResult>(
					new ConstraintResult.Failure<IEnumerable<TItem>?>(
						actual,
						expectationText,
						$"{_it} was <null>"));
			}

			int matchingCount = 0;
			int notMatchingCount = 0;
			int? totalCount = null;

			if (actual is ICollection<TItem> collectionOfT)
			{
				matchingCount = collectionOfT.Count;
				totalCount = matchingCount;
				return Task.FromResult(_quantifier.GetResult(
					actual, _it, null, matchingCount, notMatchingCount, totalCount, null, (_, _) => expectationText));
			}

			IEnumerable<TItem> materialized =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);

			foreach (TItem _ in materialized)
			{
				matchingCount++;

				if (_quantifier.IsDeterminable(matchingCount, notMatchingCount))
				{
					return Task.FromResult(_quantifier.GetResult(actual, _it, null, matchingCount, notMatchingCount,
						totalCount, null, (_, _) => expectationText));
				}

				if (cancellationToken.IsCancellationRequested)
				{
					return Task.FromResult<ConstraintResult>(new ConstraintResult.Failure<IEnumerable<TItem>>(
						actual, expectationText,
						"could not verify, because it was cancelled early"));
				}
			}

			totalCount = matchingCount + notMatchingCount;
			return Task.FromResult(_quantifier.GetResult(actual, _it, null, matchingCount, notMatchingCount,
				totalCount, null, (_, _) => expectationText));
		}

		public override string ToString()
			=> $"has {_quantifier} {_quantifier.GetItemString()}";
	}

	private readonly struct IsInOrderConstraint<TItem, TMember>(
		string it,
		Func<TItem, TMember> memberAccessor,
		SortOrder sortOrder,
		CollectionOrderOptions<TMember> options,
		string memberExpression)
		: IContextConstraint<IEnumerable<TItem>?>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<IEnumerable<TItem>?>(actual, ToString(), $"{it} was <null>");
			}

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
			=> $"is in {sortOrder.ToString().ToLower()} order{options}{memberExpression}";
	}
}
