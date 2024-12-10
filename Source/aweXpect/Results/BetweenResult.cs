using System;

namespace aweXpect.Results;

/// <summary>
///     An intermediate type to collect the maximum of the range.
/// </summary>
public class BetweenResult<TTarget, TExpectation>(
	Func<int, Action<TExpectation>, TTarget> callback,
	Func<int, TTarget> itemsCallback)
{
	/// <summary>
	///     ... and <paramref name="maximum" /> items satisfy the <paramref name="expectations" />.
	/// </summary>
	public TTarget And(int maximum,
		Action<TExpectation> expectations)
		=> callback(maximum, expectations);

	/// <summary>
	///     ... and <paramref name="maximum" /> items.
	/// </summary>
	public ItemsResult<TTarget> And(int maximum)
		=> new(itemsCallback(maximum));
}
