using System;
using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;

namespace aweXpect.Options;

public partial class CollectionMatchOptions
{
	private sealed class SameOrderIgnoreDuplicatesCollectionMatcher<T, T2> : ICollectionMatcher<T, T2>
		where T : T2
	{
		private readonly Dictionary<int, T> _additionalItems = new();
		private readonly EquivalenceRelation _equivalenceRelation;
		private readonly T[] _expectedDistinctItems;
		private readonly Dictionary<int, (T Item, T Expected)> _incorrectItems = new();
		private readonly List<T> _matchingItems = new();
		private readonly List<T> _missingItems = new();
		private readonly int _totalExpectedItems;
		private readonly HashSet<T> _uniqueItems = new();
		private int _expectationIndex = -1;
		private int _index;
		private int _matchIndex;

		public SameOrderIgnoreDuplicatesCollectionMatcher(EquivalenceRelation equivalenceRelation,
			IEnumerable<T> expected)
		{
			_equivalenceRelation = equivalenceRelation;
			_expectedDistinctItems = expected.Distinct().ToArray();
			_totalExpectedItems = _expectedDistinctItems.Length;
		}

		public string? Verify(string it, T value, IOptionsEquality<T2> options)
		{
			if (_matchIndex >= _expectedDistinctItems.Length)
			{
				if (!_uniqueItems.Add(value))
				{
					return null;
				}

				// All expected items were found -> additional items
				_additionalItems.Add(_index, value);
			}
			else if (options.AreConsideredEqual(value, _expectedDistinctItems[_matchIndex]))
			{
				// The current value is equal to the expected value
				_matchIndex++;
				_expectationIndex++;
				_matchingItems.Add(value);
				_uniqueItems.Add(value);
				foreach (int key in _additionalItems
					         .Where(item => options.AreConsideredEqual(item.Value, value))
					         .Select(x => x.Key)
					         .ToList())
				{
					_additionalItems.Remove(key);
				}
			}
			else
			{
				if (!_uniqueItems.Add(value))
				{
					return null;
				}

				if (_expectationIndex >= 0)
				{
					_expectationIndex++;
				}

				_matchIndex = 0;

				if (options.AreConsideredEqual(value, _expectedDistinctItems[_matchIndex]))
				{
					for (int i = _index - _matchingItems.Count; i < _index; i++)
					{
						_additionalItems.Add(i, _matchingItems[i]);
					}

					_matchingItems.Clear();
					_matchIndex++;
					_expectationIndex = 0;
					_matchingItems.Add(value);
				}
				else if (_expectationIndex < 0 || _expectationIndex >= _expectedDistinctItems.Length)
				{
					_additionalItems.Add(_index, value);
				}
				else
				{
					_incorrectItems.Add(_index, (value, _expectedDistinctItems[_expectationIndex]));
				}
			}

			if (_additionalItems.Count + _incorrectItems.Count + _missingItems.Count > 20)
			{
				return $"{it} was very different (> 20 deviations)";
			}

			_index++;
			return null;
		}

		public string? VerifyComplete(string it, IOptionsEquality<T2> options)
		{
			foreach (T item in _expectedDistinctItems.Skip(Math.Max(_expectationIndex - 1, _matchIndex)))
			{
				if (!_uniqueItems.Add(item))
				{
					continue;
				}

				if (_additionalItems.All(x => !options.AreConsideredEqual(x.Value, item)) &&
				    _incorrectItems.All(x => !options.AreConsideredEqual(x.Value.Item, item)))
				{
					_missingItems.Add(item);
				}

				if (_additionalItems.Count + _incorrectItems.Count + _missingItems.Count > 20)
				{
					return $"{it} was very different (> 20 deviations)";
				}
			}

			List<string> errors = new();
			errors.AddRange(IncorrectItemsError(_incorrectItems, _expectedDistinctItems, _equivalenceRelation));
			if (!_equivalenceRelation.HasFlag(EquivalenceRelation.Superset))
			{
				errors.AddRange(AdditionalItemsError(_additionalItems, _equivalenceRelation));
			}
			else if (_equivalenceRelation.HasFlag(EquivalenceRelation.ProperSuperset) &&
			         !_additionalItems.Any() &&
			         !_incorrectItems.Any(i
				         => _expectedDistinctItems.All(e => !options.AreConsideredEqual(e, i.Value.Item))))
			{
				errors.Add("did not contain any additional items");
			}

			if (!_equivalenceRelation.HasFlag(EquivalenceRelation.Subset))
			{
				errors.AddRange(MissingItemsError(_totalExpectedItems, _missingItems, _equivalenceRelation));
			}
			else if (_equivalenceRelation.HasFlag(EquivalenceRelation.ProperSubset) && !_missingItems.Any())
			{
				errors.Add("contained all expected items");
			}

			return ReturnErrorString(it, errors);
		}
	}
}
