using System;
using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Start expectations on the current enumerable of <typeparamref name="TItem" /> values.
	/// </summary>
	public static IThatHas<IEnumerable<TItem>> Has<TItem>(
		this IExpectSubject<IEnumerable<TItem>> subject)
		=> throw new NotImplementedException();
}
