using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.Helpers;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result for a <see cref="TriggersExtensions.Triggers{T}" /> expectation.
/// </summary>
public abstract class TriggerResult<T, TSelf>(
	ExpectationBuilder expectationBuilder,
	IExpectSubject<T> returnValue,
	string eventName,
	Quantifier quantifier)
	where TSelf : TriggerResult<T, TSelf>
{
	private readonly List<EventConstraint> _eventConstraints = new();
	private string _eventName = eventName;
	private Quantifier _quantifier = quantifier;

	/// <summary>
	///     Add additional trigger expectations.
	/// </summary>
	public TriggerAndResult<TSelf> And => new(s =>
	{
		_eventConstraints.Add(new EventConstraint(_eventConstraints.Count + 1, _eventName, GetFilter(), _quantifier));
		_eventName = s;
		_quantifier = new Quantifier();
		ResetFilter();

		return (TSelf)this;
	});

	/// <summary>
	///     Verifies, that it occurs at least <paramref name="minimum" /> times.
	/// </summary>
	public TSelf AtLeast(Times minimum)
	{
		_quantifier.AtLeast(minimum.Value);
		return (TSelf)this;
	}

	/// <summary>
	///     Verifies, that it occurs at most <paramref name="maximum" /> times.
	/// </summary>
	public TSelf AtMost(Times maximum)
	{
		_quantifier.AtMost(maximum.Value);
		return (TSelf)this;
	}

	/// <summary>
	///     Verifies, that it occurs between <paramref name="minimum" />...
	/// </summary>
	public BetweenResult<TSelf> Between(int minimum)
		=> new(maximum =>
		{
			_quantifier.Between(minimum, maximum);
			return (TSelf)this;
		});

	/// <summary>
	///     Verifies, that it occurs exactly <paramref name="expected" /> times.
	/// </summary>
	public TSelf Exactly(Times expected)
	{
		_quantifier.Exactly(expected.Value);
		return (TSelf)this;
	}

	/// <summary>
	///     Verifies, that it occurs never.
	/// </summary>
	public TSelf Never()
	{
		_quantifier.Exactly(0);
		return (TSelf)this;
	}

	/// <summary>
	///     Verifies, that it occurs exactly once.
	/// </summary>
	public TSelf Once()
	{
		_quantifier.Exactly(1);
		return (TSelf)this;
	}


	/// <summary>
	///     Executes the <paramref name="callback" /> while monitoring the triggered events.
	/// </summary>
	public AndOrResult<T, IExpectSubject<T>> While(Action<T> callback)
		=> While((t, _) =>
		{
			callback(t);
			return Task.CompletedTask;
		});

	/// <summary>
	///     Executes the asynchronous <paramref name="callback" /> while monitoring the triggered events.
	/// </summary>
	public AndOrResult<T, IExpectSubject<T>> While(Func<T, Task> callback)
		=> While((t, _) => callback(t));

	/// <summary>
	///     Executes the asynchronous <paramref name="callback" /> with cancellation support
	///     while monitoring the triggered events.
	/// </summary>
	public AndOrResult<T, IExpectSubject<T>> While(Func<T, CancellationToken, Task> callback)
	{
		_eventConstraints.Add(new EventConstraint(_eventConstraints.Count + 1, _eventName, GetFilter(), _quantifier));
		expectationBuilder.AddConstraint(it
			=> new EventTriggerConstraint(it,
				_eventConstraints,
				callback));
		return new AndOrResult<T, IExpectSubject<T>>(expectationBuilder, returnValue);
	}

	/// <summary>
	///     Gets the event filter.
	/// </summary>
	protected virtual TriggerEventFilter? GetFilter() => null;

	/// <summary>
	///     Resets the event filter.
	/// </summary>
	protected abstract void ResetFilter();

	/// <summary>
	///     Result for combining multiple trigger filters.
	/// </summary>
	public class TriggerAndResult<TResult>(Func<string, TResult> callback)
	{
		/// <summary>
		///     Verifies that the subject triggers an additional event with the given <paramref name="eventName" />.
		/// </summary>
		public TResult Triggers(string eventName) => callback(eventName);
	}

	private class EventConstraint(int index, string name, TriggerEventFilter? filter, Quantifier quantifier)
	{
		public int Index { get; } = index;
		public string Name { get; } = name;
		public TriggerEventFilter? Filter { get; } = filter;
		public Quantifier Quantifier { get; } = quantifier;
	}

	private readonly struct EventTriggerConstraint(
		string it,
		List<EventConstraint> eventConstraints,
		Func<T, CancellationToken, Task> callback)
		: IAsyncConstraint<T>
	{
		public async Task<ConstraintResult> IsMetBy(T actual, CancellationToken cancellationToken)
		{
			EventInfo[] events = typeof(T).GetEvents();
			EventRecorder[] recorders = new EventRecorder[eventConstraints.Count];
			for (int i = 0; i < eventConstraints.Count; i++)
			{
				string? name = eventConstraints[i].Name;
				recorders[i] = new EventRecorder(name);
				EventInfo? @event = events.FirstOrDefault(x => x.Name == name);
				if (@event == null)
				{
					throw new NotSupportedException($"Event {name} is not supported on {Formatter.Format(actual)}");
				}

				recorders[i].Attach(new WeakReference(actual), @event);
			}

			StringBuilder sb = new();
			bool hasErrors = false;
			try
			{
				await callback(actual, cancellationToken);

				sb.Append(it).Append(" was");
				bool hasMultipleConstraints = eventConstraints.Count > 1;
				if (hasMultipleConstraints)
				{
					sb.AppendLine();
				}

				foreach (IGrouping<string, EventConstraint> group in eventConstraints.GroupBy(x => x.Name))
				{
					string? name = group.Key;
					ConcurrentQueue<OccurredEvent>? eventQueue = null;
					bool hasGroupError = false;
					foreach (EventConstraint? item in group)
					{
						eventQueue = recorders[item.Index - 1].EventQueue;
						int eventCount = eventQueue.Count;
						TriggerEventFilter? filter = item.Filter;
						if (filter != null)
						{
							eventCount = eventQueue.Count(x => filter.IsMatch(name, x.Parameters));
						}

						if (item.Quantifier.Check(eventCount, true) != true)
						{
							hasErrors = true;
							hasGroupError = true;
							if (group.Count() > 1)
							{
								sb.Append("  [").Append(item.Index).Append(']');
								sb.Append(eventCount switch
								{
									0 => " never recorded",
									1 => " only recorded once",
									_ => $" only recorded {eventCount} times"
								});
								sb.AppendLine(" and");
							}
							else
							{
								if (hasMultipleConstraints)
								{
									sb.Append("  [").Append(item.Index).Append(']');
								}

								sb.Append(eventCount switch
								{
									0 => " never recorded",
									1 => " only recorded once",
									_ => $" only recorded {eventCount} times"
								});
							}
						}
					}

					if (group.Count() > 1)
					{
						sb.Length -= 4;
						sb.Length -= Environment.NewLine.Length;
					}

					if (hasGroupError)
					{
						sb.Append(" in ");
						sb.Append(Formatter.Format(eventQueue, FormattingOptions.MultipleLines)
							.Indent(hasMultipleConstraints ? "      " : "", false));
						sb.AppendLine(" and");
					}
				}
			}
			finally
			{
				for (int i = 0; i < eventConstraints.Count; i++)
				{
					recorders[i].Dispose();
				}
			}

			if (!hasErrors)
			{
				return new ConstraintResult.Success<T>(actual, ToString());
			}

			sb.Length -= 4;
			sb.Length -= Environment.NewLine.Length;
			return new ConstraintResult.Failure<T>(actual, ToString(), sb.ToString());
		}

		public override string ToString()
		{
			StringBuilder? sb = new();
			bool hasMultipleConstraints = eventConstraints.Count > 1;
			List<IGrouping<string, EventConstraint>>? groups = eventConstraints.GroupBy(x => x.Name).ToList();
			bool hasMultipleGroups = groups.Count > 1;
			foreach (IGrouping<string, EventConstraint> group in groups)
			{
				if (hasMultipleGroups)
				{
					sb.Append("  ");
				}

				bool hasMultipleGroupConstraints = group.Count() > 1;
				if (hasMultipleGroupConstraints)
				{
					sb.Append("trigger event ").Append(group.Key);
					foreach (EventConstraint? item in group)
					{
						sb.AppendLine();
						if (hasMultipleGroups)
						{
							sb.Append("  ");
						}

						sb.Append("  [").Append(item.Index).Append("]");
						if (item.Filter != null)
						{
							sb.Append(item.Filter);
						}

						sb.Append(' ').Append(item.Quantifier);
						sb.Append(" and");
					}

					sb.Length -= 4;
				}
				else
				{
					EventConstraint? item = group.First();
					if (hasMultipleConstraints)
					{
						sb.Append("[" + item.Index + "] ");
					}

					sb.Append("trigger event ").Append(group.Key);
					if (item.Filter != null)
					{
						sb.Append(item.Filter);
					}

					sb.Append(' ').Append(item.Quantifier);
				}

				sb.Append(" and");
				sb.AppendLine();
			}

			sb.Length -= 4;
			sb.Length -= Environment.NewLine.Length;
			return sb.ToString();
		}
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
