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
	///     Verifies that the collection matches the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>
		Is<TItem>(
			this IThat<IAsyncEnumerable<TItem>?> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new BeConstraint<TItem, object?>(it, doNotPopulateThisValue, expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection matches the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringCollectionMatchResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>
		Is(
			this IThat<IAsyncEnumerable<string>> source,
			IEnumerable<string> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		return new StringCollectionMatchResult<IAsyncEnumerable<string>, IThat<IAsyncEnumerable<string>>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new BeConstraint<string, string>(it, doNotPopulateThisValue, expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}
}
#endif
