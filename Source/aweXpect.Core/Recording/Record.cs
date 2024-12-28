namespace aweXpect.Recording;

/// <summary>
///     Static factory for creating callback recordings.
/// </summary>
public static class Record
{
	/// <summary>
	///     Record callback without parameters.
	/// </summary>
	public static ICallbackRecording Callback()
		=> new CallbackRecording();

	/// <summary>
	///     Record callback with a parameter of type <typeparamref name="TParameter" />.
	/// </summary>
	public static ICallbackRecording<TParameter> Callback<TParameter>()
		=> new CallbackRecording<TParameter>();
}
