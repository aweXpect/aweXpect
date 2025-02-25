using System;

namespace aweXpect.Options;

/// <summary>
///     The check interval.
/// </summary>
public interface ICheckInterval
{
	/// <summary>
	///     The next interval for checking the condition.
	/// </summary>
	TimeSpan NextCheckInterval();
}
