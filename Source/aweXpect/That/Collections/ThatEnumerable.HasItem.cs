﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;
#if NET8_0_OR_GREATER
using System.Collections.Immutable;
#endif

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the collection has an item matching the <paramref name="predicate" />…
	/// </summary>
	public static HasItemResult<IEnumerable<TItem>?> HasItem<TItem>(
		this IThat<IEnumerable<TItem>?> source, Func<TItem, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
	{
		CollectionIndexOptions indexOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new HasItemResult<IEnumerable<TItem>?>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new HasItemConstraint<TItem>(expectationBuilder, it, grammars, predicate,
					() => doNotPopulateThisValue, indexOptions)),
			source,
			indexOptions);
	}

	/// <summary>
	///     Verifies that the collection has the <paramref name="expected" /> item…
	/// </summary>
	public static HasItemObjectResult<IEnumerable<TItem>?, TItem> HasItem<TItem>(
		this IThat<IEnumerable<TItem>?> source, TItem expected)
	{
		CollectionIndexOptions indexOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		ObjectEqualityOptions<TItem> options = new();
		return new HasItemObjectResult<IEnumerable<TItem>?, TItem>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new HasItemConstraint<TItem>(expectationBuilder, it, grammars,
					a => options.AreConsideredEqual(a, expected),
					() => $"{Formatter.Format(expected)}{options}",
					indexOptions)),
			source,
			indexOptions,
			options);
	}

	/// <summary>
	///     Verifies that the collection has an item matching the <paramref name="predicate" />…
	/// </summary>
	public static HasItemResult<IEnumerable> HasItem(
		this IThat<IEnumerable> source, Func<object?, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
	{
		CollectionIndexOptions indexOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new HasItemResult<IEnumerable>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new HasItemForEnumerableConstraint<IEnumerable, object?>(
					expectationBuilder, it, grammars,
					predicate, () => doNotPopulateThisValue,
					indexOptions)),
			source,
			indexOptions);
	}

	/// <summary>
	///     Verifies that the collection has the <paramref name="expected" /> item…
	/// </summary>
	public static HasItemObjectResult<IEnumerable, object?> HasItem(
		this IThat<IEnumerable> source, object? expected)
	{
		CollectionIndexOptions indexOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		ObjectEqualityOptions<object?> options = new();
		return new HasItemObjectResult<IEnumerable, object?>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new HasItemForEnumerableConstraint<IEnumerable, object?>(
					expectationBuilder, it, grammars,
					a => options.AreConsideredEqual(a, expected),
					() => $"{Formatter.Format(expected)}{options}",
					indexOptions)),
			source,
			indexOptions,
			options);
	}

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection has an item matching the <paramref name="predicate" />…
	/// </summary>
	public static HasItemResult<ImmutableArray<TItem>> HasItem<TItem>(
		this IThat<ImmutableArray<TItem>> source, Func<TItem, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
	{
		CollectionIndexOptions indexOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new HasItemResult<ImmutableArray<TItem>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new HasItemForEnumerableConstraint<ImmutableArray<TItem>, TItem>(
					expectationBuilder, it, grammars,
					predicate, () => doNotPopulateThisValue,
					indexOptions)),
			source,
			indexOptions);
	}
#endif

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the collection has the <paramref name="expected" /> item…
	/// </summary>
	public static HasItemObjectResult<ImmutableArray<TItem>, TItem> HasItem<TItem>(
		this IThat<ImmutableArray<TItem>> source, TItem expected)
	{
		CollectionIndexOptions indexOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		ObjectEqualityOptions<TItem> options = new();
		return new HasItemObjectResult<ImmutableArray<TItem>, TItem>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new HasItemForEnumerableConstraint<ImmutableArray<TItem>, TItem>(
					expectationBuilder, it, grammars,
					a => options.AreConsideredEqual(a, expected),
					() => $"{Formatter.Format(expected)}{options}",
					indexOptions)),
			source,
			indexOptions,
			options);
	}
#endif

	private sealed class HasItemConstraint<TItem>(
		ExpectationBuilder expectationBuilder,
		string it,
		ExpectationGrammars grammars,
		Func<TItem, bool> predicate,
		Func<string> predicateDescription,
		CollectionIndexOptions options)
		: ConstraintResult.WithValue<IEnumerable<TItem>?>(grammars),
			IContextConstraint<IEnumerable<TItem>?>
	{
		private TItem? _actual;
		private bool _hasIndex;

		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			IEnumerable<TItem> materialized = context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			expectationBuilder.AddCollectionContext(materialized);
			_hasIndex = false;
			Outcome = Outcome.Failure;

			int index = -1;
			foreach (TItem item in materialized)
			{
				index++;
				bool? isIndexInRange = options.IsIndexInRange(index);
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
				Outcome = predicate(item) ? Outcome.Success : Outcome.Failure;
				if (Outcome == Outcome.Success)
				{
					break;
				}
			}

			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("has item ").Append(predicateDescription()).Append(options.GetDescription());

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (Actual is null)
			{
				stringBuilder.ItWasNull(it);
			}
			else if (_hasIndex)
			{
				if (options.HasOnlySingleIndex())
				{
					stringBuilder.Append(it).Append(" had item ");
					Formatter.Format(stringBuilder, _actual);
					stringBuilder.Append(options.GetDescription());
				}
				else
				{
					string optionDescription = options.GetDescription();
					if (string.IsNullOrEmpty(optionDescription))
					{
						optionDescription = " at any index";
					}

					stringBuilder.Append(it).Append(" did not match").Append(optionDescription);
				}
			}
			else
			{
				stringBuilder.Append(it).Append(" did not contain any item").Append(options.GetDescription());
			}
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("does not have item ").Append(predicateDescription())
				.Append(options.GetDescription());

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_actual is null)
			{
				stringBuilder.ItWasNull(it);
			}
			else
			{
				stringBuilder.Append(it).Append(" did");
			}
		}
	}

	private sealed class HasItemForEnumerableConstraint<TEnumerable, TItem>(
		ExpectationBuilder expectationBuilder,
		string it,
		ExpectationGrammars grammars,
		Func<TItem, bool> predicate,
		Func<string> predicateDescription,
		CollectionIndexOptions options)
		: ConstraintResult.WithValue<TEnumerable>(grammars),
			IContextConstraint<TEnumerable>
		where TEnumerable : IEnumerable?
	{
		private object? _actual;

		public ConstraintResult IsMetBy(TEnumerable actual, IEvaluationContext context)
		{
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			IEnumerable materialized = context.UseMaterializedEnumerable(actual);
			expectationBuilder.AddCollectionContext(materialized);
			Outcome = Outcome.Failure;

			int index = -1;
			foreach (TItem item in materialized.Cast<TItem>())
			{
				index++;
				bool? isIndexInRange = options.IsIndexInRange(index);
				if (isIndexInRange != true)
				{
					if (isIndexInRange == false)
					{
						break;
					}

					continue;
				}

				_actual = item;
				Outcome = predicate(item) ? Outcome.Success : Outcome.Failure;
				if (Outcome == Outcome.Success)
				{
					break;
				}
			}

			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("has item ").Append(predicateDescription()).Append(options.GetDescription());

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (Actual is null)
			{
				stringBuilder.ItWasNull(it);
			}
			else if (_actual is not null)
			{
				if (options.HasOnlySingleIndex())
				{
					stringBuilder.Append(it).Append(" had item ");
					Formatter.Format(stringBuilder, _actual);
					stringBuilder.Append(options.GetDescription());
				}
				else
				{
					string optionDescription = options.GetDescription();
					if (string.IsNullOrEmpty(optionDescription))
					{
						optionDescription = " at any index";
					}

					stringBuilder.Append(it).Append(" did not match").Append(optionDescription);
				}
			}
			else
			{
				stringBuilder.Append(it).Append(" did not contain any item").Append(options.GetDescription());
			}
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append("does not have item ").Append(predicateDescription())
				.Append(options.GetDescription());

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_actual is null)
			{
				stringBuilder.ItWasNull(it);
			}
			else
			{
				stringBuilder.Append(it).Append(" did");
			}
		}
	}
}
