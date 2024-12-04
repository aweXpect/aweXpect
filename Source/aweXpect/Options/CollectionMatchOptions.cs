using System;
using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect.Options;

/// <summary>
///     Options for matching a collection.
/// </summary>
public partial class CollectionMatchOptions
{
	/// <summary>
	///     Specifies the equivalence relation between subject and expected.
	/// </summary>
	[Flags]
	public enum EquivalenceRelation
	{
		/// <summary>
		///     The subject and expected collection must be equivalent (have the same items)
		/// </summary>
		Equivalent = 1,

		/// <summary>
		///     The expected collection contains at least one additional item.
		/// </summary>
		ProperSubset = 2 | Subset,

		/// <summary>
		///     The subject collection contains at least one additional item.
		/// </summary>
		ProperSuperset = 2 | Superset,

		/// <summary>
		///     The expected collection can contain additional items.
		/// </summary>
		Subset = 4,

		/// <summary>
		///     The subject collection can contain additional items.
		/// </summary>
		Superset = 8
	}

	private EquivalenceRelation _equivalenceRelation = EquivalenceRelation.Equivalent;
	private bool _ignoringDuplicates;
	private bool _inAnyOrder;

	/// <summary>
	///     Specifies the equivalence relation between subject and expected.
	/// </summary>
	public void SetEquivalenceRelation(EquivalenceRelation equivalenceRelation)
		=> _equivalenceRelation = equivalenceRelation;

	/// <summary>
	///     Ignores the order in the subject and expected values.
	/// </summary>
	public void InAnyOrder() => _inAnyOrder = true;

	/// <summary>
	///     Ignores duplicates in both collections.
	/// </summary>
	public void IgnoringDuplicates() => _ignoringDuplicates = true;

	/// <summary>
	///     Get the collection matcher for the <paramref name="expected" /> enumerable.
	/// </summary>
	public ICollectionMatcher<T, T2> GetCollectionMatcher<T, T2>(IEnumerable<T> expected)
		where T : T2
		=> (_inAnyOrder, _ignoringDuplicates) switch
		{
			(true, true) => new AnyOrderIgnoreDuplicatesCollectionMatcher<T, T2>(_equivalenceRelation, expected),
			(true, false) => new AnyOrderCollectionMatcher<T, T2>(_equivalenceRelation, expected),
			(false, true) => new SameOrderIgnoreDuplicatesCollectionMatcher<T, T2>(_equivalenceRelation, expected),
			(false, false) => new SameOrderCollectionMatcher<T, T2>(_equivalenceRelation, expected)
		};

	/// <inheritdoc />
	public override string ToString()
		=> (_inAnyOrder, _ignoringDuplicates) switch
		{
			(true, true) => ToString(_equivalenceRelation) + " in any order ignoring duplicates",
			(true, false) => ToString(_equivalenceRelation) + " in any order",
			(false, true) => ToString(_equivalenceRelation) + " ignoring duplicates",
			(false, false) => ToString(_equivalenceRelation)
		};

	private static string ToString(EquivalenceRelation equivalenceRelation)
		=> equivalenceRelation switch
		{
			EquivalenceRelation.Superset => " or more items",
			EquivalenceRelation.ProperSuperset => " and at least one more item",
			EquivalenceRelation.Subset => " or less items",
			EquivalenceRelation.ProperSubset => " and at least one item less",
			_ => ""
		};
}
