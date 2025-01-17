using System.Runtime.CompilerServices;

namespace aweXpect.Tests;

public class MyException(
	[CallerMemberName] string message = "",
	Exception? innerException = null)
	: Exception(message, innerException);
