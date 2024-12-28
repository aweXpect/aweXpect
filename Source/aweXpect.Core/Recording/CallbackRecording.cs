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

	/// <inheritdoc cref="ICallbackRecording.IsSignaled(Times?)" />
	public bool IsSignaled(Times? amount = null)
	{
		int value = amount?.Value ?? 1;
		return _counter >= value;
	}

	/// <inheritdoc cref="ICallbackRecording.Signal()" />
	public void Signal()
	{
		lock (_lock)
		{
			Interlocked.Increment(ref _counter);
			_resetEvent?.Set();
			_countdownEvent?.Signal();
		}
	}

	/// <inheritdoc cref="ICallbackRecording.Wait(TimeSpan?, CancellationToken)" />
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

	/// <inheritdoc
	///     cref="ICallbackRecording.Wait(aweXpect.Times,System.Nullable{System.TimeSpan},System.Threading.CancellationToken)" />
	public ICallbackRecordingResult Wait(Times amount, TimeSpan? timeout = null,
		CancellationToken cancellationToken = default)
	{
		if (amount.Value <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(amount), "The amount must be greater than zero.");
		}

		lock (_lock)
		{
			if (_counter >= amount.Value)
			{
				return new CallbackRecordingResult(true, _counter);
			}

			_countdownEvent = new CountdownEvent(amount.Value - _counter);
		}

		timeout ??= TimeSpan.FromSeconds(30);
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
}

internal class CallbackRecording<TParameter> : ICallbackRecording<TParameter>
{
	private readonly object _lock = new();
	private readonly List<TParameter> _parameters = new();
	private CountdownEvent? _countdownEvent;
	private int _counter;
	private ManualResetEventSlim? _resetEvent;

	public bool IsSignaled(Times? amount = null)
	{
		int value = amount?.Value ?? 1;
		return _counter >= value;
	}

	public void Signal(TParameter parameter)
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

	public ICallbackRecordingResult<TParameter> Wait(Times amount, TimeSpan? timeout = null,
		CancellationToken cancellationToken = default)

	{
		if (amount.Value <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(amount), "The amount must be greater than zero.");
		}

		lock (_lock)
		{
			if (_counter >= amount.Value)
			{
				return new CallbackRecordingResult<TParameter>(true, _parameters.ToArray());
			}

			_countdownEvent = new CountdownEvent(amount.Value - _counter);
		}

		timeout ??= TimeSpan.FromSeconds(30);
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
}
