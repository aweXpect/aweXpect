using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.Events;
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
	private readonly EventConstraints _eventConstraints = new();
	private string _eventName = eventName;
	private Quantifier _quantifier = quantifier;

	/// <summary>
	///     Add additional trigger expectations.
	/// </summary>
	public TriggerAndResult<TSelf> And => new(s =>
	{
		_eventConstraints.Add(_eventName, GetFilter(), _quantifier);
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
		_eventConstraints.Add(_eventName, GetFilter(), _quantifier);
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

	private readonly struct EventTriggerConstraint(
		string it,
		EventConstraints eventConstraints,
		Func<T, CancellationToken, Task> callback)
		: IAsyncConstraint<T>
	{
		public async Task<ConstraintResult> IsMetBy(T actual, CancellationToken cancellationToken)
		{
			using EventRecording<T> recording = eventConstraints.StartRecordingEvents(actual);

			await callback(actual, cancellationToken);

			StringBuilder sb = new();
			sb.Append(it).Append(" was");
			bool hasErrors = eventConstraints.HasErrors(recording, sb);
			if (!hasErrors)
			{
				return new ConstraintResult.Success<T>(actual, ToString());
			}

			sb.Length -= 4;
			sb.Length -= Environment.NewLine.Length;
			return new ConstraintResult.Failure<T>(actual, ToString(), sb.ToString());
		}

		public override string ToString()
			=> eventConstraints.ToString();
	}
}
