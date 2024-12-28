namespace aweXpect.Recording;

public static class Record
{
	public static ICallbackRecording Callback()
	{
		return new CallbackRecording();
	}
	public static ICallbackRecording<TParameter> Callback<TParameter>()
	{
		return new CallbackRecording<TParameter>();
	}
}
