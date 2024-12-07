using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;

namespace aweXpect.Options;

public partial class CollectionMatchOptions
{
	private sealed class AnyOrderCollectionMatcher<T, T2> : ICollectionMatcher<T, T2>
		where T : T2
	{
		private readonly Dictionary<int, T> _additionalItems = new();
		private readonly EquivalenceRelations _equivalenceRelations;
		private readonly List<T> _missingItems;
		private readonly int _totalExpectedCount;
		private int _index;

		public AnyOrderCollectionMatcher(EquivalenceRelations equivalenceRelation,
			IEnumerable<T> expected)
		{
			_equivalenceRelations = equivalenceRelation;
			_missingItems = expected.ToList();
			_totalExpectedCount = _missingItems.Count;
		}

		public bool Verify(string it, T value, IOptionsEquality<T2> options, out string? error)
		{
			if (_missingItems.All(e => !options.AreConsideredEqual(value, e)))
			{
				_additionalItems.Add(_index, value);
			}

			_missingItems.Remove(value);
			_index++;
			error = null;
			return _additionalItems.Count > 2 * CollectionFormatCount;
		}

		public bool VerifyComplete(string it, IOptionsEquality<T2> options, out string? error)
		{
			if (_additionalItems.Count + _missingItems.Count > 2 * CollectionFormatCount)
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
