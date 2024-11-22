namespace Testably.Expectations.Tests.ThatTests.Exceptions;

public sealed partial class ExceptionShould
{
	public class CustomException(string message, Exception? innerException = null)
		: Exception(message, innerException);
}
