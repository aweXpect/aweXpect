using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Expect that at least <paramref name="minimum" /> items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements<TItem> AtLeast<TItem>(
		this IExpectSubject<IEnumerable<TItem>> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum));

	/// <summary>
	///     Expect that at least <paramref name="minimum" /> items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements AtLeast(
		this IExpectSubject<IEnumerable<string>> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum));
}
