using System.Collections.Generic;
using Testably.Expectations.Core;

namespace Testably.Expectations;

/// <summary>
///     Expectations on <see cref="IEnumerable{T}" />..
/// </summary>
public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Start delegate expectations on the current enumerable of <typeparamref name="TItem" /> values.
	/// </summary>
	public static IThat<IEnumerable<TItem>> Should<TItem>(
		this IExpectSubject<IEnumerable<TItem>> subject)
		=> subject.Should(ExpectationBuilder.NoAction);
}
