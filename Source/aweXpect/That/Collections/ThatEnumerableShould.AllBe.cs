using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Customization;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that all items in the collection are equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		AllBe<TItem>(
			this IThat<IEnumerable<TItem>> source,
			TItem expected)
	{
		ObjectEqualityOptions options = new();
		return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>(source.ExpectationBuilder
				.AddConstraint(it => new AllBeConstraint<TItem>(
					it,
					() => $"have all items be equal to {Formatter.Format(expected)}",
					a => options.AreConsideredEqual(a, expected))),
			source,
			options);
	}

	/// <summary>
	///     Verifies that all items in the collection are equal to the <paramref name="expected" /> value.
	/// </summary>
	public static StringEqualityResult<IEnumerable<string>, IThat<IEnumerable<string>>> AllBe(
		this IThat<IEnumerable<string>> source,
		string expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityResult<IEnumerable<string>, IThat<IEnumerable<string>>>(source.ExpectationBuilder
				.AddConstraint(it => new AllBeConstraint<string>(
					it,
					() => $"have all items be equal to {Formatter.Format(expected)}",
					a => options.AreConsideredEqual(a, expected))),
			source,
			options);
	}

	/// <summary>
	///     Verifies that all items in the collection satisfy the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		AllBe<TItem>(
			this IThat<IEnumerable<TItem>> source,
			Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
		=> new(source.ExpectationBuilder
				.AddConstraint(it
					=> new AllBeConstraint<TItem>(
						it,
						() => $"have all items satisfy {doNotPopulateThisValue}",
						predicate)),
			source);

	private readonly struct AllBeConstraint<TItem>(
		string it,
		Func<string> expectationText,
		Func<TItem, bool> predicate)
		: IContextConstraint<IEnumerable<TItem>>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem> actual, IEvaluationContext context)
		{
			IEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			List<TItem> notMatchingItems = new(Customize.Formatting.MaximumNumberOfCollectionItems + 1);
			foreach (TItem item in materializedEnumerable)
			{
				if (!predicate(item))
				{
					notMatchingItems.Add(item);
					if (notMatchingItems.Count > Customize.Formatting.MaximumNumberOfCollectionItems)
					{
						var displayCount = Math.Min(
							Customize.Formatting.MaximumNumberOfCollectionItems,
							notMatchingItems.Count);
						return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
							$"{it} contained at least {displayCount} other {(displayCount == 1 ? "item" : "items")}: {Formatter.Format(notMatchingItems, FormattingOptions.MultipleLines)}");
					}
				}
			}

			if (notMatchingItems.Count == 0)
			{
				return new ConstraintResult.Success<IEnumerable<TItem>>(materializedEnumerable,
					ToString());
			}

			return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
				$"{it} contained {notMatchingItems.Count} other {(notMatchingItems.Count == 1 ? "item" : "items")}: {Formatter.Format(notMatchingItems, FormattingOptions.MultipleLines)}");
		}

		public override string ToString()
			=> expectationText();
	}
}
