using System;

namespace aweXpect.Results;

/// <summary>
///     An intermediate type to collect the maximum of the range.
/// </summary>
public class BetweenResult<TTarget>(
	Func<int, TTarget> callback)
{
	/// <summary>
	///     …and <paramref name="maximum" /> items…
	/// </summary>
	public TTarget And(Times maximum)
		=> callback(maximum.Value);
}
