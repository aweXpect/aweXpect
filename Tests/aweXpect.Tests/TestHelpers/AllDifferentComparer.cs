using System.Collections.Generic;

namespace aweXpect.Tests.TestHelpers;

/// <summary>
///     A comparer that treats all items as different.
/// </summary>
internal class AllDifferentComparer : IEqualityComparer<object>
{
	bool IEqualityComparer<object>.Equals(object? x, object? y) => false;

	int IEqualityComparer<object>.GetHashCode(object obj) => obj.GetHashCode();
}
