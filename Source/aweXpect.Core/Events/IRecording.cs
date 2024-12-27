using System;

namespace aweXpect.Events;

/// <summary>
///     A recording of events on a subject of type <typeparamref name="TSubject"/>.
/// </summary>
/// <remarks>
///     Stops recording events when disposed.
/// </remarks>
public interface IRecording<TSubject> : IDisposable
{
	/// <summary>
	///     Gets the number of recorded events for <paramref name="eventName" /> that match the optional <paramref name="filter" />.
	/// </summary>
	int GetEventCount(string eventName, Func<object?[], bool>? filter = null);

	/// <summary>
	///     Returns a formatted string for the recorded events for <paramref name="eventName" />.
	/// </summary>
	string ToString(string eventName, string indent);
}
