﻿#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Verifies that in the collection more than <paramref name="minimum" /> items…
	/// </summary>
	public static Elements<TItem> MoreThan<TItem>(
		this IThat<IAsyncEnumerable<TItem>?> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.MoreThan(minimum));

	/// <summary>
	///     Verifies that in the collection more than <paramref name="minimum" /> items…
	/// </summary>
	public static Elements MoreThan(
		this IThat<IAsyncEnumerable<string?>?> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.MoreThan(minimum));
}
#endif
