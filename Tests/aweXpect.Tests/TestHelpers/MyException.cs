using System.Runtime.CompilerServices;

namespace Testably.Expectations.Tests.TestHelpers;

public class MyException(
	[CallerMemberName] string message = "",
	Exception? innerException = null)
	: Exception(message, innerException);
