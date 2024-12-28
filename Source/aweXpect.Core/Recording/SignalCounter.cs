using System;
using System.Collections.Generic;
using System.Threading;

namespace aweXpect.Recording;

/// <summary>
///     Creates a new signal counter without parameters.
/// </summary>
public class SignalCounter : ISignalCounter
{
	private readonly object _lock = new();
	private CountdownEvent? _countdownEvent;
	private int _counter;
	private ManualResetEventSlim? _resetEvent;

	/// <inheritdoc cref="ISignalCounter.IsSignaled(Times?)" />
	public bool IsSignaled(Times? amount = null)
	{
		int value = amount?.Value ?? 1;
		return _counter >= value;
	}

	/// <inheritdoc cref="ISignalCounter.Signal()" />
	public void Signal()
	{
		lock (_lock)
		{
			Interlocked.Increment(ref _counter);
			_resetEvent?.Set();
			_countdownEvent?.Signal();
		}
	}

	/// <inheritdoc cref="ISignalCounter.Wait(TimeSpan?, CancellationToken)" />
	public ISignalCounterResult Wait(
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
					return new SignalCounterResult(true, _counter);
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

			return new SignalCounterResult(false, _counter);
		}

		return new SignalCounterResult(_counter > 0, _counter);
	}

	/// <inheritdoc
	///     cref="ISignalCounter.Wait(aweXpect.Times,System.Nullable{System.TimeSpan},System.Threading.CancellationToken)" />
	public ISignalCounterResult Wait(Times amount, TimeSpan? timeout = null,
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
				return new SignalCounterResult(true, _counter);
			}

			_countdownEvent = new CountdownEvent(amount.Value - _counter);
		}

		timeout ??= TimeSpan.FromSeconds(30);
		try
		{
			if (_countdownEvent.Wait(timeout.Value, cancellationToken))
			{
				return new SignalCounterResult(true, _counter);
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

		return new SignalCounterResult(false, _counter);
	}
}

/// <summary>
///     Creates a new signal counter with parameters of type <typeparamref name="TParameter" />.
/// </summary>
public class SignalCounter<TParameter> : ISignalCounter<TParameter>
{
	private readonly object _lock = new();
	private readonly List<TParameter> _parameters = new();
	private CountdownEvent? _countdownEvent;
	private int _counter;
	private ManualResetEventSlim? _resetEvent;

	/// <inheritdoc cref="ISignalCounter{TParameter}.IsSignaled(Times?)" />
	public bool IsSignaled(Times? amount = null)
	{
		int value = amount?.Value ?? 1;
		return _counter >= value;
	}

	/// <inheritdoc cref="ISignalCounter{TParameter}.Signal(TParameter)" />
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

	/// <inheritdoc cref="ISignalCounter{TParameter}.Wait(TimeSpan?, CancellationToken)" />
	public ISignalCounterResult<TParameter> Wait(
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
					return new SignalCounterResult<TParameter>(true, _parameters.ToArray());
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

			return new SignalCounterResult<TParameter>(false, _parameters.ToArray());
		}

		return new SignalCounterResult<TParameter>(_counter > 0, _parameters.ToArray());
	}

	/// <inheritdoc cref="ISignalCounter{TParameter}.Wait(Times, TimeSpan?, CancellationToken)" />
	public ISignalCounterResult<TParameter> Wait(Times amount, TimeSpan? timeout = null,
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
				return new SignalCounterResult<TParameter>(true, _parameters.ToArray());
			}

			_countdownEvent = new CountdownEvent(amount.Value - _counter);
		}

		timeout ??= TimeSpan.FromSeconds(30);
		try
		{
			if (_countdownEvent.Wait(timeout.Value, cancellationToken))
			{
				return new SignalCounterResult<TParameter>(true, _parameters.ToArray());
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

		return new SignalCounterResult<TParameter>(false, _parameters.ToArray());
	}
}
