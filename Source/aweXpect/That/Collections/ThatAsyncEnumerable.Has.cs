#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Verifies that the <paramref name="subject" /> has…
	/// </summary>
	public static IThatHas<IAsyncEnumerable<TItem>?> Has<TItem>(
		this IThat<IAsyncEnumerable<TItem>?> subject)
		=> subject.ThatHas();
}
#endif
