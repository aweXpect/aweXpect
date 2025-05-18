using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection less than <paramref name="maximum" /> items…
	/// </summary>
	public static Elements<TItem> LessThan<TItem>(
		this IThat<IEnumerable<TItem>?> subject,
		int maximum)
		=> new(subject, EnumerableQuantifier.LessThan(maximum));

	/// <summary>
	///     Verifies that in the collection less than <paramref name="maximum" /> items…
	/// </summary>
	public static Elements LessThan(
		this IThat<IEnumerable<string?>?> subject,
		int maximum)
		=> new(subject, EnumerableQuantifier.LessThan(maximum));
}
