#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Customization;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Verifies that the collection contains the <paramref name="expected" /> value.
	/// </summary>
	public static ObjectCountResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
		Contains<TItem>(
			this IThat<IAsyncEnumerable<TItem>?> source,
			TItem expected)
	{
		Quantifier quantifier = new();
		ObjectEqualityOptions<TItem> options = new();
		return new ObjectCountResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainConstraint<TItem>(
					it, grammars,
					q => $"contains {Formatter.Format(expected)}{options} {q}",
					a => options.AreConsideredEqual(a, expected),
					quantifier)),
			source,
			quantifier,
			options);
	}

	/// <summary>
	///     Verifies that the collection contains the <paramref name="expected" /> value.
	/// </summary>
	public static StringEqualityTypeCountResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>
		Contains(
			this IThat<IAsyncEnumerable<string?>?> source,
			string? expected)
	{
		Quantifier quantifier = new();
		StringEqualityOptions options = new();
		return new StringEqualityTypeCountResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainConstraint<string?>(
					it, grammars,
					q => $"contains {Formatter.Format(expected)}{options} {q}",
					a => options.AreConsideredEqual(a, expected),
					quantifier)),
			source,
			quantifier,
			options);
	}

	/// <summary>
	///     Verifies that the collection contains an item that satisfies the <paramref name="predicate" />.
	/// </summary>
	public static CountResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>
		Contains<TItem>(
			this IThat<IAsyncEnumerable<TItem>?> source,
			Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
	{
		Quantifier quantifier = new();
		return new CountResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainConstraint<TItem>(
					it, grammars,
					q => $"contains item matching {doNotPopulateThisValue.TrimCommonWhiteSpace()} {q}",
					predicate,
					quantifier)),
			source,
			quantifier);
	}

	/// <summary>
	///     Verifies that the collection contains the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionContainResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
		Contains<TItem>(
			this IThat<IAsyncEnumerable<TItem>?> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.Contains);
		return new ObjectCollectionContainResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsConstraint<TItem, TItem>(it, grammars, doNotPopulateThisValue.TrimCommonWhiteSpace(), expected,
					options, matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection contains the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringCollectionContainResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>
		Contains(
			this IThat<IAsyncEnumerable<string?>?> source,
			IEnumerable<string?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.Contains);
		return new StringCollectionContainResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsConstraint<string?, string?>(it, grammars, doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection does not contain the <paramref name="unexpected" /> value.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
		DoesNotContain<TItem>(
			this IThat<IAsyncEnumerable<TItem>?> source,
			TItem unexpected)
	{
		ObjectEqualityOptions<TItem> options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainConstraint<TItem>(it, grammars,
					_ => $"does not contain {Formatter.Format(unexpected)}{options}",
					a => options.AreConsideredEqual(a, unexpected),
					Quantifier.Never())),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection does not contain the <paramref name="unexpected" /> value.
	/// </summary>
	public static StringEqualityResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>
		DoesNotContain(
			this IThat<IAsyncEnumerable<string?>?> source,
			string? unexpected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainConstraint<string?>(it, grammars,
					_ => $"does not contain {Formatter.Format(unexpected)}{options}",
					a => options.AreConsideredEqual(a, unexpected),
					Quantifier.Never())),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the collection contains no item that satisfies the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>
		DoesNotContain<TItem>(
			this IThat<IAsyncEnumerable<TItem>?> source,
			Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ContainConstraint<TItem>(it, grammars,
					_ => $"does not contain item matching {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
					predicate,
					Quantifier.Never())),
			source);

	private sealed class ContainConstraint<TItem>(
		string it,
		ExpectationGrammars grammars,
		Func<Quantifier, string> expectationText,
		Func<TItem, bool> predicate,
		Quantifier quantifier)
		: ConstraintResult.WithNotNullValue<IAsyncEnumerable<TItem>?>(it, grammars),
			IAsyncContextConstraint<IAsyncEnumerable<TItem>?>
	{
		private readonly List<TItem> _items = [];
		private int _count;
		private bool _isFinished;

		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem>? actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			Actual = actual;
			if (actual is null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			int maximumNumberOfCollectionItems =
				Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get();
			_count = 0;
			bool isFailed = false;
			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				if (_items.Count <= maximumNumberOfCollectionItems)
				{
					_items.Add(item);
				}

				if (predicate(item))
				{
					_count++;
					bool? check = quantifier.Check(_count, false);
					if (check == false)
					{
						isFailed = true;
					}

					if (check == true)
					{
						Outcome = Outcome.Success;
						return this;
					}
				}

				if (_items.Count > maximumNumberOfCollectionItems && isFailed)
				{
					Outcome = Outcome.Failure;
					return this;
				}
			}

			if (quantifier.Check(_count, true) == true)
			{
				Outcome = Outcome.Success;
				return this;
			}

			_isFinished = true;
			Outcome = Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(expectationText(quantifier));

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_isFinished)
			{
				stringBuilder.Append(It).Append(" contained it ").Append(_count).Append(" times in ");
				Formatter.Format(stringBuilder, _items, FormattingOptions.MultipleLines);
			}
			else
			{
				stringBuilder.Append(It).Append(" contained it at least ").Append(_count).Append(" times in ");
				Formatter.Format(stringBuilder, _items, FormattingOptions.MultipleLines);
			}
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(expectationText.Invoke(quantifier));

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(It).Append(" did");
	}
}
#endif
