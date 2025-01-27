using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Expect that no items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements<TItem> None<TItem>(
		this IThat<IEnumerable<TItem>?> subject)
		=> new(subject, EnumerableQuantifier.None);

	/// <summary>
	///     Expect that no items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements None(
		this IThat<IEnumerable<string?>> subject)
		=> new(subject, EnumerableQuantifier.None);
}
