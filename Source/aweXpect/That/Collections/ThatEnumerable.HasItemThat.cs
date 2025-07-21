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
#if NET8_0_OR_GREATER
using System.Collections;
using System.Collections.Immutable;
using System.Linq;
#endif

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the collection has an item that complies with the <paramref name="expectations" />…
	/// </summary>
	public static HasItemResult<IEnumerable<TItem>?> HasItemThat<TItem>(
		this IThat<IEnumerable<TItem>?> source, Action<IThat<TItem>> expectations)
	{
		CollectionIndexOptions indexOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new HasItemResult<IEnumerable<TItem>?>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new HasItemThatConstraint<TItem>(expectationBuilder, it, grammars, expectations, indexOptions)),
			source,
			indexOptions);
	}

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection has an item that complies with the <paramref name="expectations" />…
	/// </summary>
	public static HasItemResult<ImmutableArray<TItem>> HasItemThat<TItem>(
		this IThat<ImmutableArray<TItem>> source, Action<IThat<TItem>> expectations)
	{
		CollectionIndexOptions indexOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new HasItemResult<ImmutableArray<TItem>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new HasItemThatForEnumerableConstraint<ImmutableArray<TItem>, TItem>(
					expectationBuilder, it, grammars, expectations, indexOptions)),
			source,
			indexOptions);
	}
#endif

	private sealed class HasItemThatConstraint<TItem> : ConstraintResult.WithValue<IEnumerable<TItem>?>,
		IAsyncContextConstraint<IEnumerable<TItem>?>
	{
		private readonly ExpectationBuilder _expectationBuilder;
		private readonly string _it;
		private readonly ManualExpectationBuilder<TItem> _itemExpectationBuilder;
		private readonly CollectionIndexOptions _options;
		private TItem? _actual;
		private bool _hasIndex;

		public HasItemThatConstraint(ExpectationBuilder expectationBuilder,
			string it,
			ExpectationGrammars grammars,
			Action<IThat<TItem>> expectations,
			CollectionIndexOptions options) : base(grammars)
		{
			_expectationBuilder = expectationBuilder;
			_it = it;
			_options = options;

			_itemExpectationBuilder = new ManualExpectationBuilder<TItem>(_expectationBuilder, Grammars);
			expectations.Invoke(new ThatSubject<TItem>(_itemExpectationBuilder));
		}

		public async Task<ConstraintResult> IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			IEnumerable<TItem> materialized = context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			_expectationBuilder.AddCollectionContext(materialized);
			_hasIndex = false;
			Outcome = Outcome.Failure;

			int index = -1;
			foreach (TItem item in materialized)
			{
				index++;
				bool? isIndexInRange = _options.DoesIndexMatch(index);
				if (isIndexInRange != true)
				{
					if (isIndexInRange == false)
					{
						break;
					}

					continue;
				}

				_hasIndex = true;
				_actual = item;
				ConstraintResult isMatch = await _itemExpectationBuilder.IsMetBy(item, context, cancellationToken);
				Outcome = isMatch.Outcome;
				if (Outcome == Outcome.Success)
				{
					break;
				}
			}

			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("has item that ");
			_itemExpectationBuilder.AppendExpectation(stringBuilder, indentation);
			stringBuilder.Append(_options.GetDescription());
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (Actual is null)
			{
				stringBuilder.ItWasNull(_it);
			}
			else if (_hasIndex)
			{
				if (_options.MatchesOnlySingleIndex())
				{
					stringBuilder.Append(_it).Append(" had item ");
					Formatter.Format(stringBuilder, _actual);
					stringBuilder.Append(_options.GetDescription());
				}
				else
				{
					string optionDescription = _options.GetDescription();
					if (string.IsNullOrEmpty(optionDescription))
					{
						optionDescription = " at any index";
					}

					stringBuilder.Append(_it).Append(" did not match").Append(optionDescription);
				}
			}
			else
			{
				stringBuilder.Append(_it).Append(" did not contain any item").Append(_options.GetDescription());
			}
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("does not have item that ");
			_itemExpectationBuilder.AppendExpectation(stringBuilder, indentation);
			stringBuilder.Append(_options.GetDescription());
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_actual is null)
			{
				stringBuilder.ItWasNull(_it);
			}
			else
			{
				stringBuilder.Append(_it).Append(" did");
			}
		}
	}

#if NET8_0_OR_GREATER
	private sealed class HasItemThatForEnumerableConstraint<TEnumerable, TItem> :
		ConstraintResult.WithValue<TEnumerable>,
		IAsyncContextConstraint<TEnumerable>
		where TEnumerable : IEnumerable?
	{
		private readonly ExpectationBuilder _expectationBuilder;
		private readonly string _it;
		private readonly ManualExpectationBuilder<TItem> _itemExpectationBuilder;
		private readonly CollectionIndexOptions _options;
		private object? _actual;

		public HasItemThatForEnumerableConstraint(ExpectationBuilder expectationBuilder,
			string it,
			ExpectationGrammars grammars,
			Action<IThat<TItem>> expectations,
			CollectionIndexOptions options) : base(grammars)
		{
			_expectationBuilder = expectationBuilder;
			_it = it;
			_options = options;

			_itemExpectationBuilder = new ManualExpectationBuilder<TItem>(_expectationBuilder, Grammars);
			expectations.Invoke(new ThatSubject<TItem>(_itemExpectationBuilder));
		}

		public async Task<ConstraintResult> IsMetBy(TEnumerable? actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			IEnumerable materialized = context.UseMaterializedEnumerable(actual);
			_expectationBuilder.AddCollectionContext(materialized);
			Outcome = Outcome.Failure;

			int index = -1;
			foreach (TItem item in materialized.Cast<TItem>())
			{
				index++;
				bool? isIndexInRange = _options.DoesIndexMatch(index);
				if (isIndexInRange != true)
				{
					if (isIndexInRange == false)
					{
						break;
					}

					continue;
				}

				_actual = item;
				ConstraintResult isMatch = await _itemExpectationBuilder.IsMetBy(item, context, cancellationToken);
				Outcome = isMatch.Outcome;
				if (Outcome == Outcome.Success)
				{
					break;
				}
			}

			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("has item that ");
			_itemExpectationBuilder.AppendExpectation(stringBuilder, indentation);
			stringBuilder.Append(_options.GetDescription());
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (Actual is null)
			{
				stringBuilder.ItWasNull(_it);
			}
			else if (_actual is not null)
			{
				if (_options.MatchesOnlySingleIndex())
				{
					stringBuilder.Append(_it).Append(" had item ");
					Formatter.Format(stringBuilder, _actual);
					stringBuilder.Append(_options.GetDescription());
				}
				else
				{
					string optionDescription = _options.GetDescription();
					if (string.IsNullOrEmpty(optionDescription))
					{
						optionDescription = " at any index";
					}

					stringBuilder.Append(_it).Append(" did not match").Append(optionDescription);
				}
			}
			else
			{
				stringBuilder.Append(_it).Append(" did not contain any item").Append(_options.GetDescription());
			}
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("does not have item that ");
			_itemExpectationBuilder.AppendExpectation(stringBuilder, indentation);
			stringBuilder.Append(_options.GetDescription());
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_actual is null)
			{
				stringBuilder.ItWasNull(_it);
			}
			else
			{
				stringBuilder.Append(_it).Append(" did");
			}
		}
	}
#endif
}
