using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Expect that at most <paramref name="maximum" /> items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements<TItem> AtMost<TItem>(
		this IExpectSubject<IEnumerable<TItem>> subject,
		int maximum)
		=> new(subject, EnumerableQuantifier.AtMost(maximum));

	/// <summary>
	///     Expect that at most <paramref name="maximum" /> items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements AtMost(
		this IExpectSubject<IEnumerable<string>> subject,
		int maximum)
		=> new(subject, EnumerableQuantifier.AtMost(maximum));
}
