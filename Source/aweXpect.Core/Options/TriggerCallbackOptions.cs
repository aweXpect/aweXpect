using System;
using aweXpect.Recording;

namespace aweXpect.Options;

/// <summary>
///     Options for <see cref="ISignalCounter" />
/// </summary>
public class TriggerCallbackOptions
{
	/// <summary>
	///     The timeout to use for the recording.
	/// </summary>
	public TimeSpan? Timeout { get; set; }
}
