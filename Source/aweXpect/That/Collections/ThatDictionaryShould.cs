using System.Collections.Generic;
using System.Linq;
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

	internal static bool ContainsValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
		=> dictionary.Any(x => value?.Equals(x.Value) == true);
}
