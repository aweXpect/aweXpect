﻿using System;

namespace aweXpect.Results;

/// <summary>
///     The result for counting items in a collection.
/// </summary>
public class CollectionCountResult<TReturn>(Func<EnumerableQuantifier, TReturn> factory)
{
	/// <summary>
	///     Verifies that the collection has exactly <paramref name="expected" /> items.
	/// </summary>
	public TReturn EqualTo(int expected)
		=> factory(EnumerableQuantifier.Exactly(expected));

	/// <summary>
	///     Verifies that the collection has at least <paramref name="minimum" /> items.
	/// </summary>
	public TReturn AtLeast(int minimum)
		=> factory(EnumerableQuantifier.AtLeast(minimum));

	/// <summary>
	///     Verifies that the collection has at most <paramref name="maximum" /> items.
	/// </summary>
	public TReturn AtMost(int maximum)
		=> factory(EnumerableQuantifier.AtMost(maximum));

	/// <summary>
	///     Verifies that the collection has between <paramref name="minimum" />…
	/// </summary>
	public BetweenResult<TReturn> Between(int minimum)
		=> new(maximum => factory(EnumerableQuantifier.Between(minimum, maximum)));
}
