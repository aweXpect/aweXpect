using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect.Options;

public partial class CollectionMatchOptions
{
	private sealed class AnyOrderCollectionMatcher<T, T2> : ICollectionMatcher<T, T2>
		where T : T2
	{
		private readonly Dictionary<int, T> _additionalItems = new();
		private readonly List<T> _expectedList;
		private readonly int _totalExpectedCount;
		private int _index;
		private readonly EquivalenceRelation _equivalenceRelation;

		public AnyOrderCollectionMatcher(EquivalenceRelation equivalenceRelation,
			IEnumerable<T> expected)
		{
			_equivalenceRelation = equivalenceRelation;
			_expectedList = expected.ToList();
			_totalExpectedCount = _expectedList.Count;
		}

		public string? Verify(string it, T value, IOptionsEquality<T2> options)
		{
			if (_expectedList.All(e => !options.AreConsideredEqual(value, e)))
			{
				_additionalItems.Add(_index, value);
			}

			if (_additionalItems.Count > 20)
			{
				return $"{it} was very different (> 20 deviations)";
			}

			_expectedList.Remove(value);
			_index++;
			return null;
		}

		public string? VerifyComplete(string it, IOptionsEquality<T2> options)
		{
			if (_expectedList.Count + _additionalItems.Count > 20)
			{
				return $"{it} was very different (> 20 deviations)";
			}
			
			string? missingItemsError = MissingItemsError(it, _totalExpectedCount, _expectedList, _equivalenceRelation);
			
			bool hasAdditionalItems = _additionalItems.Any();
			if (hasAdditionalItems && !_equivalenceRelation.HasFlag(EquivalenceRelation.Superset))
			{
				if (_additionalItems.Count == 1)
				{
					KeyValuePair<int, T> firstAdditionalItem = _additionalItems.Single();
					return AppendIfNotNull(
						$"{it} contained item {Formatter.Format(firstAdditionalItem.Value)} at index {firstAdditionalItem.Key} that was not expected",
						missingItemsError, "  ");
				}

				return AppendIfNotNull($"{it} contained{Environment.NewLine}" + string.Join(
						$" and{Environment.NewLine}", _additionalItems.Select(x => $"  item {Formatter.Format(x.Value)} at index {x.Key} that was not expected")),
					missingItemsError, "  ");
			}

			if (!hasAdditionalItems && _equivalenceRelation.HasFlag(EquivalenceRelation.ProperSuperset))
			{
				return AppendIfNotNull($"{it} did not contain any additional items", missingItemsError);
			}

			return missingItemsError;
		}

		private static string AppendIfNotNull(string prefix, string? suffix, string indentation = "")
		{
			if (suffix == null)
			{
				return prefix;
			}

			return $"{prefix} and{Environment.NewLine}{(indentation == "" ? suffix : suffix.Indent(indentation))}";
		}

		private static string? MissingItemsError(string it, int total, List<T> missingItems,
			EquivalenceRelation equivalenceRelation)
		{
			bool hasMissingItems = missingItems.Any();
			if (hasMissingItems && !equivalenceRelation.HasFlag(EquivalenceRelation.Subset))
			{
				if (missingItems.Count == 1)
				{
					return
						$"{it} lacked {missingItems.Count} of {total} expected items: {Formatter.Format(missingItems.Single())}";
				}

				StringBuilder sb = new();
				sb.Append(it).Append(" lacked ").Append(missingItems.Count).Append(" of ")
					.Append(total).Append(" expected items:");
				foreach (T? missingItem in missingItems)
				{
					sb.AppendLine().Append("  ");
					Formatter.Format(sb, missingItem);
					sb.Append(",");
				}

				sb.Length--;
				return sb.ToString();
			}

			if (!hasMissingItems && equivalenceRelation.HasFlag(EquivalenceRelation.ProperSubset))
			{
				return $"{it} did contain all expected items";
			}

			return null;
		}

		public void Dispose() => _index = -1;
	}
}
