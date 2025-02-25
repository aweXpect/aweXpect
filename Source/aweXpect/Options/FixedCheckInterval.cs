using System;

namespace aweXpect.Options;

/// <summary>
///     A <see cref="ICheckInterval" /> which always returns a fixed <paramref name="interval" />.
/// </summary>
public class FixedCheckInterval(TimeSpan interval) : ICheckInterval
{
	/// <inheritdoc cref="ICheckInterval.NextCheckInterval()" />
	public TimeSpan NextCheckInterval() => interval;
}
