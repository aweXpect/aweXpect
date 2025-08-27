using System;
using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;
using aweXpect.Core.Helpers;

namespace aweXpect.Options;

public partial class CollectionMatchOptions
{
	private class SameOrderIgnoreDuplicatesCollectionMatcher<T, T2>(
		EquivalenceRelations equivalenceRelation,
		IEnumerable<T> expected,
		bool ignoreInterspersedItems)
		: SameOrderIgnoreDuplicatesCollectionMatcherBase<T, T2, T>(equivalenceRelation, expected, ignoreInterspersedItems)
		where T : T2
	{
		protected override bool AreConsideredEqual(T value, T expected, IOptionsEquality<T2> options)
			=> options.AreConsideredEqual(value, expected);
	}

	private class SameOrderIgnoreDuplicatesFromPredicateCollectionMatcher<T, T2>(
		EquivalenceRelations equivalenceRelation,
		IEnumerable<Func<T, bool>> expected,
		bool ignoreInterspersedItems)
		: SameOrderIgnoreDuplicatesCollectionMatcherBase<T, T2, Func<T, bool>>(equivalenceRelation, expected, ignoreInterspersedItems)
		where T : T2
	{
		protected override bool AreConsideredEqual(T value, Func<T, bool> expected, IOptionsEquality<T2> options)
			=> expected(value);
	}

	private abstract class SameOrderIgnoreDuplicatesCollectionMatcherBase<T, T2, T3> : ICollectionMatcher<T, T2>
		where T : T2
	{
		private readonly Dictionary<int, T> _additionalItems = new();
		private readonly EquivalenceRelations _equivalenceRelations;
		private readonly bool _ignoreInterspersedItems;
		private readonly T3[] _expectedDistinctItems;
		private readonly List<T> _foundItems = new();
		private readonly Dictionary<int, (T Item, T3 Expected)> _incorrectItems = new();
		private readonly List<T> _matchingItems = new();
		private readonly List<T3> _missingItems = new();
		private readonly int _totalExpectedItems;
		private readonly HashSet<T> _uniqueItems = new();
		private int _expectationIndex = -1;
		private int _index;
		private int _matchIndex;
		private int _maxMatchIndex;

		protected SameOrderIgnoreDuplicatesCollectionMatcherBase(EquivalenceRelations equivalenceRelation,
			IEnumerable<T3> expected,
			bool ignoreInterspersedItems)
		{
			_equivalenceRelations = equivalenceRelation;
			_ignoreInterspersedItems = ignoreInterspersedItems;
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
			else if (AreConsideredEqual(value, _expectedDistinctItems[_matchIndex], options))
			{
				VerifyTheCurrentValueIsEqualToTheExpectedValue(value, options);
			}
			else if (_ignoreInterspersedItems)
			{
				if (!_uniqueItems.Add(value))
				{
					return false;
				}

				_additionalItems.Add(_index, value);
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
			foreach (T3 item in _expectedDistinctItems.Skip(Math.Max(_expectationIndex - 1, _maxMatchIndex)))
			{
				KeyValuePair<int, T> additionalItem = _additionalItems
					.FirstOrDefault(a => AreConsideredEqual(a.Value, item, options));
				if (!additionalItem.IsDefault())
				{
					_additionalItems.Remove(additionalItem.Key);
					_missingItems.Add(item);
				}
				
				if (_uniqueItems.Any(v => AreConsideredEqual(v, item, options)))
				{
					continue;
				}
				
				if (_additionalItems.All(x => !AreConsideredEqual(x.Value, item, options)) &&
				    _incorrectItems.All(x => !AreConsideredEqual(x.Value.Item, item, options)))
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
			errors.AddRange(IncorrectItemsError(_incorrectItems));
			if (!_equivalenceRelations.HasFlag(EquivalenceRelations.Contains))
			{
				errors.AddRange(AdditionalItemsError(_additionalItems));
			}
			else if (_equivalenceRelations.HasFlag(EquivalenceRelations.ContainsProperly) &&
			         !_additionalItems.Any() &&
			         !_incorrectItems.Any(i
				         => _expectedDistinctItems.All(e => !AreConsideredEqual(i.Value.Item, e, options))))
			{
				errors.Add("did not contain any additional items");
			}

			if (!_equivalenceRelations.HasFlag(EquivalenceRelations.IsContainedIn))
			{
				errors.AddRange(MissingItemsError(_totalExpectedItems, _missingItems, _equivalenceRelations, true));
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
					if (!AreConsideredEqual(foundItems[j], _expectedDistinctItems[i + j], options))
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

			if (AreConsideredEqual(value, _expectedDistinctItems[_matchIndex], options))
			{
				for (int i = _index - _matchingItems.Count; i < _index; i++)
				{
					_additionalItems.Add(i, _matchingItems[i]);
				}

				_matchingItems.Clear();
				_matchIndex++;
				_maxMatchIndex = Math.Max(_matchIndex, _maxMatchIndex);
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
			_maxMatchIndex = Math.Max(_matchIndex, _maxMatchIndex);
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

		protected abstract bool AreConsideredEqual(T value, T3 expected, IOptionsEquality<T2> options);
	}
}
