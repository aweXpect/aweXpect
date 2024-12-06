using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;

namespace aweXpect.Options;

public partial class CollectionMatchOptions
{
	private sealed class AnyOrderIgnoreDuplicatesCollectionMatcher<T, T2> : ICollectionMatcher<T, T2>
		where T : T2
	{
		private readonly Dictionary<int, T> _additionalItems = new();
		private readonly EquivalenceRelation _equivalenceRelation;
		private readonly List<T> _missingItems;
		private readonly int _totalExpectedCount;
		private readonly HashSet<T> _uniqueItems = new();
		private int _index;

		public AnyOrderIgnoreDuplicatesCollectionMatcher(EquivalenceRelation equivalenceRelation,
			IEnumerable<T> expected)
		{
			_equivalenceRelation = equivalenceRelation;
			_missingItems = expected.Distinct().ToList();
			_totalExpectedCount = _missingItems.Count;
		}

		public string? Verify(string it, T value, IOptionsEquality<T2> options)
		{
			if (_uniqueItems.Contains(value))
			{
				return null;
			}

			if (_missingItems.All(e => !options.AreConsideredEqual(value, e)))
			{
				_additionalItems.Add(_index, value);
			}

			_missingItems.Remove(value);
			_uniqueItems.Add(value);
			_index++;

			if (_additionalItems.Count > 20)
			{
				return $"{it} was very different (> 20 deviations)";
			}

			return null;
		}

		public string? VerifyComplete(string it, IOptionsEquality<T2> options)
		{
			if (_missingItems.Count + _additionalItems.Count > 20)
			{
				return $"{it} was very different (> 20 deviations)";
			}

			List<string> errors = new();
			if (!_equivalenceRelation.HasFlag(EquivalenceRelation.Superset))
			{
				errors.AddRange(AdditionalItemsError(_additionalItems, _equivalenceRelation));
			}
			else if (_equivalenceRelation.HasFlag(EquivalenceRelation.ProperSuperset) && !_additionalItems.Any())
			{
				errors.Add("did not contain any additional items");
			}

			if (!_equivalenceRelation.HasFlag(EquivalenceRelation.Subset))
			{
				errors.AddRange(MissingItemsError(_totalExpectedCount, _missingItems, _equivalenceRelation));
			}
			else if (_equivalenceRelation.HasFlag(EquivalenceRelation.ProperSubset) && !_missingItems.Any())
			{
				errors.Add("contained all expected items");
			}

			return ReturnErrorString(it, errors);
		}
	}
}
