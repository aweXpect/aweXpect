using System.Collections.Generic;
#if NET8_0_OR_GREATER
using System.Collections.Immutable;
#endif
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection no items…
	/// </summary>
	public static Elements<TItem, IEnumerable<TItem>?> None<TItem>(
		this IThat<IEnumerable<TItem>?> subject)
		=> new(subject,
			EnumerableQuantifier.None(subject.Get().ExpectationBuilder.ExpectationGrammars |
			                          ExpectationGrammars.Plural));

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that in the collection no items…
	/// </summary>
	public static Elements<TItem, ImmutableArray<TItem>> None<TItem>(
		this IThat<ImmutableArray<TItem>> subject)
		=> new(subject, EnumerableQuantifier.None(subject.Get().ExpectationBuilder.ExpectationGrammars));
#endif

	/// <summary>
	///     Verifies that in the collection no items…
	/// </summary>
	public static Elements<IEnumerable<string?>?> None(
		this IThat<IEnumerable<string?>?> subject)
		=> new(subject,
			EnumerableQuantifier.None(subject.Get().ExpectationBuilder.ExpectationGrammars |
			                          ExpectationGrammars.Plural));
}
