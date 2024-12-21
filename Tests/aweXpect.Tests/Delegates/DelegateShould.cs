using System.Runtime.CompilerServices;
using System.Threading;
using aweXpect.Tests.TestHelpers;

namespace aweXpect.Tests.Delegates;

public sealed partial class DelegateShould
{
	public class CustomException(
		[CallerMemberName] string message = "",
		Exception? innerException = null)
		: Exception(message, innerException)
	{
		public string? Value { get; set; }
	}

	public class SubCustomException(
		[CallerMemberName] string message = "",
		Exception? innerException = null)
		: CustomException(message, innerException);

	public class OtherException(
		[CallerMemberName] string message = "",
		Exception? innerException = null)
		: Exception(message, innerException);

	public class OuterException(
		[CallerMemberName] string message = "",
		Exception? innerException = null)
		: Exception(message, innerException);
}
