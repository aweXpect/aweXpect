using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Text;

namespace aweXpect.Recording;

internal sealed class EventRecorder(string eventName) : IDisposable
{
	private readonly ConcurrentQueue<RecordedEvent> _eventQueue = new();
	private Action? _onDispose;

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
				$"The {eventName} event contains too many parameters ({handlerType.GetParameters().Length}): {Formatter.Format(handlerType.GetParameters().Select(x => x.ParameterType))}");
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
		=> _eventQueue.Enqueue(new RecordedEvent(eventName));

	public void RecordEvent<T1>(T1 parameter1)
		=> _eventQueue.Enqueue(new RecordedEvent(eventName, parameter1));

	public void RecordEvent<T1, T2>(T1 parameter1, T2 parameter2)
		=> _eventQueue.Enqueue(new RecordedEvent(eventName, parameter1, parameter2));

	public void RecordEvent<T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3)
		=> _eventQueue.Enqueue(new RecordedEvent(eventName, parameter1, parameter2, parameter3));

	public void RecordEvent<T1, T2, T3, T4>(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4)
		=> _eventQueue.Enqueue(new RecordedEvent(eventName, parameter1, parameter2, parameter3, parameter4));

	/// <summary>
	///     Returns a formatted string for all recorded events.
	/// </summary>
	public override string ToString()
		=> Formatter.Format(_eventQueue, FormattingOptions.MultipleLines);

	/// <summary>
	///     Gets the number of recorded events that match the <paramref name="filter" />.
	/// </summary>
	public int GetEventCount(Func<object?[], bool>? filter)
	{
		if (filter != null)
		{
			return _eventQueue.Count(x => filter(x.Parameters));
		}

		return _eventQueue.Count;
	}

	private readonly struct RecordedEvent(string name, params object?[] parameters)
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
