namespace aweXpect.Recording;

/// <summary>
///     A recording of events on a subject of type <typeparamref name="TSubject" />.
/// </summary>
#pragma warning disable S2326 // Unused type parameters should be removed
public interface IEventRecording<TSubject>
{
	/// <summary>
	///     Stops the recording of events.
	/// </summary>
	IEventRecordingResult Stop();
}
#pragma warning restore S2326
