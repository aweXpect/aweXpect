using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect.Options;

public partial class CollectionMatchOptions
{
	private sealed class SameOrderIgnoreDuplicatesCollectionMatcher<T, T2>(
		EquivalenceRelation equivalenceRelation,
		IEnumerable<T> expected)
		: ICollectionMatcher<T, T2>
		where T : T2
	{
		private readonly Dictionary<int, (T Item, T? Expected)> _additionalItems = new();
		private readonly T[] _expectedDistinctItems = expected.Distinct().ToArray();
		private readonly List<T> _missingItems = new();
		private readonly HashSet<T> _uniqueItems = new();
		private int _index;

		public string? Verify(string it, T value, IOptionsEquality<T2> options)
		{
			if (_uniqueItems.Contains(value))
			{
				return null;
			}

			if (_expectedDistinctItems.Length > _index)
			{
				var expectedItem = _expectedDistinctItems[_index];
				if (!options.AreConsideredEqual(value, expectedItem))
				{
					if (_additionalItems.All(x => !options.AreConsideredEqual(x.Value.Item, expectedItem)))
					{
						_missingItems.Add(expectedItem);
					}

					_additionalItems.Add(_index, (value, expectedItem));
				}
			}
			else
			{
				_additionalItems.Add(_index, (value, default));
			}

			if (_additionalItems.Count + _missingItems.Count > 20)
			{
				return $"{it} was very different (> 20 deviations)";
			}

			_index++;
			_missingItems.Remove(value);
			_uniqueItems.Add(value);
			return null;
		}

		public string? VerifyComplete(string it, IOptionsEquality<T2> options)
		{
			int total = _index;
			foreach (var item in _expectedDistinctItems.Skip(_index))
			{
				if (!_uniqueItems.Add(item))
				{
					continue;
				}
				
				total++;
				if (_additionalItems.All(x => !options.AreConsideredEqual(x.Value.Item, item)))
				{
					_missingItems.Add(item);
				}

				if (_additionalItems.Count + _missingItems.Count > 20)
				{
					return $"{it} was very different (> 20 deviations)";
				}
			}

			string? missingItemsError = MissingItemsError(it, total, _missingItems, equivalenceRelation);
			bool hasAdditionalItems = _additionalItems.Any();
			if (hasAdditionalItems && !equivalenceRelation.HasFlag(EquivalenceRelation.Superset))
			{
				if (_additionalItems.Count == 1)
				{
					KeyValuePair<int, (T Item, T? Expected)> firstAdditionalItem = _additionalItems.Single();
					return AppendIfNotNull(firstAdditionalItem.Value.Expected is null
							? $"{it} contained item {Formatter.Format(firstAdditionalItem.Value.Item)} at index {firstAdditionalItem.Key} that was not expected"
							: $"{it} contained item {Formatter.Format(firstAdditionalItem.Value.Item)} at index {firstAdditionalItem.Key} instead of {Formatter.Format(firstAdditionalItem.Value.Expected)}",
						missingItemsError, "  ");
				}

				return AppendIfNotNull($"{it} contained{Environment.NewLine}" + string.Join(
						$" and{Environment.NewLine}", _additionalItems.Select(x => x.Value.Expected is null
							? $"  item {Formatter.Format(x.Value.Item)} at index {x.Key} that was not expected"
							: $"  item {Formatter.Format(x.Value.Item)} at index {x.Key} instead of {Formatter.Format(x.Value.Expected)}")),
					missingItemsError, "  ");
			}

			if (!hasAdditionalItems && equivalenceRelation.HasFlag(EquivalenceRelation.ProperSuperset))
			{
				return AppendIfNotNull($"{it} did not contain any additional items", missingItemsError);
			}

			return missingItemsError;
		}

		public void Dispose()
		{
			_uniqueItems.Clear();
			_index = -1;
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
	}
}
