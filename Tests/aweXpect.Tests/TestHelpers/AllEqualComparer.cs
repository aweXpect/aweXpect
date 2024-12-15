using System.Collections.Generic;

namespace aweXpect.Tests.TestHelpers;

/// <summary>
///     A comparer that treats all items as equal.
/// </summary>
internal class AllEqualComparer : IEqualityComparer<object>
{
	bool IEqualityComparer<object>.Equals(object? x, object? y) => true;

	int IEqualityComparer<object>.GetHashCode(object obj) => obj.GetHashCode();
}
