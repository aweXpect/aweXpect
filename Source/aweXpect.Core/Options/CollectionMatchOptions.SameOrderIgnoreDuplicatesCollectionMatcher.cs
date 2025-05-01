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
		private readonly EquivalenceRelations _equivalenceRelations;
		private readonly T[] _expectedDistinctItems;
		private readonly List<T> _foundItems = new();
		private readonly Dictionary<int, (T Item, T Expected)> _incorrectItems = new();
		private readonly List<T> _matchingItems = new();
		private readonly List<T> _missingItems = new();
		private readonly int _totalExpectedItems;
		private readonly HashSet<T> _uniqueItems = new();
		private int _expectationIndex = -1;
		private int _index;
		private int _matchIndex;

		public SameOrderIgnoreDuplicatesCollectionMatcher(EquivalenceRelations equivalenceRelation,
			IEnumerable<T> expected)
		{
			_equivalenceRelations = equivalenceRelation;
			_expectedDistinctItems = expected.Distinct().ToArray();
			_totalExpectedItems = _expectedDistinctItems.Length;
		}

		public bool Verify(string it, T value, IOptionsEquality<T2> options, int maximumNumber, out string? error)
		{
			error = null;
			if (_equivalenceRelations.HasFlag(EquivalenceRelations.IsContainedIn))
			{
				_foundItems.Add(value);
			}

			if (_matchIndex >= _expectedDistinctItems.Length)
			{
				if (!_uniqueItems.Add(value))
				{
					return false;
				}

				// All expected items were found -> additional items
				_additionalItems.Add(_index, value);
			}
			else if (options.AreConsideredEqual(value, _expectedDistinctItems[_matchIndex]))
			{
				VerifyTheCurrentValueIsEqualToTheExpectedValue(value, options);
			}
			else
			{
				if (!_uniqueItems.Add(value))
				{
					return false;
				}

				VerifyTheCurrentValueIsDifferentFromTheExpectedValue(value, options);
			}

			_index++;
			return _additionalItems.Count + _incorrectItems.Count + _missingItems.Count >
			       2 * maximumNumber;
		}

#pragma warning disable S3776 // https://rules.sonarsource.com/csharp/RSPEC-3776
		public bool VerifyComplete(string it, IOptionsEquality<T2> options, int maximumNumber, out string? error)
		{
			int maximumNumberOfCollectionItems =
				maximumNumber;
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

				if (_additionalItems.Count + _incorrectItems.Count + _missingItems.Count >
				    2 * maximumNumberOfCollectionItems)
				{
					error = null;
					return true;
				}
			}

			if (_equivalenceRelations.HasFlag(EquivalenceRelations.IsContainedIn) &&
			    !_incorrectItems.Any())
			{
				VerifyCompleteForSubsetMatch(options);
			}

			List<string> errors = new();
			errors.AddRange(IncorrectItemsError(_incorrectItems, _expectedDistinctItems, _equivalenceRelations));
			if (!_equivalenceRelations.HasFlag(EquivalenceRelations.Contains))
			{
				errors.AddRange(AdditionalItemsError(_additionalItems));
			}
			else if (_equivalenceRelations.HasFlag(EquivalenceRelations.ContainsProperly) &&
			         !_additionalItems.Any() &&
			         !_incorrectItems.Any(i
				         => _expectedDistinctItems.All(e => !options.AreConsideredEqual(e, i.Value.Item))))
			{
				errors.Add("did not contain any additional items");
			}

			if (!_equivalenceRelations.HasFlag(EquivalenceRelations.IsContainedIn))
			{
				errors.AddRange(MissingItemsError(_totalExpectedItems, _missingItems, _equivalenceRelations));
			}
			else if (_equivalenceRelations.HasFlag(EquivalenceRelations.IsContainedInProperly) && !_missingItems.Any())
			{
				errors.Add("contained all expected items");
			}

			error = ReturnErrorString(it, errors);
			return error != null;
		}
#pragma warning restore S3776

		private void VerifyCompleteForSubsetMatch(IOptionsEquality<T2> options)
		{
			T[] foundItems = _foundItems.Distinct().ToArray();
			for (int i = 0; i < _expectedDistinctItems.Length - foundItems.Length; i++)
			{
				bool isMatch = true;
				for (int j = 0; j < foundItems.Length; j++)
				{
					if (!options.AreConsideredEqual(foundItems[j], _expectedDistinctItems[i + j]))
					{
						isMatch = false;
						break;
					}
				}

				if (isMatch)
				{
					_additionalItems.Clear();
					break;
				}
			}
		}

		private void VerifyTheCurrentValueIsDifferentFromTheExpectedValue(T value, IOptionsEquality<T2> options)
		{
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

		private void VerifyTheCurrentValueIsEqualToTheExpectedValue(T value, IOptionsEquality<T2> options)
		{
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
	}
}
