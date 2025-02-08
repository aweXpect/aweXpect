#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Verifies that the collection is contained in the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionBeContainedInResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
		IsContainedIn<TItem>(
			this IThat<IAsyncEnumerable<TItem>?> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		return new
			ObjectCollectionBeContainedInResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
				source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
					new IsConstraint<TItem, TItem>(it, doNotPopulateThisValue, expected, options, matchOptions)),
				source,
				options,
				matchOptions);
	}

	/// <summary>
	///     Verifies that the collection is contained in the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringCollectionBeContainedInResult<IAsyncEnumerable<string?>,
			IThat<IAsyncEnumerable<string?>?>>
		IsContainedIn(
			this IThat<IAsyncEnumerable<string?>?> source,
			IEnumerable<string?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		return new StringCollectionBeContainedInResult<IAsyncEnumerable<string?>,
			IThat<IAsyncEnumerable<string?>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new IsConstraint<string?, string?>(it, doNotPopulateThisValue, expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}
}
#endif
