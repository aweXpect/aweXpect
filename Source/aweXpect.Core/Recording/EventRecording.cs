using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
#if NET8_0_OR_GREATER
using System.Threading.Channels;
#endif

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

#if NET8_0_OR_GREATER
	public async Task<IEventRecordingResult> StopWhen(Func<IEventRecordingResult, bool> areFound, TimeSpan timeout)
	{
		if (timeout > TimeSpan.Zero && !areFound(this))
		{
			Channel<bool> channel = Channel.CreateUnbounded<bool>();
			using CancellationTokenSource cts = new(timeout);
			CancellationToken token = cts.Token;
			foreach (EventRecorder recorder in _recorders.Values)
			{
				recorder.Register(channel.Writer);
			}

			try
			{
#pragma warning disable S3267 // https://rules.sonarsource.com/csharp/RSPEC-3267
				await foreach (bool _ in channel.Reader.ReadAllAsync(token))
				{
					if (areFound(this))
					{
						break;
					}
				}
#pragma warning restore S3267
			}
			catch (OperationCanceledException)
			{
				// Ignore cancellation
			}
		}

		foreach (EventRecorder recorder in _recorders.Values)
		{
			recorder.Dispose();
		}

		return this;
	}
#else
	public Task<IEventRecordingResult> StopWhen(Func<IEventRecordingResult, bool> areFound, TimeSpan timeout)
	{
		DateTime now = DateTime.Now;
		DateTime endTime = now.Add(timeout);
		if (timeout > TimeSpan.Zero && !areFound(this))
		{
			using (ManualResetEventSlim ms = new())
			{
				foreach (EventRecorder recorder in _recorders.Values)
				{
					recorder.Register(ms);
				}

				while (true)
				{
					now = DateTime.Now;
					if (now >= endTime)
					{
						break;
					}

					ms.Reset();
					ms.Wait(endTime - now);
					if (areFound(this))
					{
						break;
					}
				}
			}
		}

		foreach (EventRecorder recorder in _recorders.Values)
		{
			recorder.Dispose();
		}

		return Task.FromResult<IEventRecordingResult>(this);
	}
#endif

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
