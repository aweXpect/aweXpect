namespace aweXpect.Recording;

/// <summary>
///     A recording of events on a subject of type <typeparamref name="TSubject" />.
/// </summary>
/// <remarks>
///     Stops recording events when disposed.
/// </remarks>
public interface IEventRecording<TSubject>
{
	/// <summary>
	///     Stops the recording of events.
	/// </summary>
	IEventRecordingResult Stop();
}
