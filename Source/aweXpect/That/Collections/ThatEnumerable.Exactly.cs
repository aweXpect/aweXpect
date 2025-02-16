using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection exactly <paramref name="expected" /> items…
	/// </summary>
	public static Elements<TItem> Exactly<TItem>(
		this IThat<IEnumerable<TItem>?> subject,
		int expected)
		=> new(subject, EnumerableQuantifier.Exactly(expected));

	/// <summary>
	///     Verifies that in the collection exactly <paramref name="expected" /> items…
	/// </summary>
	public static Elements Exactly(
		this IThat<IEnumerable<string?>?> subject,
		int expected)
		=> new(subject, EnumerableQuantifier.Exactly(expected));
}
