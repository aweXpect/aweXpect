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
					it,
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
					it,
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
					it,
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
				new IsConstraint<TItem, TItem>(it, doNotPopulateThisValue.TrimCommonWhiteSpace(), expected, options, matchOptions)),
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
				new IsConstraint<string?, string?>(it, doNotPopulateThisValue.TrimCommonWhiteSpace(), expected, options, matchOptions)),
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
				new NotContainConstraint<TItem>(it,
					() => $"does not contain {Formatter.Format(unexpected)}{options}",
					a => options.AreConsideredEqual(a, unexpected))),
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
				new NotContainConstraint<string?>(it,
					() => $"does not contain {Formatter.Format(unexpected)}{options}",
					a => options.AreConsideredEqual(a, unexpected))),
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
				new NotContainConstraint<TItem>(it,
					() => $"does not contain item matching {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
					predicate)),
			source);

	private readonly struct ContainConstraint<TItem>(
		string it,
		Func<Quantifier, string> expectationText,
		Func<TItem, bool> predicate,
		Quantifier quantifier)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>?>
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem>? actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>?>(actual, ToString(), $"{it} was <null>");
			}

			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			int maximumNumberOfCollectionItems =
				Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get();
			List<TItem> items = new(maximumNumberOfCollectionItems + 1);
			int count = 0;
			bool isFailed = false;
			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				if (items.Count <= maximumNumberOfCollectionItems)
				{
					items.Add(item);
				}

				if (predicate(item))
				{
					count++;
					bool? check = quantifier.Check(count, false);
					if (check == false)
					{
						isFailed = true;
					}

					if (check == true)
					{
						return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(materializedEnumerable,
							ToString());
					}
				}

				if (items.Count > maximumNumberOfCollectionItems && isFailed)
				{
					return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
						$"{it} contained it at least {count} times in {Formatter.Format(items.ToArray(), FormattingOptions.MultipleLines)}");
				}
			}

			if (quantifier.Check(count, true) == true)
			{
				return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(materializedEnumerable,
					ToString());
			}

			return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
				$"{it} contained it {count} times in {Formatter.Format(items.ToArray(), FormattingOptions.MultipleLines)}");
		}

		public override string ToString()
			=> expectationText(quantifier);
	}

	private readonly struct NotContainConstraint<TItem>(
		string it,
		Func<string> expectationText,
		Func<TItem, bool> predicate)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>?>
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem>? actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>?>(actual, ToString(), $"{it} was <null>");
			}

			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				if (predicate(item))
				{
					return new ConstraintResult.Failure(ToString(),
						$"{it} did");
				}
			}

			return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(materializedEnumerable,
				ToString());
		}

		public override string ToString() => expectationText.Invoke();
	}
}
#endif
