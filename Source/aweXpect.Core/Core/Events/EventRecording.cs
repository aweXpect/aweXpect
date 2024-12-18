using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using aweXpect.Core.Helpers;
using aweXpect.Options;

namespace aweXpect.Core.Events;

internal class EventRecording<T> : IDisposable
{
	private readonly Dictionary<string, EventRecorder> _recorders = new();

	public EventRecording(T actual, IEnumerable<string> eventNames)
	{
		EventInfo[] events = typeof(T).GetEvents();
		foreach (string? eventName in eventNames)
		{
			EventRecorder? recorder = new(eventName);
			_recorders.Add(eventName, recorder);
			EventInfo? @event = events.FirstOrDefault(x => x.Name == eventName);
			if (@event == null)
			{
				throw new NotSupportedException($"Event {eventName} is not supported on {Formatter.Format(actual)}");
			}

			recorder.Attach(new WeakReference(actual), @event);
		}
	}

	public void Dispose()
	{
		foreach (EventRecorder recorder in _recorders.Values)
		{
			recorder.Dispose();
		}
	}

	public int GetEventCount(string name, TriggerEventFilter? filter)
	{
		if (filter != null)
		{
			return _recorders[name].EventQueue.Count(x => filter.IsMatch(name, x.Parameters));
		}

		return _recorders[name].EventQueue.Count;
	}

	public string ToString(string name, string indent)
	{
		ConcurrentQueue<OccurredEvent>? eventQueue = _recorders[name].EventQueue;
		if (string.IsNullOrEmpty(indent))
		{
			return Formatter.Format(eventQueue, FormattingOptions.MultipleLines);
		}

		return Formatter.Format(eventQueue, FormattingOptions.MultipleLines).Indent(indent, false);
	}

	private sealed class EventRecorder(string name) : IDisposable
	{
		private Action? _onDispose;
		public ConcurrentQueue<OccurredEvent> EventQueue { get; } = new();

		public void Dispose() => _onDispose?.Invoke();

		public void Attach(WeakReference subject, EventInfo eventInfo)
		{
			MethodInfo handlerType = eventInfo.EventHandlerType!.GetMethod("Invoke")!;
			Delegate? handler = null;
			foreach (MethodInfo method in typeof(EventRecorder).GetMethods().Where(x => x.Name == nameof(RecordEvent)))
			{
				if (method.GetParameters().Length == handlerType.GetParameters().Length)
				{
					MethodInfo handlerMethod = method;
					if (handlerType.GetParameters().Length > 0)
					{
						handlerMethod = method
							.MakeGenericMethod(handlerType.GetParameters()
								.Select(x => x.ParameterType)
								.ToArray());
					}

					handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, handlerMethod);
				}
			}

			if (handler == null)
			{
				throw new NotSupportedException(
					$"The event {name} contains too many parameters ({handlerType.GetParameters().Length}): {Formatter.Format(handlerType.GetParameters().Select(x => x.ParameterType))}");
			}

			eventInfo.AddEventHandler(subject.Target, handler);

			_onDispose = () =>
			{
				if (subject.Target is not null)
				{
					eventInfo.RemoveEventHandler(subject.Target, handler);
				}
			};
		}

		public void RecordEvent()
			=> EventQueue.Enqueue(new OccurredEvent(name));

		public void RecordEvent<T1>(T1 parameter1)
			=> EventQueue.Enqueue(new OccurredEvent(name, parameter1));

		public void RecordEvent<T1, T2>(T1 parameter1, T2 parameter2)
			=> EventQueue.Enqueue(new OccurredEvent(name, parameter1, parameter2));

		public void RecordEvent<T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3)
			=> EventQueue.Enqueue(new OccurredEvent(name, parameter1, parameter2, parameter3));

		public void RecordEvent<T1, T2, T3, T4>(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4)
			=> EventQueue.Enqueue(new OccurredEvent(name, parameter1, parameter2, parameter3, parameter4));
	}

	private readonly struct OccurredEvent(string name, params object?[] parameters)
	{
		public string Name { get; } = name;
		public object?[] Parameters { get; } = parameters;

		/// <inheritdoc />
		public override string ToString()
		{
			StringBuilder sb = new();
			sb.Append(Name).Append('(');
			if (Parameters.Length > 0)
			{
				foreach (object? parameter in Parameters)
				{
					Formatter.Format(sb, parameter);
					sb.Append(", ");
				}

				sb.Length -= 2;
			}

			sb.Append(')');
			return sb.ToString();
		}
	}
}
