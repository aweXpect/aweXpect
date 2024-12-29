using System;
using aweXpect.Recording;

namespace aweXpect.Customization;

/// <summary>
///     Customizes the recording settings.
/// </summary>
public interface ICustomizeRecording
{
	/// <summary>
	///     The default timeout for the <see cref="SignalCounter" />.
	/// </summary>
	TimeSpan DefaultTimeout { get; }

	/// <summary>
	///     Specifies the default timeout for the <see cref="SignalCounter" />.
	/// </summary>
	/// <returns>
	///     An object, that will revert the default timeout to 30 seconds upon disposal.
	/// </returns>
	IDisposable SetDefaultTimeout(TimeSpan timeout);
}
