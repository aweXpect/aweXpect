using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect.Options;

/// <summary>
///     Options for matching a collection.
/// </summary>
public partial class CollectionMatchOptions
{
	/// <summary>
	///     Specifies the equivalence relation between subject and expected.
	/// </summary>
	[Flags]
	public enum EquivalenceRelation
	{
		/// <summary>
		///     The subject and expected collection must be equivalent (have the same items)
		/// </summary>
		Equivalent = 1,

		/// <summary>
		///     The expected collection contains at least one additional item.
		/// </summary>
		ProperSubset = 2 | Subset,

		/// <summary>
		///     The subject collection contains at least one additional item.
		/// </summary>
		ProperSuperset = 2 | Superset,

		/// <summary>
		///     The expected collection can contain additional items.
		/// </summary>
		Subset = 4,

		/// <summary>
		///     The subject collection can contain additional items.
		/// </summary>
		Superset = 8
	}

	private EquivalenceRelation _equivalenceRelation = EquivalenceRelation.Equivalent;
	private bool _ignoringDuplicates;
	private bool _inAnyOrder;

	/// <summary>
	///     Specifies the equivalence relation between subject and expected.
	/// </summary>
	public void SetEquivalenceRelation(EquivalenceRelation equivalenceRelation)
		=> _equivalenceRelation = equivalenceRelation;

	/// <summary>
	///     Ignores the order in the subject and expected values.
	/// </summary>
	public void InAnyOrder() => _inAnyOrder = true;

	/// <summary>
	///     Ignores duplicates in both collections.
	/// </summary>
	public void IgnoringDuplicates() => _ignoringDuplicates = true;

	/// <summary>
	///     Get the collection matcher for the <paramref name="expected" /> enumerable.
	/// </summary>
	public ICollectionMatcher<T, T2> GetCollectionMatcher<T, T2>(IEnumerable<T> expected)
		where T : T2
		=> (_inAnyOrder, _ignoringDuplicates) switch
		{
			(true, true) => new AnyOrderIgnoreDuplicatesCollectionMatcher<T, T2>(_equivalenceRelation, expected),
			(true, false) => new AnyOrderCollectionMatcher<T, T2>(_equivalenceRelation, expected),
			(false, true) => new SameOrderIgnoreDuplicatesCollectionMatcher<T, T2>(_equivalenceRelation, expected),
			(false, false) => new SameOrderCollectionMatcher<T, T2>(_equivalenceRelation, expected)
		};

	/// <inheritdoc />
	public override string ToString()
		=> (_inAnyOrder, _ignoringDuplicates) switch
		{
			(true, true) => ToString(_equivalenceRelation) + " in any order ignoring duplicates",
			(true, false) => ToString(_equivalenceRelation) + " in any order",
			(false, true) => ToString(_equivalenceRelation) + " ignoring duplicates",
			(false, false) => ToString(_equivalenceRelation)
		};

	private static string ToString(EquivalenceRelation equivalenceRelation)
		=> equivalenceRelation switch
		{
			EquivalenceRelation.Superset => " or more items",
			EquivalenceRelation.ProperSuperset => " and at least one more item",
			EquivalenceRelation.Subset => " or less items",
			EquivalenceRelation.ProperSubset => " and at least one item less",
			_ => ""
		};

	private static string? ReturnErrorString(string it, List<string> errors)
	{
		if (errors.Count > 0)
		{
			if (errors.Count > 1)
			{
				StringBuilder sb = new();
				sb.Append(it);
				foreach (string error in errors)
				{
					sb.AppendLine().Append(error.Indent()).Append(" and");
				}

				sb.Length -= 4;
				return sb.ToString();
			}

			return $"{it} {errors.Single()}";
		}

		return null;
	}

		private static IEnumerable<string> AdditionalItemsError<T>(Dictionary<int, T> additionalItems,
			EquivalenceRelation equivalenceRelation)
		{
			bool hasAdditionalItems = additionalItems.Any();
			if (hasAdditionalItems)
			{
				foreach (KeyValuePair<int, T> additionalItem in additionalItems)
				{
					yield return
						$"contained item {Formatter.Format(additionalItem.Value)} at index {additionalItem.Key} that was not expected";
				}
			}
		}

		private static IEnumerable<string> IncorrectItemsError<T>(Dictionary<int, (T Item, T Expected)> incorrectItems,
			T[] expectedItems,
			EquivalenceRelation equivalenceRelation)
		{
			bool hasIncorrectItems = incorrectItems.Any();
			if (hasIncorrectItems)
			{
				foreach (KeyValuePair<int, (T Item, T Expected)> incorrectItem in incorrectItems)
				{
					if (equivalenceRelation.HasFlag(EquivalenceRelation.Superset) &&
					    !expectedItems.Contains(incorrectItem.Value.Item))
					{
						continue;
					}

					yield return
						$"contained item {Formatter.Format(incorrectItem.Value.Item)} at index {incorrectItem.Key} instead of {Formatter.Format(incorrectItem.Value.Expected)}";
				}
			}
		}

		private static IEnumerable<string> MissingItemsError<T>(int total, List<T> missingItems,
			EquivalenceRelation equivalenceRelation)
		{
			bool hasMissingItems = missingItems.Any();
			if (hasMissingItems && !equivalenceRelation.HasFlag(EquivalenceRelation.Subset))
			{
				if (missingItems.Count == 1)
				{
					yield return
						$"lacked {missingItems.Count} of {total} expected items: {Formatter.Format(missingItems.Single())}";
					yield break;
				}

				StringBuilder sb = new();
				sb.Append("lacked ").Append(missingItems.Count).Append(" of ")
					.Append(total).Append(" expected items:");
				foreach (T? missingItem in missingItems)
				{
					sb.AppendLine().Append("  ");
					Formatter.Format(sb, missingItem);
					sb.Append(",");
				}

				sb.Length--;
				yield return sb.ToString();
			}

			if (!hasMissingItems && equivalenceRelation.HasFlag(EquivalenceRelation.ProperSubset))
			{
				yield return "did contain all expected items";
			}
		}
}
