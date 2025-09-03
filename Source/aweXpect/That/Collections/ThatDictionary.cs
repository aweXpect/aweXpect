using System.Collections.Generic;
using System.Linq;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="IDictionary{TKey,TValue}" />.
/// </summary>
public static partial class ThatDictionary
{
	internal static bool ContainsValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
		=> value is null
			? dictionary.Any(x => x.Value is null)
			: dictionary.Any(x => value.Equals(x.Value));
}
