using System.Threading;

namespace aweXpect.Recording;

/// <summary>
///     The result when waiting in a <see cref="SignalCounter" />.
/// </summary>
public class SignalCounterResult(bool wasTriggered, int counter)
{
	/// <summary>
	///     The number of times the callback was triggered.
	/// </summary>
	public int Count { get; } = counter;

	/// <summary>
	///     Flag, indicating if the waiting was successful or not.
	/// </summary>
	/// <remarks>
	///     This flag will be <see langword="false" /> if the timeout expired
	///     or the <see cref="CancellationToken" /> was cancelled prior to enough triggered callbacks;
	///     otherwise <see langword="true" />.
	/// </remarks>
	public bool IsSuccess { get; } = wasTriggered;
}

/// <summary>
///     The result when waiting in a <see cref="SignalCounter{TParameter}" />.
/// </summary>
public class SignalCounterResult<TParameter>(bool wasTriggered, TParameter[] parameters)
	: SignalCounterResult(wasTriggered, parameters.Length)
{
	/// <summary>
	///     The parameters provided while triggering the callback.
	/// </summary>
	public TParameter[] Parameters { get; } = parameters;
}
