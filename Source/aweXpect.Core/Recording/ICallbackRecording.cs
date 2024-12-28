using System;
using System.Threading;

namespace aweXpect.Recording;

/// <summary>
///     Record executions of a callback.
/// </summary>
public interface ICallbackRecording
{
	/// <summary>
	///     Checks if the callback was signaled at least <paramref name="amount" /> times.
	/// </summary>
	/// <remarks>
	///     If no <paramref name="amount" /> is specified, checks if it was signaled at least once.
	/// </remarks>
	bool IsSignaled(Times? amount = null);

	/// <summary>
	///     Signals that the callback was executed.
	/// </summary>
	void Signal();

	/// <summary>
	///     Blocks the current thread until the callback was executed at least once
	///     or the <paramref name="timeout" /> expired
	///     or the <paramref name="cancellationToken" /> was cancelled.
	/// </summary>
	/// <remarks>
	///     If no <paramref name="timeout" /> is specified (set to <see langword="null" />),
	///     a default timeout of 30 seconds is used.
	/// </remarks>
	ICallbackRecordingResult Wait(
		TimeSpan? timeout = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	///     Blocks the current thread until the callback was executed at least the required <paramref name="amount" /> of times
	///     or the <paramref name="timeout" /> expired
	///     or the <paramref name="cancellationToken" /> was cancelled.
	/// </summary>
	/// <remarks>
	///     If no <paramref name="timeout" /> is specified (set to <see langword="null" />),
	///     a default timeout of 30 seconds is used.
	/// </remarks>
	ICallbackRecordingResult Wait(
		Times amount,
		TimeSpan? timeout = null,
		CancellationToken cancellationToken = default);
}

/// <summary>
///     Record executions of a callback with a <typeparamref name="TParameter" />.
/// </summary>
public interface ICallbackRecording<TParameter>
{
	/// <summary>
	///     Checks if the callback was signaled at least <paramref name="amount" /> times.
	/// </summary>
	/// <remarks>
	///     If no <paramref name="amount" /> is specified, checks if it was signaled at least once.
	/// </remarks>
	bool IsSignaled(Times? amount = null);

	/// <summary>
	///     Signals that the callback was executed with the provided <paramref name="parameter" />.
	/// </summary>
	void Signal(TParameter parameter);

	/// <summary>
	///     Blocks the current thread until the callback was executed at least once
	///     or the <paramref name="timeout" /> expired
	///     or the <paramref name="cancellationToken" /> was cancelled.
	/// </summary>
	/// <remarks>
	///     If no <paramref name="timeout" /> is specified (set to <see langword="null" />),
	///     a default timeout of 30 seconds is used.
	/// </remarks>
	ICallbackRecordingResult<TParameter> Wait(
		TimeSpan? timeout = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	///     Blocks the current thread until the callback was executed at least the required <paramref name="amount" /> of times
	///     or the <paramref name="timeout" /> expired
	///     or the <paramref name="cancellationToken" /> was cancelled.
	/// </summary>
	/// <remarks>
	///     If no <paramref name="timeout" /> is specified (set to <see langword="null" />),
	///     a default timeout of 30 seconds is used.
	/// </remarks>
	ICallbackRecordingResult<TParameter> Wait(
		Times amount,
		TimeSpan? timeout = null,
		CancellationToken cancellationToken = default);
}
