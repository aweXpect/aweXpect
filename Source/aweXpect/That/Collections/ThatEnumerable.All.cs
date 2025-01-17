using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Expect that all items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements<TItem> All<TItem>(
		this IExpectSubject<IEnumerable<TItem>> subject)
		=> new(subject, EnumerableQuantifier.All);

	/// <summary>
	///     Expect that all items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements All(
		this IExpectSubject<IEnumerable<string?>> subject)
		=> new(subject, EnumerableQuantifier.All);
}
