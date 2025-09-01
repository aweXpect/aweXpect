﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using aweXpect.Core;

namespace aweXpect.Options;

public partial class CollectionMatchOptions
{
	private sealed class AnyOrderCollectionMatcher<T, T2>(
		EquivalenceRelations equivalenceRelation,
		IEnumerable<T> expected)
		: AnyOrderCollectionMatcherBase<T, T2, T>(equivalenceRelation, expected)
		where T : T2
	{
#if NET8_0_OR_GREATER
		protected override ValueTask<bool> AreConsideredEqual(T value, T expected, IOptionsEquality<T2> options)
#else
		protected override Task<bool> AreConsideredEqual(T value, T expected, IOptionsEquality<T2> options)
#endif
			=> options.AreConsideredEqual(value, expected);
	}

	private sealed class AnyOrderFromExpectationCollectionMatcher<T, T2>(
		EquivalenceRelations equivalenceRelation,
		IEnumerable<ExpectationItem<T>> expected)
		: AnyOrderCollectionMatcherBase<T, T2, ExpectationItem<T>>(equivalenceRelation, expected)
		where T : T2
	{
#if NET8_0_OR_GREATER
		protected override ValueTask<bool>
#else
		protected override Task<bool>
#endif
			AreConsideredEqual(T value, ExpectationItem<T> expected, IOptionsEquality<T2> options)
			=> expected.IsMetBy(value);
	}

	private sealed class AnyOrderFromPredicateCollectionMatcher<T, T2>(
		EquivalenceRelations equivalenceRelation,
		IEnumerable<Expression<Func<T, bool>>> expected)
		: AnyOrderCollectionMatcherBase<T, T2, Expression<Func<T, bool>>>(equivalenceRelation, expected)
		where T : T2
	{
#if NET8_0_OR_GREATER
		protected override ValueTask<bool> AreConsideredEqual(T value, Expression<Func<T, bool>> expected,
			IOptionsEquality<T2> options)
			=> ValueTask.FromResult(expected.Compile().Invoke(value));
#else
		protected override Task<bool> AreConsideredEqual(T value, Expression<Func<T, bool>> expected,
			IOptionsEquality<T2> options)
			=> Task.FromResult(expected.Compile().Invoke(value));
#endif
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

#if NET8_0_OR_GREATER
		public async ValueTask<(bool, string?)>
#else
		public async Task<(bool, string?)>
#endif
			Verify(string it, T value, IOptionsEquality<T2> options, int maximumNumber)
		{
			if (await All(_missingItems, e => AreConsideredEqual(value, e, options), true))
			{
				_additionalItems.Add(_index, value);
			}

			await RemoveFirst(_missingItems, e => AreConsideredEqual(value, e, options));
			_index++;
			return (_additionalItems.Count > 2 * maximumNumber, null);
		}

#if NET8_0_OR_GREATER
		public ValueTask<(bool, string?)>
#else
		public Task<(bool, string?)>
#endif
			VerifyComplete(string it, IOptionsEquality<T2> options, int maximumNumber)
		{
			if (_additionalItems.Count + _missingItems.Count > 2 * maximumNumber)
			{
#if NET8_0_OR_GREATER
				return ValueTask.FromResult<(bool, string?)>((true, null));
#else
				return Task.FromResult<(bool, string?)>((true, null));
#endif
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

			string? error = ReturnErrorString(it, errors);
#if NET8_0_OR_GREATER
			return ValueTask.FromResult<(bool, string?)>((error != null, error));
#else
				return Task.FromResult<(bool, string?)>((error != null, error));
#endif
		}

#if NET8_0_OR_GREATER
		protected abstract ValueTask<bool>
#else
		protected abstract Task<bool>
#endif
			AreConsideredEqual(T value, T3 expected, IOptionsEquality<T2> options);
	}
}
