#if NET6_0_OR_GREATER
using System.Collections.Generic;
using Testably.Expectations.Core;

namespace Testably.Expectations;

/// <summary>
///     Expectations on <see cref="IAsyncEnumerable{T}" />.
/// </summary>
public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Start delegate expectations on the current async enumerable of <typeparamref name="TItem" /> values.
	/// </summary>
	public static IThat<IAsyncEnumerable<TItem>> Should<TItem>(
		this IExpectSubject<IAsyncEnumerable<TItem>> subject)
		=> subject.Should(ExpectationBuilder.NoAction);
}

#endif
