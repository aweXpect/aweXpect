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
		private readonly EquivalenceRelations _equivalenceRelations;
		private readonly List<T> _missingItems;
		private readonly int _totalExpectedCount;
		private readonly HashSet<T> _uniqueItems = new();
		private int _index;

		public AnyOrderIgnoreDuplicatesCollectionMatcher(EquivalenceRelations equivalenceRelation,
			IEnumerable<T> expected)
		{
			_equivalenceRelations = equivalenceRelation;
			_missingItems = expected.Distinct().ToList();
			_totalExpectedCount = _missingItems.Count;
		}

		public bool Verify(string it, T value, IOptionsEquality<T2> options, out string? error)
		{
			error = null;
			if (_uniqueItems.Contains(value))
			{
				return false;
			}

			if (_missingItems.All(e => !options.AreConsideredEqual(value, e)))
			{
				_additionalItems.Add(_index, value);
			}

			_missingItems.Remove(value);
			_uniqueItems.Add(value);
			_index++;

			return _additionalItems.Count > 2 * Customization.Customize.Formatting.MaximumNumberOfCollectionItems;
		}

		public bool VerifyComplete(string it, IOptionsEquality<T2> options, out string? error)
		{
			if (_missingItems.Count + _additionalItems.Count > 2 * Customization.Customize.Formatting.MaximumNumberOfCollectionItems)
			{
				error = null;
				return true;
			}

			List<string> errors = new();
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
				errors.AddRange(MissingItemsError(_totalExpectedCount, _missingItems, _equivalenceRelations));
			}
			else if (_equivalenceRelations.HasFlag(EquivalenceRelations.ProperSubset) && !_missingItems.Any())
			{
				errors.Add("contained all expected items");
			}

			error = ReturnErrorString(it, errors);
			return error != null;
		}
	}
}
