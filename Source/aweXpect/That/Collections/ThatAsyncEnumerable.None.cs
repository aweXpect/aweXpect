#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Expect that no items of the <see cref="IAsyncEnumerable{TItem}" />…
	/// </summary>
	public static Elements<TItem> None<TItem>(
		this IThat<IAsyncEnumerable<TItem>?> subject)
		=> new(subject, EnumerableQuantifier.None);

	/// <summary>
	///     Expect that no items of the <see cref="IAsyncEnumerable{TItem}" />…
	/// </summary>
	public static Elements None(
		this IThat<IAsyncEnumerable<string?>> subject)
		=> new(subject, EnumerableQuantifier.None);
}
#endif
