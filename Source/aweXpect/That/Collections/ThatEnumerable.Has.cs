using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Expect that the <paramref name="subject" /> has…
	/// </summary>
	public static IThatHas<IEnumerable<TItem>> Has<TItem>(
		this IThat<IEnumerable<TItem>> subject)
		=> subject.ThatHas();
}
