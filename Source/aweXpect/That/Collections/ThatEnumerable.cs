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
	private sealed class IsConstraint<TItem, TMatch>(
		string it,
		ExpectationGrammars grammars,
		string expectedExpression,
		IEnumerable<TItem>? expected,
		IOptionsEquality<TMatch> options,
		CollectionMatchOptions matchOptions)
		: ConstraintResult.WithNotNullValue<IEnumerable<TItem>?>(it, grammars),
			IContextConstraint<IEnumerable<TItem>?>
		where TItem : TMatch
	{
		private string? _failure;
		
		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			if (expected is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			IEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			ICollectionMatcher<TItem, TMatch> matcher = matchOptions.GetCollectionMatcher<TItem, TMatch>(expected);
			int maximumNumber = Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get();

			foreach (TItem item in materializedEnumerable)
			{
				if (matcher.Verify(It, item, options, maximumNumber, out _failure))
				{
					_failure ??= TooManyDeviationsError(materializedEnumerable);
					Outcome = Outcome.Failure;
					return this;
				}
			}

			if (matcher.VerifyComplete(It, options, maximumNumber, out _failure))
			{
				_failure ??= TooManyDeviationsError(materializedEnumerable);
				Outcome = Outcome.Failure;
				return this;
			}

			Outcome = Outcome.Success;
			return this;
		}

		private string TooManyDeviationsError(IEnumerable<TItem> materializedEnumerable)
			=> $"{It} was completely different: {Formatter.Format(materializedEnumerable, FormattingOptions.MultipleLines)} had more than {2 * Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get()} deviations compared to {Formatter.Format(expected, FormattingOptions.MultipleLines)}";

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)		{
			stringBuilder.Append(matchOptions.GetExpectation(expectedExpression));
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
			=> throw new NotImplementedException();

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();
	}

	private sealed class CollectionConstraint<TItem>
		: ConstraintResult.WithNotNullValue<IEnumerable<TItem>?>,
			IAsyncContextConstraint<IEnumerable<TItem>?>
	{
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
			string verb) : base(it, grammars)
		{
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
			if (Grammars.HasFlag(ExpectationGrammars.Nested))
			{
				stringBuilder.Append(_quantifier);
				stringBuilder.Append(' ');
				stringBuilder.Append(_expectationText(Grammars));
			}
			else
			{
				stringBuilder.Append(_expectationText(Grammars));
				stringBuilder.Append(" for ");
				stringBuilder.Append(_quantifier);
				stringBuilder.Append(' ');
				stringBuilder.Append(_quantifier.GetItemString());
			}
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			_quantifier.AppendResult(stringBuilder, Grammars, _matchingCount, _notMatchingCount, _totalCount, _verb);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (Grammars.HasFlag(ExpectationGrammars.Nested))
			{
				stringBuilder.Append("not ");
				stringBuilder.Append(_quantifier);
				stringBuilder.Append(' ');
				stringBuilder.Append(_expectationText(Grammars));
			}
			else
			{
				stringBuilder.Append(_expectationText(Grammars.Negate()));
				stringBuilder.Append(" for ");
				stringBuilder.Append(_quantifier);
				stringBuilder.Append(' ');
				stringBuilder.Append(_quantifier.GetItemString());
			}
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			_quantifier.AppendResult(stringBuilder, Grammars.Negate(), _matchingCount, _notMatchingCount, _totalCount, _verb);
		}
	}

	private sealed class SyncCollectionCountConstraint<TItem>
		: ConstraintResult.WithNotNullValue<IEnumerable<TItem>?>,
			IAsyncContextConstraint<IEnumerable<TItem>?>
	{
		private readonly EnumerableQuantifier _quantifier;
		private int _matchingCount;
		private int _notMatchingCount;
		private int? _totalCount;

		public SyncCollectionCountConstraint(string it, ExpectationGrammars grammars, EnumerableQuantifier quantifier)
			: base(it, grammars)
		{
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
			_quantifier.AppendResult(stringBuilder, Grammars, _matchingCount, _notMatchingCount, _totalCount);
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
			_quantifier.AppendResult(stringBuilder, Grammars.Negate(), _matchingCount, _notMatchingCount, _totalCount);
		}
	}

	private sealed class IsInOrderConstraint<TItem, TMember>(
		string it,
		ExpectationGrammars grammars,
		Func<TItem, TMember> memberAccessor,
		SortOrder sortOrder,
		CollectionOrderOptions<TMember> options,
		string memberExpression)
		: ConstraintResult.WithNotNullValue<IEnumerable<TItem>?>(it, grammars),
			IContextConstraint<IEnumerable<TItem>?>
	{
		private string? _failureText;

		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
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
					_failureText =
						$"{It} had {Formatter.Format(previous)} before {Formatter.Format(current)} which is not in {sortOrder.ToString().ToLower()} order in ";
					Outcome = Outcome.Failure;
					return this;
				}

				previous = current;
			}

			Outcome = Outcome.Success;
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
			Formatter.Format(stringBuilder, Actual, FormattingOptions.MultipleLines);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not in ").Append(sortOrder.ToString().ToLower()).Append(" order");
			stringBuilder.Append(options).Append(memberExpression);
		}
 
		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was in ");
			Formatter.Format(stringBuilder, Actual, FormattingOptions.MultipleLines);
		}
	}
}
