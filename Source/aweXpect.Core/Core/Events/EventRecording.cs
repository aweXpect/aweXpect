using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using aweXpect.Options;

namespace aweXpect.Core.Events;

internal class EventRecording<T> : IDisposable
{
	private readonly Dictionary<string, EventRecorder> _recorders = new();

	/// <summary>
	///     Creates a new recording the given <paramref name="eventNames" /> that are triggered on the
	///     <paramref name="subject" />.
	/// </summary>
	public EventRecording(T subject, IEnumerable<string> eventNames)
	{
		EventInfo[] events = typeof(T).GetEvents();
		foreach (string? eventName in eventNames)
		{
			EventRecorder? recorder = new(eventName);
			_recorders.Add(eventName, recorder);
			EventInfo? @event = events.FirstOrDefault(x => x.Name == eventName);
			if (@event == null)
			{
				throw new NotSupportedException($"Event {eventName} is not supported on {Formatter.Format(subject)}");
			}

			recorder.Attach(new WeakReference(subject), @event);
		}
	}

	/// <inheritdoc />
	public void Dispose()
	{
		foreach (EventRecorder recorder in _recorders.Values)
		{
			recorder.Dispose();
		}
	}

	/// <summary>
	///     Gets the number of recorded events for <paramref name="eventName" /> that match the <paramref name="filter" />.
	/// </summary>
	public int GetEventCount(string eventName, TriggerEventFilter? filter)
		=> _recorders[eventName].GetEventCount(filter);

	/// <summary>
	///     Returns a formatted string for the recorded events for <paramref name="eventName" />.
	/// </summary>
	public string ToString(string eventName, string indent)
		=> _recorders[eventName].ToString(indent);
}
