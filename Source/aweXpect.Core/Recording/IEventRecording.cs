using System;
using System.Threading.Tasks;

namespace aweXpect.Recording;

/// <summary>
///     A recording of events on a subject of type <typeparamref name="TSubject" />.
/// </summary>
#pragma warning disable S2326 // Unused type parameters should be removed
public interface IEventRecording<TSubject>
{
	/// <summary>
	///     Stops the recording of events when checked events <see paramref="areFound" />
	///     or the <paramref name="timeout" /> elapsed.
	/// </summary>
	Task<IEventRecordingResult> StopWhen(Func<IEventRecordingResult, bool> areFound, TimeSpan timeout);
}
#pragma warning restore S2326
