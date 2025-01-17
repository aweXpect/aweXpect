#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Expect that all items of the <see cref="IAsyncEnumerable{TItem}" />…
	/// </summary>
	public static Elements<TItem> All<TItem>(
		this IExpectSubject<IAsyncEnumerable<TItem>> subject)
	=> new(subject, EnumerableQuantifier.All);
	
	/// <summary>
	///     Expect that all items of the <see cref="IAsyncEnumerable{TItem}" />…
	/// </summary>
	public static Elements All(
		this IExpectSubject<IAsyncEnumerable<string?>> subject)
		=> new(subject, EnumerableQuantifier.All);
}
#endif
