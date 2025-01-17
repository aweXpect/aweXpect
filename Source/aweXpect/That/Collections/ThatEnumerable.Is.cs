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
	///     Verifies that the collection matches the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>
		Is<TItem>(
			this IExpectSubject<IEnumerable<TItem>> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new BeConstraint<TItem, object?>(it, doNotPopulateThisValue, expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection matches the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringCollectionMatchResult<IEnumerable<string>, IExpectSubject<IEnumerable<string>>>
		Is(this IExpectSubject<IEnumerable<string>> source,
			IEnumerable<string> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		return new StringCollectionMatchResult<IEnumerable<string>, IExpectSubject<IEnumerable<string>>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new BeConstraint<string, string>(it, doNotPopulateThisValue, expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}
}
