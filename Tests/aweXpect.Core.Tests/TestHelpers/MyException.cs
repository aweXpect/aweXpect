using System.Runtime.CompilerServices;

namespace aweXpect.Core.Tests.TestHelpers;

public class MyException(
	[CallerMemberName] string message = "",
	Exception? innerException = null)
	: Exception(message, innerException);
