using System;

namespace Testably.Expectations;

/// <summary>
///     An intermediate type to collect the maximum of the range.
/// </summary>
public class BetweenResult<TTarget>(
	Func<int, TTarget> callback)
{
	/// <summary>
	///     ... and <paramref name="maximum" />...
	/// </summary>
	public TTarget And(int maximum)
		=> callback(maximum);
}
