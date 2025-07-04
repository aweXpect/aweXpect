﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aweXpect.Core;
using aweXpect.Core.Helpers;

namespace aweXpect.Options;

/// <summary>
///     Options for matching a collection.
/// </summary>
public partial class CollectionMatchOptions(
	CollectionMatchOptions.EquivalenceRelations equivalenceRelations
		= CollectionMatchOptions.EquivalenceRelations.Equivalent)
{
#pragma warning disable S4070 // Non-flags enums should not be marked with "FlagsAttribute"
	/// <summary>
	///     Specifies the equivalence relation between subject and expected.
	/// </summary>
	[Flags]
	public enum EquivalenceRelations
	{
		/// <summary>
		///     The subject and expected collection must be equivalent (have the same items)
		/// </summary>
		Equivalent = 1,

		/// <summary>
		///     The subject collection is contained in the expected collection which has at least one additional item.
		/// </summary>
		IsContainedInProperly = 2 | IsContainedIn,

		/// <summary>
		///     The subject collection contains the expected collection and at least one additional item.
		/// </summary>
		ContainsProperly = 2 | Contains,

		/// <summary>
		///     The subject collection is contained in the expected collection.
		/// </summary>
		IsContainedIn = 4,

		/// <summary>
		///     The subject collection contains the expected collection.
		/// </summary>
		Contains = 8,
	}
#pragma warning restore S4070

	private EquivalenceRelations _equivalenceRelations = equivalenceRelations;
	private bool _ignoringDuplicates;
	private bool _ignoringInterspersedItems;
	private bool _inAnyOrder;

	/// <summary>
	///     Specifies the equivalence relation between subject and expected.
	/// </summary>
	public void SetEquivalenceRelation(EquivalenceRelations equivalenceRelation)
		=> _equivalenceRelations = equivalenceRelation;

	/// <summary>
	///     Ignores the order in the subject and expected values.
	/// </summary>
	public void InAnyOrder() => _inAnyOrder = true;

	/// <summary>
	///     Ignores duplicates in both collections.
	/// </summary>
	public void IgnoringDuplicates() => _ignoringDuplicates = true;

	/// <summary>
	///     Ignores interspersed items in the actual collection.
	/// </summary>
	public void IgnoringInterspersedItems() => _ignoringInterspersedItems = true;

	/// <summary>
	///     Get the collection matcher for the <paramref name="expected" /> enumerable.
	/// </summary>
	public ICollectionMatcher<T, T2> GetCollectionMatcher<T, T2>(IEnumerable<T> expected)
		where T : T2
		=> (_inAnyOrder, _ignoringDuplicates) switch
		{
			(true, true) => new AnyOrderIgnoreDuplicatesCollectionMatcher<T, T2>(_equivalenceRelations, expected),
			(true, false) => new AnyOrderCollectionMatcher<T, T2>(_equivalenceRelations, expected),
			(false, true) => new SameOrderIgnoreDuplicatesCollectionMatcher<T, T2>(_equivalenceRelations, expected,
				_ignoringInterspersedItems),
			(false, false) => new SameOrderCollectionMatcher<T, T2>(_equivalenceRelations, expected,
				_ignoringInterspersedItems),
		};

	/// <summary>
	///     Specifies the expectation for the <paramref name="expectedExpression" /> using the provided
	///     <paramref name="grammars" />.
	/// </summary>
	public string GetExpectation(string expectedExpression, ExpectationGrammars grammars)
		=> (_inAnyOrder, _ignoringDuplicates, _ignoringInterspersedItems) switch
		{
			(true, true, _) => ToString(_equivalenceRelations, expectedExpression, grammars) +
			                   " in any order ignoring duplicates",
			(true, false, _) => ToString(_equivalenceRelations, expectedExpression, grammars) + " in any order",
			(false, true, false) => ToString(_equivalenceRelations, expectedExpression, grammars) +
			                        " in order ignoring duplicates",
			(false, false, false) => ToString(_equivalenceRelations, expectedExpression, grammars) + " in order",
			(false, true, true) => ToString(_equivalenceRelations, expectedExpression, grammars) +
			                       " in order ignoring duplicates and interspersed items",
			(false, false, true) => ToString(_equivalenceRelations, expectedExpression, grammars) +
			                        " in order ignoring interspersed items",
		};

	private static string ToString(EquivalenceRelations equivalenceRelation, string expectedExpression,
		ExpectationGrammars grammars)
		=> (equivalenceRelation, grammars.IsNegated()) switch
		{
			(EquivalenceRelations.Contains, false)
				=> $"contains collection {expectedExpression}",
			(EquivalenceRelations.Contains, true)
				=> $"does not contain collection {expectedExpression}",
			(EquivalenceRelations.ContainsProperly, false)
				=> $"contains collection {expectedExpression} and at least one additional item",
			(EquivalenceRelations.ContainsProperly, true)
				=> $"does not contain collection {expectedExpression} and at least one additional item",
			(EquivalenceRelations.IsContainedIn, false)
				=> $"is contained in collection {expectedExpression}",
			(EquivalenceRelations.IsContainedIn, true)
				=> $"is not contained in collection {expectedExpression}",
			(EquivalenceRelations.IsContainedInProperly, false)
				=> $"is contained in collection {expectedExpression} which has at least one additional item",
			(EquivalenceRelations.IsContainedInProperly, true)
				=> $"is not contained in collection {expectedExpression} which has at least one additional item",
			(_, false) => $"matches collection {expectedExpression}",
			(_, true) => $"does not match collection {expectedExpression}",
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

			return $"{it} {errors[0]}";
		}

		return null;
	}

	private static IEnumerable<string> AdditionalItemsError<T>(Dictionary<int, T> additionalItems)
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

	private static IEnumerable<string> IncorrectItemsError<T>(Dictionary<int, (T Item, T Expected)> incorrectItems)
	{
		bool hasIncorrectItems = incorrectItems.Any();
		if (hasIncorrectItems)
		{
			foreach (KeyValuePair<int, (T Item, T Expected)> incorrectItem in incorrectItems)
			{
				yield return
					$"contained item {Formatter.Format(incorrectItem.Value.Item)} at index {incorrectItem.Key} instead of {Formatter.Format(incorrectItem.Value.Expected)}";
			}
		}
	}

	private static IEnumerable<string> MissingItemsError<T>(int total, List<T> missingItems,
		EquivalenceRelations equivalenceRelation, bool ignoringDuplicates)
	{
		if (total == 0)
		{
			yield break;
		}

		bool hasMissingItems = missingItems.Any();
		if (total == missingItems.Count)
		{
			yield return ignoringDuplicates switch
			{
				true => $"lacked all {total} unique expected items",
				false => $"lacked all {total} expected items",
			};
			yield break;
		}

		if (hasMissingItems && !equivalenceRelation.HasFlag(EquivalenceRelations.IsContainedIn))
		{
			if (missingItems.Count == 1)
			{
				yield return
					$"lacked {missingItems.Count} of {total} expected items: {Formatter.Format(missingItems[0])}";
				yield break;
			}

			StringBuilder sb = new();
			sb.Append("lacked ").Append(missingItems.Count).Append(" of ")
				.Append(total).Append(" expected items:");
			foreach (T? missingItem in missingItems)
			{
				sb.AppendLine().Append("  ");
				Formatter.Format(sb, missingItem);
				sb.Append(',');
			}

			sb.Length--;
			yield return sb.ToString();
		}
	}
}
