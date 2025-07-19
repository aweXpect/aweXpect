#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aweXpect.Helpers;

internal interface IMaterializedEnumerable<out T> : ICountable
{
	IReadOnlyList<T> MaterializedItems { get; }
	Task MaterializeItems(int minimumNumberOfItems);
}
#endif
