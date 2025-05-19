#if NET8_0_OR_GREATER
using System.Collections.Generic;

namespace aweXpect.Helpers;

internal interface IMaterializedEnumerable<out T> : ICountable
{
	IReadOnlyList<T> MaterializedItems { get; }
}
#endif
