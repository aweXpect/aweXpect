using System;
using System.Threading;

namespace aweXpect.Customization;

public partial class Customize : ICustomizeRecording
{
	private static readonly int DefaultTimeoutSeconds = 30;

	private readonly AsyncLocal<TimeSpan?> _defaultTimeout = new();

	/// <summary>
	///     Customizes the recording settings.
	/// </summary>
	public static ICustomizeRecording Recording => Instance;

	/// <inheritdoc />
	TimeSpan ICustomizeRecording.DefaultTimeout
		=> _defaultTimeout.Value ?? TimeSpan.FromSeconds(DefaultTimeoutSeconds);

	/// <inheritdoc />
	IDisposable ICustomizeRecording.SetDefaultTimeout(TimeSpan timeout)
	{
		_defaultTimeout.Value = timeout;
		return new ActionDisposable(() => _defaultTimeout.Value = null);
	}
}
