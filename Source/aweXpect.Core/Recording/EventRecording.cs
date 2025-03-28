﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace aweXpect.Recording;

internal sealed class EventRecording<TSubject> : IEventRecording<TSubject>, IEventRecordingResult
	where TSubject : notnull
{
	private readonly Dictionary<string, EventRecorder> _recorders = new();
	private readonly string _subjectExpression;

	/// <summary>
	///     Creates a new recording the given <paramref name="eventNames" /> that are triggered on the
	///     <paramref name="subject" />.
	/// </summary>
	public EventRecording(TSubject subject, string subjectExpression, params string[] eventNames)
	{
		_subjectExpression = subjectExpression;
		EventInfo[] events = subject.GetType().GetEvents();
		if (eventNames.Length == 0)
		{
			eventNames = events.Select(x => x.Name).ToArray();
		}

		foreach (string? eventName in eventNames)
		{
			EventRecorder recorder = new(eventName);
			_recorders.Add(eventName, recorder);
			EventInfo? @event = events.FirstOrDefault(x => x.Name == eventName);
			if (@event == null)
			{
				throw new NotSupportedException($"Event {eventName} is not supported on {Formatter.Format(subject)}");
			}

			recorder.Attach(new WeakReference(subject), @event);
		}
	}

	/// <summary>
	///     Stops the recording of events.
	/// </summary>
	public IEventRecordingResult Stop()
	{
		foreach (EventRecorder recorder in _recorders.Values)
		{
			recorder.Dispose();
		}

		return this;
	}

	/// <summary>
	///     Gets the number of recorded events for <paramref name="eventName" /> that match the <paramref name="filter" />.
	/// </summary>
	public int GetEventCount(string eventName, Func<object?[], bool>? filter = null)
		=> _recorders[eventName].GetEventCount(filter);

	/// <summary>
	///     Returns a formatted string for the recorded events for <paramref name="eventName" />.
	/// </summary>
	public string ToString(string eventName)
		=> _recorders[eventName].ToString();

	/// <inheritdoc />
	public override string ToString()
		=> _subjectExpression;
}
