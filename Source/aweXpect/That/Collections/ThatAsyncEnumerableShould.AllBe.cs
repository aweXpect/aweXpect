#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
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

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that all items in the collection are equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		AllBe<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			TItem expected)
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new AllBeConstraint<TItem>(
					it,
					() => $"have all items be equal to {Formatter.Format(expected)}",
					a => options.AreConsideredEqual(a, expected))),
			source,
			options);
	}

	/// <summary>
	///     Verifies that all items in the collection are equal to the <paramref name="expected" /> value.
	/// </summary>
	public static StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>> AllBe(
		this IThat<IAsyncEnumerable<string>> source,
		string expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new AllBeConstraint<string>(
					it,
					() => $"have all items be equal to {Formatter.Format(expected)}",
					a => options.AreConsideredEqual(a, expected))),
			source,
			options);
	}

	private readonly struct AllBeConstraint<TItem>(
		string it,
		Func<string> expectationText,
		Func<TItem, bool> predicate)
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		public async Task<ConstraintResult> IsMetBy(
			IAsyncEnumerable<TItem> actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			IAsyncEnumerable<TItem> materializedAsyncEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			List<TItem> notMatchingItems = new(Customize.Formatting.MaximumNumberOfCollectionItems + 1);
			await foreach (TItem item in materializedAsyncEnumerable.WithCancellation(cancellationToken))
			{
				if (!predicate(item))
				{
					notMatchingItems.Add(item);
					if (notMatchingItems.Count > Customize.Formatting.MaximumNumberOfCollectionItems)
					{
						int displayCount = Math.Min(
							Customize.Formatting.MaximumNumberOfCollectionItems,
							notMatchingItems.Count);
						return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
							$"{it} contained at least {displayCount} other {(displayCount == 1 ? "item" : "items")}: {Formatter.Format(notMatchingItems, FormattingOptions.MultipleLines)}");
					}
				}
			}

			if (notMatchingItems.Count == 0)
			{
				return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(materializedAsyncEnumerable,
					ToString());
			}

			return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(),
				$"{it} contained {notMatchingItems.Count} other {(notMatchingItems.Count == 1 ? "item" : "items")}: {Formatter.Format(notMatchingItems, FormattingOptions.MultipleLines)}");
		}

		public override string ToString()
			=> expectationText();
	}
}
#endif
