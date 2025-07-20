#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Verifies that the collection has an item matching the <paramref name="predicate" />…
	/// </summary>
	public static HasItemResult<IAsyncEnumerable<TItem>?> HasItem<TItem>(
		this IThat<IAsyncEnumerable<TItem>?> source, Func<TItem, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
	{
		CollectionIndexOptions indexOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new HasItemResult<IAsyncEnumerable<TItem>?>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new HasItemConstraint<TItem>(expectationBuilder, it, grammars, predicate,
					() => doNotPopulateThisValue, indexOptions)),
			source,
			indexOptions);
	}

	/// <summary>
	///     Verifies that the collection has the <paramref name="expected" /> item…
	/// </summary>
	public static HasItemObjectResult<IAsyncEnumerable<TItem>?, TItem> HasItem<TItem>(
		this IThat<IAsyncEnumerable<TItem>?> source, TItem expected)
	{
		CollectionIndexOptions indexOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		ObjectEqualityOptions<TItem> options = new();
		return new HasItemObjectResult<IAsyncEnumerable<TItem>?, TItem>(
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
	///     Verifies that the collection has the <paramref name="expected" /> item…
	/// </summary>
	public static HasItemStringResult<IAsyncEnumerable<string?>?> HasItem(
		this IThat<IAsyncEnumerable<string?>?> source, string? expected)
	{
		CollectionIndexOptions indexOptions = new();
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		StringEqualityOptions options = new();
		return new HasItemStringResult<IAsyncEnumerable<string?>?>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new HasItemConstraint<string?>(expectationBuilder, it, grammars,
					a => options.AreConsideredEqual(a, expected),
					() => options.GetExpectation(expected, grammars),
					indexOptions)),
			source,
			indexOptions,
			options);
	}

	private sealed class HasItemConstraint<TItem>(
		ExpectationBuilder expectationBuilder,
		string it,
		ExpectationGrammars grammars,
		Func<TItem, bool> predicate,
		Func<string> predicateDescription,
		CollectionIndexOptions options)
		: ConstraintResult.WithValue<IAsyncEnumerable<TItem>?>(grammars),
			IAsyncContextConstraint<IAsyncEnumerable<TItem>?>
	{
		private TItem? _actual;
		private bool _hasIndex;

		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem>? actual, IEvaluationContext context,
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
			await expectationBuilder.AddCollectionContext(materialized as IMaterializedEnumerable<TItem>);
			_hasIndex = false;
			Outcome = Outcome.Failure;

			int index = -1;
			await foreach (TItem item in materialized.WithCancellation(cancellationToken))
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
}
#endif
