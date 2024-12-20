using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that the collection matches the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		Be<TItem>(
			this IThat<IEnumerable<TItem>> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>(source.ExpectationBuilder
				.AddConstraint(it
					=> new BeConstraint<TItem, object?>(it, doNotPopulateThisValue, expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}
	/// <summary>
	///     Verifies that the collection matches the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringCollectionMatchResult<IEnumerable<string>, IThat<IEnumerable<string>>>
		Be(this IThat<IEnumerable<string>> source,
			IEnumerable<string> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		return new StringCollectionMatchResult<IEnumerable<string>, IThat<IEnumerable<string>>>(source.ExpectationBuilder
				.AddConstraint(it
					=> new BeConstraint<string, string>(it, doNotPopulateThisValue, expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}
}
