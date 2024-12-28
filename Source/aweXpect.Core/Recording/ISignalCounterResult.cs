using System.Threading;

namespace aweXpect.Recording;

/// <summary>
///     The result of a <see cref="ISignalCounter" />.
/// </summary>
public interface ISignalCounterResult
{
	/// <summary>
	///     The number of times the callback was triggered.
	/// </summary>
	public int Count { get; }

	/// <summary>
	///     Flag, indicating if the waiting was successful or not.
	/// </summary>
	/// <remarks>
	///     This flag will be <see langword="false" /> if the timeout expired
	///     or the <see cref="CancellationToken" /> was cancelled prior to enough triggered callbacks;
	///     otherwise <see langword="true" />.
	/// </remarks>
	public bool IsSuccess { get; }
}

/// <summary>
///     The result of a <see cref="ISignalCounter{TParameter}" />.
/// </summary>
public interface ISignalCounterResult<out TParameter> : ISignalCounterResult
{
	/// <summary>
	///     The parameters provided while triggering the callback.
	/// </summary>
	public TParameter[] Parameters { get; }
}
