namespace aweXpect.Recording;

internal class SignalCounterResult(bool wasTriggered, int counter)
	: ISignalCounterResult
{
	/// <inheritdoc cref="ISignalCounterResult.Count" />
	public int Count { get; } = counter;

	/// <inheritdoc cref="ISignalCounterResult.IsSuccess" />
	public bool IsSuccess { get; } = wasTriggered;
}

internal class SignalCounterResult<TParameter>(bool wasTriggered, TParameter[] parameters)
	: ISignalCounterResult<TParameter>
{
	/// <inheritdoc cref="ISignalCounterResult.Count" />
	public int Count => Parameters.Length;

	/// <inheritdoc cref="ISignalCounterResult.IsSuccess" />
	public bool IsSuccess { get; } = wasTriggered;

	/// <inheritdoc cref="ISignalCounterResult{TParameter}.Parameters" />
	public TParameter[] Parameters { get; } = parameters;
}
