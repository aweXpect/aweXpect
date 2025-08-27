using System;
using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;
using aweXpect.Core.Helpers;

namespace aweXpect.Options;

public partial class CollectionMatchOptions
{
	private class SameOrderCollectionMatcher<T, T2>(
		EquivalenceRelations equivalenceRelation,
		IEnumerable<T> expected,
		bool ignoreInterspersedItems)
		: SameOrderCollectionMatcherBase<T, T2, T>(equivalenceRelation, expected, ignoreInterspersedItems)
		where T : T2
	{
		protected override bool AreConsideredEqual(T value, T expected, IOptionsEquality<T2> options)
			=> options.AreConsideredEqual(value, expected);
	}

	private class SameOrderFromPredicateCollectionMatcher<T, T2>(
		EquivalenceRelations equivalenceRelation,
		IEnumerable<Func<T, bool>> expected,
		bool ignoreInterspersedItems)
		: SameOrderCollectionMatcherBase<T, T2, Func<T, bool>>(equivalenceRelation, expected, ignoreInterspersedItems)
		where T : T2
	{
		protected override bool AreConsideredEqual(T value, Func<T, bool> expected, IOptionsEquality<T2> options)
			=> expected(value);
	}

	private abstract class SameOrderCollectionMatcherBase<T, T2, T3> : ICollectionMatcher<T, T2>
		where T : T2
	{
		private readonly Dictionary<int, T> _additionalItems = new();
		private readonly EquivalenceRelations _equivalenceRelations;
		private readonly T3[] _expectedItems;
		private readonly List<T> _foundItems = new();
		private readonly bool _ignoreInterspersedItems;
		private readonly Dictionary<int, (T Item, T3 Expected)> _incorrectItems = new();
		private readonly List<T> _matchingItems = new();
		private readonly List<T3> _missingItems = new();
		private readonly int _totalExpectedItems;
		private int _expectationIndex = -1;
		private int _index;
		private int _matchIndex;
		private int _maxMatchIndex;

		protected SameOrderCollectionMatcherBase(EquivalenceRelations equivalenceRelation,
			IEnumerable<T3> expected,
			bool ignoreInterspersedItems)
		{
			_equivalenceRelations = equivalenceRelation;
			_ignoreInterspersedItems = ignoreInterspersedItems;
			_expectedItems = expected.ToArray();
			_totalExpectedItems = _expectedItems.Length;
		}

		public bool Verify(string it, T value, IOptionsEquality<T2> options, int maximumNumber, out string? error)
		{
			_foundItems.Add(value);

			if (_matchIndex >= _expectedItems.Length)
			{
				// All expected items were found -> additional items
				_additionalItems.Add(_index, value);
			}
			else if (AreConsideredEqual(value, _expectedItems[_matchIndex], options))
			{
				VerifyTheCurrentValueIsEqualToTheExpectedValue(value);
			}
			else if (_ignoreInterspersedItems)
			{
				_additionalItems.Add(_index, value);
			}
			else
			{
				VerifyTheCurrentValueIsDifferentFromTheExpectedValue(value, options);
			}

			_index++;
			error = null;
			int errorThreshold = 2 * maximumNumber;
			int errorCount = _incorrectItems.Count + _missingItems.Count;
			if (!_equivalenceRelations.HasFlag(EquivalenceRelations.Contains))
			{
				errorCount += _additionalItems.Count;
			}

			return errorCount > errorThreshold;
		}

#pragma warning disable S3776 // https://rules.sonarsource.com/csharp/RSPEC-3776
		public bool VerifyComplete(string it, IOptionsEquality<T2> options, int maximumNumber, out string? error)
		{
			int consideredExpectedItems = Math.Max(_expectationIndex - 1, _maxMatchIndex);
			if (_expectedItems.Length > consideredExpectedItems)
			{
				for (int i = consideredExpectedItems; i < _expectedItems.Length; i++)
				{
					T3 item = _expectedItems[i];
					KeyValuePair<int, T> additionalItem = _additionalItems
						.FirstOrDefault(a => AreConsideredEqual(a.Value, item, options));
					if (!additionalItem.IsDefault())
					{
						_additionalItems.Remove(additionalItem.Key);
						_missingItems.Add(item);
					}
					else if (_additionalItems.All(x => !AreConsideredEqual(x.Value, item, options)) &&
					         _incorrectItems.All(x => !AreConsideredEqual(x.Value.Item, item, options)))
					{
						_missingItems.Add(item);
					}

					if (_additionalItems.Count + _incorrectItems.Count + _missingItems.Count >
					    2 * maximumNumber)
					{
						error = null;
						return true;
					}
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
			else if (_equivalenceRelations.HasFlag(EquivalenceRelations.ContainsProperly) && !_additionalItems.Any())
			{
				errors.Add("did not contain any additional items");
			}

			if (!_equivalenceRelations.HasFlag(EquivalenceRelations.IsContainedIn))
			{
				errors.AddRange(MissingItemsError(_totalExpectedItems, _missingItems, _equivalenceRelations, false));
			}
			else if (_equivalenceRelations.HasFlag(EquivalenceRelations.IsContainedInProperly) && !_missingItems.Any())
			{
				errors.Add("contained all expected items");
			}

			error = ReturnErrorString(it, errors);
			return error != null;
		}
#pragma warning restore S3776

		private void VerifyTheCurrentValueIsDifferentFromTheExpectedValue(T value, IOptionsEquality<T2> options)
		{
			bool movedMatch = _equivalenceRelations.HasFlag(EquivalenceRelations.IsContainedIn) &&
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

			if (AreConsideredEqual(value, _expectedItems[_matchIndex], options))
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
				_maxMatchIndex = Math.Max(_matchIndex, _maxMatchIndex);
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

#pragma warning disable S3776 // https://rules.sonarsource.com/csharp/RSPEC-3776
		private bool SearchForMatchInFoundItems(T value, IOptionsEquality<T2> options)
		{
			for (int i = 1; i < _expectedItems.Length - _matchingItems.Count; i++)
			{
				var expectedItem = _expectedItems[_matchIndex + i];
				if (AreConsideredEqual(value, _expectedItems[_matchIndex + i], options))
				{
					bool couldBeMatch = true;
					for (int j = 0; j < _matchingItems.Count; j++)
					{
						if (!AreConsideredEqual(_matchingItems[j], _expectedItems[j + i], options))
						{
							couldBeMatch = false;
						}
					}

					if (couldBeMatch)
					{
						_matchIndex += i;

						for (int j = 0; j < i; j++)
						{
							_missingItems.Add(expectedItem);
						}

						return true;
					}
				}
			}

			return false;
		}
#pragma warning restore S3776

		private void VerifyTheCurrentValueIsEqualToTheExpectedValue(T value)
		{
			_matchIndex++;
			_maxMatchIndex = Math.Max(_matchIndex, _maxMatchIndex);
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
					if (!AreConsideredEqual(_foundItems[j], _expectedItems[i + j], options))
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

		protected abstract bool AreConsideredEqual(T value, T3 expected, IOptionsEquality<T2> options);
	}
}
