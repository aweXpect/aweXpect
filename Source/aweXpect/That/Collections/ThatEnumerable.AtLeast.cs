using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection at least <paramref name="minimum" /> items…
	/// </summary>
	public static Elements<TItem> AtLeast<TItem>(
		this IThat<IEnumerable<TItem>?> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum, subject.ThatIs().ExpectationBuilder.ExpectationGrammars));

	/// <summary>
	///     Verifies that in the collection at least <paramref name="minimum" /> items…
	/// </summary>
	public static Elements AtLeast(
		this IThat<IEnumerable<string?>?> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum));
}
