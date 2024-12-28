namespace aweXpect.Recording;

internal class CallbackRecordingResult(bool wasTriggered, int counter)
	: ICallbackRecordingResult
{
	/// <inheritdoc cref="ICallbackRecordingResult.Count" />
	public int Count { get; } = counter;

	/// <inheritdoc cref="ICallbackRecordingResult.IsSuccess" />
	public bool IsSuccess { get; } = wasTriggered;
}

internal class CallbackRecordingResult<TParameter>(bool wasTriggered, TParameter[] parameters)
	: ICallbackRecordingResult<TParameter>
{
	/// <inheritdoc cref="ICallbackRecordingResult.Count" />
	public int Count => Parameters.Length;

	/// <inheritdoc cref="ICallbackRecordingResult.IsSuccess" />
	public bool IsSuccess { get; } = wasTriggered;

	/// <inheritdoc cref="ICallbackRecordingResult{TParameter}.Parameters" />
	public TParameter[] Parameters { get; } = parameters;
}
