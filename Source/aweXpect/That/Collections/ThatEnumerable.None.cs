﻿using System.Collections;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
#if NET8_0_OR_GREATER
using System.Collections.Immutable;
#endif

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection no items…
	/// </summary>
	public static Elements<TItem> None<TItem>(
		this IThat<IEnumerable<TItem>?> subject)
		=> new(subject, EnumerableQuantifier.None(subject.Get().ExpectationBuilder.ExpectationGrammars |
		                                          ExpectationGrammars.Plural));

	/// <summary>
	///     Verifies that in the collection no items…
	/// </summary>
	public static Elements None(
		this IThat<IEnumerable<string?>?> subject)
		=> new(subject, EnumerableQuantifier.None(subject.Get().ExpectationBuilder.ExpectationGrammars |
		                                          ExpectationGrammars.Plural));

	/// <summary>
	///     Verifies that in the collection no items…
	/// </summary>
	public static ElementsForEnumerable<IEnumerable> None(
		this IThat<IEnumerable> subject)
		=> new(subject, EnumerableQuantifier.None(subject.Get().ExpectationBuilder.ExpectationGrammars |
		                                          ExpectationGrammars.Plural));

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that in the collection no items…
	/// </summary>
	public static ElementsForStructEnumerable<ImmutableArray<TItem>, TItem> None<TItem>(
		this IThat<ImmutableArray<TItem>> subject)
		=> new(subject, EnumerableQuantifier.None(subject.Get().ExpectationBuilder.ExpectationGrammars |
		                                          ExpectationGrammars.Plural));
#endif

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that in the collection no items…
	/// </summary>
	public static ElementsForStructEnumerable<ImmutableArray<string?>> None(
		this IThat<ImmutableArray<string?>> subject)
		=> new(subject, EnumerableQuantifier.None(subject.Get().ExpectationBuilder.ExpectationGrammars |
		                                          ExpectationGrammars.Plural));
#endif
}
