namespace aweXpect.Tests;

public sealed partial class ThatException
{
	public class CustomException(string message, Exception? innerException = null)
		: Exception(message, innerException);
}
