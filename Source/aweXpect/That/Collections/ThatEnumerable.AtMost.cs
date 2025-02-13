using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection at most <paramref name="maximum" /> items…
	/// </summary>
	public static Elements<TItem> AtMost<TItem>(
		this IThat<IEnumerable<TItem>?> subject,
		int maximum)
		=> new(subject, EnumerableQuantifier.AtMost(maximum));

	/// <summary>
	///     Verifies that in the collection at most <paramref name="maximum" /> items…
	/// </summary>
	public static Elements AtMost(
		this IThat<IEnumerable<string?>?> subject,
		int maximum)
		=> new(subject, EnumerableQuantifier.AtMost(maximum));
}
