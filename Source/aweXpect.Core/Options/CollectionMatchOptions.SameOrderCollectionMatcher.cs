using System;
using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;

namespace aweXpect.Options;

public partial class CollectionMatchOptions
{
	private sealed class SameOrderCollectionMatcher<T, T2> : ICollectionMatcher<T, T2>
		where T : T2
	{
		private readonly Dictionary<int, T> _additionalItems = new();
		private readonly EquivalenceRelations _equivalenceRelations;
		private readonly T[] _expectedItems;
		private readonly List<T> _foundItems = new();
		private readonly Dictionary<int, (T Item, T Expected)> _incorrectItems = new();
		private readonly List<T> _matchingItems = new();
		private readonly List<T> _missingItems = new();
		private readonly int _totalExpectedItems;
		private int _expectationIndex = -1;
		private int _index;
		private int _matchIndex;

		public SameOrderCollectionMatcher(EquivalenceRelations equivalenceRelation,
			IEnumerable<T> expected)
		{
			_equivalenceRelations = equivalenceRelation;
			_expectedItems = expected.ToArray();
			_totalExpectedItems = _expectedItems.Length;
		}

		public bool Verify(string it, T value, IOptionsEquality<T2> options, out string? error)
		{
			_foundItems.Add(value);

			if (_matchIndex >= _expectedItems.Length)
			{
				// All expected items were found -> additional items
				_additionalItems.Add(_index, value);
			}
			else if (options.AreConsideredEqual(value, _expectedItems[_matchIndex]))
			{
				VerifyTheCurrentValueIsEqualToTheExpectedValue(value);
			}
			else
			{
				VerifyTheCurrentValueIsDifferentFromTheExpectedValue(value, options);
			}

			_index++;
			error = null;
			return _additionalItems.Count + _incorrectItems.Count + _missingItems.Count > 2 * Customization.Customize.Formatting.MaximumNumberOfCollectionItems;
		}

		public bool VerifyComplete(string it, IOptionsEquality<T2> options, out string? error)
		{
			int consideredExpectedItems = Math.Max(_expectationIndex - 1, _matchIndex);
			if (_expectedItems.Length > consideredExpectedItems)
			{
				for (int i = consideredExpectedItems; i < _expectedItems.Length; i++)
				{
					T item = _expectedItems[i];
					if (_additionalItems.All(x => !options.AreConsideredEqual(x.Value, item)) &&
					    _incorrectItems.All(x => !options.AreConsideredEqual(x.Value.Item, item)))
					{
						_missingItems.Add(item);
					}

					if (_additionalItems.Count + _incorrectItems.Count + _missingItems.Count >
					    2 * Customization.Customize.Formatting.MaximumNumberOfCollectionItems)
					{
						error = null;
						return true;
					}
				}
			}

			if (_equivalenceRelations.HasFlag(EquivalenceRelations.Subset) &&
			    !_incorrectItems.Any())
			{
				VerifyCompleteForSubsetMatch(options);
			}

			List<string> errors = new();
			errors.AddRange(IncorrectItemsError(_incorrectItems, _expectedItems, _equivalenceRelations));
			if (!_equivalenceRelations.HasFlag(EquivalenceRelations.Superset))
			{
				errors.AddRange(AdditionalItemsError(_additionalItems));
			}
			else if (_equivalenceRelations.HasFlag(EquivalenceRelations.ProperSuperset) && !_additionalItems.Any())
			{
				errors.Add("did not contain any additional items");
			}

			if (!_equivalenceRelations.HasFlag(EquivalenceRelations.Subset))
			{
				errors.AddRange(MissingItemsError(_totalExpectedItems, _missingItems, _equivalenceRelations));
			}
			else if (_equivalenceRelations.HasFlag(EquivalenceRelations.ProperSubset) && !_missingItems.Any())
			{
				errors.Add("contained all expected items");
			}

			error = ReturnErrorString(it, errors);
			return error != null;
		}

		private void VerifyTheCurrentValueIsDifferentFromTheExpectedValue(T value, IOptionsEquality<T2> options)
		{
			bool movedMatch = _equivalenceRelations.HasFlag(EquivalenceRelations.Subset) &&
			                  _matchIndex > 0 &&
			                  SearchForMatchInFoundItems(value, options);

			if (!movedMatch)
			{
				if (_expectationIndex >= 0)
				{
					_expectationIndex++;
				}

				_matchIndex = 0;
			}

			if (options.AreConsideredEqual(value, _expectedItems[_matchIndex]))
			{
				if (!movedMatch)
				{
					for (int i = _index - _matchingItems.Count; i < _index; i++)
					{
						_additionalItems.Add(i, _matchingItems[i]);
					}
				}

				_matchingItems.Clear();
				_matchIndex++;
				_expectationIndex = 0;
				_matchingItems.Add(value);
			}
			else if (movedMatch || _expectationIndex < 0 || _expectationIndex >= _expectedItems.Length)
			{
				_additionalItems.Add(_index, value);
			}
			else
			{
				_incorrectItems.Add(_index, (value, _expectedItems[_expectationIndex]));
			}
		}

		private bool SearchForMatchInFoundItems(T value, IOptionsEquality<T2> options)
		{
			for (int i = 1; i < _expectedItems.Length - _matchingItems.Count; i++)
			{
				if (options.AreConsideredEqual(value, _expectedItems[_matchIndex + i]))
				{
					bool couldBeMatch = true;
					for (int j = 0; j < _matchingItems.Count; j++)
					{
						if (!options.AreConsideredEqual(_matchingItems[j], _expectedItems[j + i]))
						{
							couldBeMatch = false;
						}
					}

					if (couldBeMatch)
					{
						_matchIndex += i;

						for (int j = 0; j < i; j++)
						{
							_missingItems.Add(_matchingItems[j]);
						}

						return true;
					}
				}
			}

			return false;
		}

		private void VerifyTheCurrentValueIsEqualToTheExpectedValue(T value)
		{
			_matchIndex++;
			_expectationIndex++;
			_matchingItems.Add(value);
		}

		private void VerifyCompleteForSubsetMatch(IOptionsEquality<T2> options)
		{
			for (int i = 0; i < _expectedItems.Length - _foundItems.Count; i++)
			{
				bool isMatch = true;
				for (int j = 0; j < _foundItems.Count; j++)
				{
					if (!options.AreConsideredEqual(_foundItems[j], _expectedItems[i + j]))
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
	}
}
