using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="IDictionary{TKey,TValue}" />.
/// </summary>
public static partial class ThatDictionaryShould
{
	/// <summary>
	///     Start expectations on the current dictionary.
	/// </summary>
	public static IThat<IDictionary<TKey, TValue>> Should<TKey, TValue>(
		this IExpectSubject<IDictionary<TKey, TValue>> subject)
		=> subject.Should(That.WithoutAction);
}
