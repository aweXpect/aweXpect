using System;
using System.Collections.Generic;
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

	private class CollectionConstraint<TItem>
		: ConstraintResult<IEnumerable<TItem>?>,
			IAsyncContextConstraint<IEnumerable<TItem>?>
	{
		private readonly string _it;
		private readonly ExpectationGrammars _grammars;
		private readonly EnumerableQuantifier _quantifier;
		private readonly Func<ExpectationGrammars, string> _expectationText;
		private readonly Func<TItem, bool> _predicate;
		private readonly string _verb;
		private int _matchingCount;
		private int _notMatchingCount;
		private int? _totalCount;

		public CollectionConstraint(string it,
			ExpectationGrammars grammars,
			EnumerableQuantifier quantifier,
			Func<ExpectationGrammars, string> expectationText,
			Func<TItem, bool> predicate,
			string verb) : base(grammars)
		{
			_it = it;
			_grammars = grammars;
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
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return Task.FromResult<ConstraintResult>(this);
			}

			IEnumerable<TItem> materialized = context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			bool cancelEarly = actual is not ICollection<TItem>;
			_matchingCount = 0;
			_notMatchingCount = 0;

			foreach (TItem item in materialized)
			{
				if (_predicate(item))
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
					return Task.FromResult<ConstraintResult>(this);
				}

				if (cancellationToken.IsCancellationRequested)
				{
					Outcome = Outcome.Undecided;
					return Task.FromResult<ConstraintResult>(this);
				}
			}

			_totalCount = _matchingCount + _notMatchingCount;
			Outcome = _quantifier.GetOutcome(_matchingCount, _notMatchingCount, _totalCount);
			return Task.FromResult<ConstraintResult>(this);
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
				stringBuilder.Append(_it);
				stringBuilder.Append(" was <null>");
			}
			else if (Outcome == Outcome.Undecided)
			{
				stringBuilder.Append("could not verify, because it was cancelled early");
			}
			else
			{
				_quantifier.AppendResult(stringBuilder, _grammars, _matchingCount, _notMatchingCount, _totalCount, _verb);
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
				stringBuilder.Append(_it);
				stringBuilder.Append(" was <null>");
			}
			else if (Outcome == Outcome.Undecided)
			{
				stringBuilder.Append("could not verify, because it was cancelled early");
			}
			else
			{
				_quantifier.AppendResult(stringBuilder, _grammars.Negate(), _matchingCount, _notMatchingCount, _totalCount, _verb);
			}
		}
	}

	private class SyncCollectionCountConstraint<TItem>
		: ConstraintResult<IEnumerable<TItem>?>,
			IAsyncContextConstraint<IEnumerable<TItem>?>
	{
		private readonly string _it;
		private readonly ExpectationGrammars _grammars;
		private readonly EnumerableQuantifier _quantifier;
		private int _matchingCount;
		private int _notMatchingCount;
		private int? _totalCount;

		public SyncCollectionCountConstraint(string it, ExpectationGrammars grammars, EnumerableQuantifier quantifier)
			: base(grammars)
		{
			_it = it;
			_grammars = grammars;
			_quantifier = quantifier;
		}

		public Task<ConstraintResult> IsMetBy(
			IEnumerable<TItem>? actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return Task.FromResult<ConstraintResult>(this);
			}

			_matchingCount = 0;
			_notMatchingCount = 0;

			if (actual is ICollection<TItem> collectionOfT)
			{
				_matchingCount = collectionOfT.Count;
				_totalCount = _matchingCount;
				Outcome = _quantifier.GetOutcome(_matchingCount, _notMatchingCount, _totalCount);
				return Task.FromResult<ConstraintResult>(this);
			}

			IEnumerable<TItem> materialized =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);

			foreach (TItem _ in materialized)
			{
				_matchingCount++;

				if (_quantifier.IsDeterminable(_matchingCount, _notMatchingCount))
				{
					Outcome = _quantifier.GetOutcome(_matchingCount, _notMatchingCount, _totalCount);
					return Task.FromResult<ConstraintResult>(this);
				}

				if (cancellationToken.IsCancellationRequested)
				{
					Outcome = Outcome.Undecided;
					return Task.FromResult<ConstraintResult>(this);
				}
			}

			_totalCount = _matchingCount + _notMatchingCount;
			Outcome = _quantifier.GetOutcome(_matchingCount, _notMatchingCount, _totalCount);
			return Task.FromResult<ConstraintResult>(this);
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
				stringBuilder.Append(_it);
				stringBuilder.Append(" was <null>");
			}
			else if (Outcome == Outcome.Undecided)
			{
				stringBuilder.Append("could not verify, because it was cancelled early");
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
				stringBuilder.Append(_it);
				stringBuilder.Append(" was <null>");
			}
			else if (Outcome == Outcome.Undecided)
			{
				stringBuilder.Append("could not verify, because it was cancelled early");
			}
			else
			{
				_quantifier.AppendResult(stringBuilder, _grammars.Negate(), _matchingCount, _notMatchingCount, _totalCount);
			}
		}
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
