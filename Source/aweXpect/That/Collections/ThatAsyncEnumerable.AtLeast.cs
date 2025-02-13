#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Verifies that in the collection at least <paramref name="minimum" /> items…
	/// </summary>
	public static Elements<TItem> AtLeast<TItem>(
		this IThat<IAsyncEnumerable<TItem>?> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum));

	/// <summary>
	///     Verifies that in the collection at least <paramref name="minimum" /> items…
	/// </summary>
	public static Elements AtLeast(
		this IThat<IAsyncEnumerable<string?>?> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum));
}
#endif
