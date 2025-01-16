using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Expect that exactly <paramref name="expected" /> items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements<TItem> Exactly<TItem>(
		this IExpectSubject<IEnumerable<TItem>> subject,
		int expected)
		=> new(subject, EnumerableQuantifier.Exactly(expected));

	/// <summary>
	///     Expect that exactly <paramref name="expected" /> items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements Exactly(
		this IExpectSubject<IEnumerable<string>> subject,
		int expected)
		=> new(subject, EnumerableQuantifier.Exactly(expected));
}
