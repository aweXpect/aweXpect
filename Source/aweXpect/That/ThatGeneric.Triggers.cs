using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatGeneric
{
	/// <summary>
	///     Verifies that the subject triggers an event.
	/// </summary>
	public static TriggerParameterResult<T> Triggers<T>(
		this IExpectSubject<T> subject, string eventName)
	{
		IThat<T> should = subject.Should(_ => { });
		return new TriggerParameterResult<T>(should, eventName);
	}

	/// <summary>
	///     Verifies that the subject triggers a <see cref="INotifyPropertyChanged.PropertyChanged"/> event.
	/// </summary>
	public static TriggerPropertyChangedParameterResult<T> TriggersPropertyChanged<T>(this IExpectSubject<T> subject)
		where T : INotifyPropertyChanged
	{
		IThat<T> should = subject.Should(_ => { });
		return new TriggerPropertyChangedParameterResult<T>(should, nameof(INotifyPropertyChanged.PropertyChanged));
	}
}

public class TriggerPropertyChangedParameterResult<T>(IThat<T> that, string eventName) : TriggerResult<T>(that, eventName)
{
	public TriggerResult<T> WithPropertyChangedEventArgs(Func<PropertyChangedEventArgs, bool> predicate,
		[CallerArgumentExpression("predicate")] string doNotPopulateThisValue = "")
	{
		Filter ??= new EventFilter();
		Filter.AddPredicate<PropertyChangedEventArgs>(o => o.Any(x => x is PropertyChangedEventArgs m && predicate(m)), doNotPopulateThisValue);
		return this;
	}
}
public class TriggerParameterResult<T>(IThat<T> that, string eventName) : TriggerResult<T>(that, eventName)
{
	public TriggerResult<T> WithParameter<TProperty>(Func<TProperty, bool> predicate,
		[CallerArgumentExpression("predicate")] string doNotPopulateThisValue = "")
	{
		Filter ??= new EventFilter();
		Filter.AddPredicate<TProperty>(o => o.Any(x => x is TProperty m && predicate(m)), doNotPopulateThisValue);
		return this;
	}

	public TriggerResult<T> WithParameter<TProperty>(int argumentPosition, Func<TProperty, bool> predicate,
		[CallerArgumentExpression("predicate")] string doNotPopulateThisValue = "")
	{
		Filter ??= new EventFilter();
		Filter.AddPredicate<TProperty>(
			o => o.Length < argumentPosition && o[argumentPosition] is TProperty m && predicate(m),
			doNotPopulateThisValue);
		return this;
	}
}


public class EventFilter
{
	private readonly List<Func<object?[], bool>> _predicates = new();
	private readonly StringBuilder _toString = new();

	public void AddPredicate<TProperty>(Func<object?[], bool> predicate, string predicateExpression)
	{
		if (_predicates.Count != 0)
		{
			_toString.Append(" and");
		}

		_toString.Append(" with ");
		Formatter.Format(_toString, typeof(TProperty));
		_toString.Append(" parameter ");
		_toString.Append(predicateExpression);
		_predicates.Add(predicate);
	}

	public bool IsMatch(object?[] arguments)
		=> _predicates.All(predicate => predicate(arguments));

	public override string ToString() => _toString.ToString();
}
public class TriggerResult<T>(IThat<T> that, string eventName)
{
	public EventFilter? Filter { get; protected set; }

	public CountResult<T, IThat<T>> While(Action<T> callback)
	{
		Quantifier quantifier = new();
		that.ExpectationBuilder.AddConstraint(it => new EventConstraint(it, eventName, callback, Filter, quantifier));
		return new CountResult<T, IThat<T>>(that.ExpectationBuilder, that, quantifier);
	}

	private readonly struct EventConstraint(
		string it,
		string eventName,
		Action<T> callback,
		EventFilter? filter,
		Quantifier quantifier)
		: IContextConstraint<T>
	{
		public ConstraintResult IsMetBy(T actual, IEvaluationContext context)
		{
			using EventRecorder recorder = new();

			EventInfo[] events = typeof(T).GetEvents();
			string name = eventName;
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
				EventFilter f = filter;
				eventCount = recorder.EventQueue.Count(x => f.IsMatch(x.Parameters));
			}

			if (quantifier.Check(eventCount, true) == true)
			{
				return new ConstraintResult.Success<T>(actual, ToString());
			}

			return new ConstraintResult.Failure<T>(actual, ToString(),
				$"{it} was only recorded {(eventCount == 1 ? "once" : $"{eventCount} times")}");
		}

		public override string ToString() => $"trigger event {eventName}{filter?.ToString() ?? ""} {quantifier}";
	}

	private sealed class EventRecorder : IDisposable
	{
		private int _sequence;
		private Action? onDispose;
		public ConcurrentQueue<OccurredEvent> EventQueue { get; } = new();

		public void Dispose() => onDispose?.Invoke();

		public void Attach(WeakReference subject, EventInfo eventInfo)
		{
			MethodInfo handlerType = eventInfo.EventHandlerType?.GetMethod("Invoke") ??
			                         throw new MissingMethodException("Invoke");
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

			eventInfo.AddEventHandler(subject.Target, handler);

			onDispose = () =>
			{
				if (subject.Target is not null)
				{
					eventInfo.RemoveEventHandler(subject.Target, handler);
				}
			};
		}

		public void RecordEvent()
		{
			int sequence = Interlocked.Increment(ref _sequence);
			EventQueue.Enqueue(new OccurredEvent(sequence));
		}

		public void RecordEvent<T1>(T1 parameter1)
		{
			int sequence = Interlocked.Increment(ref _sequence);
			EventQueue.Enqueue(new OccurredEvent(sequence, parameter1));
		}

		public void RecordEvent<T1, T2>(T1 parameter1, T2 parameter2)
		{
			int sequence = Interlocked.Increment(ref _sequence);
			EventQueue.Enqueue(new OccurredEvent(sequence, parameter1, parameter2));
		}

		public void RecordEvent<T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3)
		{
			int sequence = Interlocked.Increment(ref _sequence);
			EventQueue.Enqueue(new OccurredEvent(sequence, parameter1, parameter2, parameter3));
		}

		public void RecordEvent<T1, T2, T3, T4>(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4)
		{
			int sequence = Interlocked.Increment(ref _sequence);
			EventQueue.Enqueue(new OccurredEvent(sequence, parameter1, parameter2, parameter3, parameter4));
		}
	}

	private class OccurredEvent(int sequence, params object?[] parameters)
	{
		public object?[] Parameters { get; } = parameters;

		public int Sequence { get; } = sequence;
	}
}
