using System;

namespace aweXpect.Results;

/// <summary>
///     An intermediate type to collect the number of times.
/// </summary>
public class CountTimesResult<TTarget>(
	Func<int, TTarget> callback)
{
	/// <summary>
	///     …once.
	/// </summary>
	public TTarget Once()
		=> callback(1);

	/// <summary>
	///     …twice.
	/// </summary>
	public TTarget Twice()
		=> callback(2);
}
