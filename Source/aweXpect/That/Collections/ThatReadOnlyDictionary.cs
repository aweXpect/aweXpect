using System.Collections.Generic;
using System.Linq;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="IReadOnlyDictionary{TKey,TValue}" />.
/// </summary>
public static partial class ThatReadOnlyDictionary
{
	internal static bool ContainsValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TValue value)
		=> value is null
			? dictionary.Any(x => x.Value is null)
			: dictionary.Any(x => value.Equals(x.Value));
}
