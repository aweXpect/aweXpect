using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;

namespace aweXpect.Options;

public partial class CollectionMatchOptions
{
	private sealed class AnyOrderCollectionMatcher<T, T2>(
		EquivalenceRelations equivalenceRelation,
		IEnumerable<T> expected)
		: AnyOrderCollectionMatcherBase<T, T2, T>(equivalenceRelation, expected)
		where T : T2
	{
		protected override bool AreConsideredEqual(T value, T expected, IOptionsEquality<T2> options)
			=> options.AreConsideredEqual(value, expected);
	}

	private sealed class AnyOrderFromExpectationCollectionMatcher<T, T2>(
		EquivalenceRelations equivalenceRelation,
		IEnumerable<ItemExpectation<T>> expected)
		: AnyOrderCollectionMatcherBase<T, T2, ItemExpectation<T>>(equivalenceRelation, expected)
		where T : T2
	{
		protected override bool AreConsideredEqual(T value, ItemExpectation<T> expected, IOptionsEquality<T2> options)
			=> expected.IsMetBy(value);
	}

	private sealed class AnyOrderFromPredicateCollectionMatcher<T, T2>(
		EquivalenceRelations equivalenceRelation,
		IEnumerable<Expression<Func<T, bool>>> expected)
		: AnyOrderCollectionMatcherBase<T, T2, Expression<Func<T, bool>>>(equivalenceRelation, expected)
		where T : T2
	{
		protected override bool AreConsideredEqual(T value, Expression<Func<T, bool>> expected, IOptionsEquality<T2> options)
			=> expected.Compile().Invoke(value);
	}

	private abstract class AnyOrderCollectionMatcherBase<T, T2, T3> : ICollectionMatcher<T, T2>
		where T : T2
	{
		private readonly Dictionary<int, T> _additionalItems = new();
		private readonly EquivalenceRelations _equivalenceRelations;
		private readonly List<T3> _missingItems;
		private readonly int _totalExpectedCount;
		private int _index;

		protected AnyOrderCollectionMatcherBase(EquivalenceRelations equivalenceRelation, IEnumerable<T3> expected)
		{
			_equivalenceRelations = equivalenceRelation;
			_missingItems = expected.ToList();
			_totalExpectedCount = _missingItems.Count;
		}

		public bool Verify(string it, T value, IOptionsEquality<T2> options, int maximumNumber, out string? error)
		{
			if (_missingItems.All(e => !AreConsideredEqual(value, e, options)))
			{
				_additionalItems.Add(_index, value);
			}

			RemoveFirst(_missingItems, e => AreConsideredEqual(value, e, options));
			_index++;
			error = null;
			return _additionalItems.Count > 2 * maximumNumber;
		}

		public bool VerifyComplete(string it, IOptionsEquality<T2> options, int maximumNumber, out string? error)
		{
			if (_additionalItems.Count + _missingItems.Count > 2 * maximumNumber)
			{
				error = null;
				return true;
			}

			List<string> errors = new();
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
				errors.AddRange(MissingItemsError(_totalExpectedCount, _missingItems, _equivalenceRelations, false));
			}
			else if (_equivalenceRelations.HasFlag(EquivalenceRelations.IsContainedInProperly) && !_missingItems.Any())
			{
				errors.Add("contained all expected items");
			}

			error = ReturnErrorString(it, errors);
			return error != null;
		}

		protected abstract bool AreConsideredEqual(T value, T3 expected, IOptionsEquality<T2> options);

		private static void RemoveFirst(List<T3> items, Func<T3, bool> predicate)
		{
			int index = -1;
			foreach (T3? item in items)
			{
				index++;
				if (predicate(item))
				{
					items.RemoveAt(index);
					break;
				}
			}
		}
	}
}
