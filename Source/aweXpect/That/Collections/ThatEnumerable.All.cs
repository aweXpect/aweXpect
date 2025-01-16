using System;
using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Start expectations on the current enumerable of <typeparamref name="TItem" /> values.
	/// </summary>
	public static ThatAll<TItem> All<TItem>(
		this IExpectSubject<IEnumerable<TItem>> subject)
		=> throw new NotImplementedException();

	/// <summary>
	///     Expectations on all items of the <see cref="IEnumerable{TItem}" />.
	/// </summary>
	public partial class ThatAll<TItem>
	{
	}
}
