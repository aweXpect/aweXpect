using System;
using System.Collections.Generic;
using System.Threading;

namespace aweXpect.Recording;

internal class CallbackRecording : ICallbackRecording
{
	private readonly object _lock = new();
	private CountdownEvent? _countdownEvent;
	private int _counter;
	private ManualResetEventSlim? _resetEvent;

	public void Trigger()
	{
		lock (_lock)
		{
			Interlocked.Increment(ref _counter);
			_resetEvent?.Set();
			_countdownEvent?.Signal();
		}
	}

	public ICallbackRecordingResult Wait(
		TimeSpan? timeout = null,
		CancellationToken cancellationToken = default)
	{
		lock (_lock)
		{
			if (_counter == 0)
			{
				_resetEvent = new ManualResetEventSlim();
			}
		}

		timeout ??= TimeSpan.FromSeconds(30);
		if (_resetEvent != null)
		{
			try
			{
				if (_resetEvent.Wait(timeout.Value, cancellationToken))
				{
					return new CallbackRecordingResult(true, _counter);
				}
			}
			catch (OperationCanceledException)
			{
				// Ignore a cancelled operation
			}
			finally
			{
				_resetEvent.Dispose();
			}

			return new CallbackRecordingResult(false, _counter);
		}

		return new CallbackRecordingResult(_counter > 0, _counter);
	}

	public ICallbackRecordingResult WaitMultiple(int amount, TimeSpan? timeout = null,
		CancellationToken cancellationToken = default)
	{
		if (amount <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(amount), "The amount must be greater than zero.");
		}

		lock (_lock)
		{
			if (_counter >= amount)
			{
				return new CallbackRecordingResult(true, _counter);
			}

			_countdownEvent = new CountdownEvent(amount - _counter);
		}

		timeout ??= TimeSpan.FromSeconds(30);
		if (_countdownEvent != null)
		{
			try
			{
				if (_countdownEvent.Wait(timeout.Value, cancellationToken))
				{
					return new CallbackRecordingResult(true, _counter);
				}
			}
			catch (OperationCanceledException)
			{
				// Ignore a cancelled operation
			}
			finally
			{
				_countdownEvent.Dispose();
			}

			return new CallbackRecordingResult(false, _counter);
		}

		return new CallbackRecordingResult(_counter >= amount, _counter);
	}
}

internal class CallbackRecording<TParameter> : ICallbackRecording<TParameter>
{
	private readonly object _lock = new();
	private readonly List<TParameter> _parameters = new();
	private int _counter;
	private ManualResetEventSlim? _resetEvent;
	private CountdownEvent? _countdownEvent;

	public void Trigger(TParameter parameter)
	{
		lock (_lock)
		{
			Interlocked.Increment(ref _counter);
			_parameters.Add(parameter);
			_resetEvent?.Set();
			_countdownEvent?.Signal();
		}
	}

	public ICallbackRecordingResult<TParameter> Wait(
		TimeSpan? timeout = null,
		CancellationToken cancellationToken = default)
	{
		lock (_lock)
		{
			if (_counter == 0)
			{
				_resetEvent = new ManualResetEventSlim();
			}
		}

		timeout ??= TimeSpan.FromSeconds(30);
		if (_resetEvent != null)
		{
			try
			{
				if (_resetEvent.Wait(timeout.Value, cancellationToken))
				{
					return new CallbackRecordingResult<TParameter>(true, _parameters.ToArray());
				}
			}
			catch (OperationCanceledException)
			{
				// Ignore a cancelled operation
			}
			finally
			{
				_resetEvent.Dispose();
			}

			return new CallbackRecordingResult<TParameter>(false, _parameters.ToArray());
		}

		return new CallbackRecordingResult<TParameter>(_counter > 0, _parameters.ToArray());
	}

	public ICallbackRecordingResult<TParameter> WaitMultiple(int amount, TimeSpan? timeout = null,
		CancellationToken cancellationToken = default)
	
	{
		if (amount <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(amount), "The amount must be greater than zero.");
		}

		lock (_lock)
		{
			if (_counter >= amount)
			{
				return new CallbackRecordingResult<TParameter>(true, _parameters.ToArray());
			}

			_countdownEvent = new CountdownEvent(amount - _counter);
		}

		timeout ??= TimeSpan.FromSeconds(30);
		if (_countdownEvent != null)
		{
			try
			{
				if (_countdownEvent.Wait(timeout.Value, cancellationToken))
				{
					return new CallbackRecordingResult<TParameter>(true, _parameters.ToArray());
				}
			}
			catch (OperationCanceledException)
			{
				// Ignore a cancelled operation
			}
			finally
			{
				_countdownEvent.Dispose();
			}

			return new CallbackRecordingResult<TParameter>(false, _parameters.ToArray());
		}

		return new CallbackRecordingResult<TParameter>(_counter >= amount, _parameters.ToArray());
	}
}
