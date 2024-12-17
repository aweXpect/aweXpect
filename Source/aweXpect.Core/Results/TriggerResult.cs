using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Text;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result for a <see cref="TriggersExtensions.Triggers{T}" /> expectation.
/// </summary>
public class TriggerResult<T>(IThat<T> returnValue, string eventName)
{
	/// <summary>
	///     Executes the <paramref name="callback" /> while monitoring the triggered events.
	/// </summary>
	public CountResult<T, IThat<T>> While(Action<T> callback)
	{
		Quantifier quantifier = new();
		returnValue.ExpectationBuilder.AddConstraint(it
			=> new EventConstraint(it, eventName, callback, GetFilter(), quantifier));
		return new CountResult<T, IThat<T>>(returnValue.ExpectationBuilder, returnValue, quantifier);
	}

	/// <summary>
	///     Gets the event filter.
	/// </summary>
	protected virtual TriggerEventFilter? GetFilter() => null;

	private readonly struct EventConstraint(
		string it,
		string eventName,
		Action<T> callback,
		TriggerEventFilter? filter,
		Quantifier quantifier)
		: IContextConstraint<T>
	{
		public ConstraintResult IsMetBy(T actual, IEvaluationContext context)
		{
			EventInfo[] events = typeof(T).GetEvents();
			string name = eventName;
			using EventRecorder recorder = new(name);

			EventInfo? @event = events.FirstOrDefault(x => x.Name == name);
			if (@event == null)
			{
				throw new NotSupportedException($"Event {name} is not supported on {Formatter.Format(actual)}");
			}

			recorder.Attach(new WeakReference(actual), @event);
			callback(actual);
			int eventCount = recorder.EventQueue.Count;
			if (filter != null)
			{
				TriggerEventFilter f = filter;
				eventCount = recorder.EventQueue.Count(x => f.IsMatch(name, x.Parameters));
			}

			if (quantifier.Check(eventCount, true) == true)
			{
				return new ConstraintResult.Success<T>(actual, ToString());
			}

			return new ConstraintResult.Failure<T>(actual, ToString(),
				eventCount switch
				{
					0 => $"{it} was never recorded in {Formatter.Format(recorder.EventQueue, FormattingOptions.MultipleLines)}",
					1 => $"{it} was only recorded once in {Formatter.Format(recorder.EventQueue, FormattingOptions.MultipleLines)}",
					_ => $"{it} was only recorded {eventCount} times in {Formatter.Format(recorder.EventQueue, FormattingOptions.MultipleLines)}"
				});
		}

		public override string ToString() => $"trigger event {eventName}{filter?.ToString() ?? ""} {quantifier}";
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
				throw new NotSupportedException($"The event {name} contains too many parameters ({handlerType.GetParameters().Length}): {Formatter.Format(handlerType.GetParameters().Select(x => x.ParameterType))}");
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
		{
			EventQueue.Enqueue(new OccurredEvent(name));
		}

		public void RecordEvent<T1>(T1 parameter1)
		{
			EventQueue.Enqueue(new OccurredEvent(name, parameter1));
		}

		public void RecordEvent<T1, T2>(T1 parameter1, T2 parameter2)
		{
			EventQueue.Enqueue(new OccurredEvent(name, parameter1, parameter2));
		}

		public void RecordEvent<T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3)
		{
			EventQueue.Enqueue(new OccurredEvent(name, parameter1, parameter2, parameter3));
		}

		public void RecordEvent<T1, T2, T3, T4>(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4)
		{
			EventQueue.Enqueue(new OccurredEvent(name, parameter1, parameter2, parameter3, parameter4));
		}
	}

	private readonly struct OccurredEvent(string name, params object?[] parameters)
	{
		public string Name { get; } = name;
		public object?[] Parameters { get; } = parameters;

		/// <inheritdoc />
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append(Name).Append('(');
			if (Parameters.Length > 0)
			{
				foreach (var parameter in Parameters)
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
