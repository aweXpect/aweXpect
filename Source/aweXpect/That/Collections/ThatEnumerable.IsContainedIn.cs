using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the collection is contained in the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionBeContainedInResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>
		IsContainedIn<TItem>(
			this IExpectSubject<IEnumerable<TItem>> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		return new ObjectCollectionBeContainedInResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new BeConstraint<TItem, object?>(it, doNotPopulateThisValue, expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection is contained in the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringCollectionBeContainedInResult<IEnumerable<string>, IExpectSubject<IEnumerable<string>>>
		IsContainedIn(this IExpectSubject<IEnumerable<string>> source,
			IEnumerable<string> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		return new StringCollectionBeContainedInResult<IEnumerable<string>, IExpectSubject<IEnumerable<string>>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new BeConstraint<string, string>(it, doNotPopulateThisValue, expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}
}
