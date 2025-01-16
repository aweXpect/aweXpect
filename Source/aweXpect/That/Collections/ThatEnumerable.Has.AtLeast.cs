using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Start expectations on the current enumerable of <typeparamref name="TItem" /> values.
	/// </summary>
	public static ItemsResult<AndOrResult<IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>>> AtLeast<TItem>(
		this IThatHas<IEnumerable<TItem>> subject,
		int minimum)
		=> throw new NotImplementedException();
	/// <summary>
	///     Start expectations on the current enumerable of <typeparamref name="TItem" /> values.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>> AtLeast<TItem>(
		this IThatHas<IEnumerable<TItem>> subject,
		int minimum,
		Action<IExpectSubject<TItem>> where)
		=> throw new NotImplementedException();
}
