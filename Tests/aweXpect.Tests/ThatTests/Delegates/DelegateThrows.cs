using System.Runtime.CompilerServices;

namespace Testably.Expectations.Tests.ThatTests.Delegates;

public sealed partial class DelegateThrows
{
	[Theory]
	[AutoData]
	public async Task ShouldSupportNestedChecks(
		string innermostMessage, string innerMessage, string outerMessage)
	{
		Exception exception = new CustomException(outerMessage,
			new SubCustomException(innerMessage,
				// ReSharper disable once NotResolvedInText
				new ArgumentException(innermostMessage, paramName: nameof(innermostMessage))));
		void Act() => throw exception;

		CustomException result = await That(Act).Should().Throw<CustomException>()
			.WithInnerException(e1 => e1
				.HaveMessage(innerMessage).And
				.HaveInner<ArgumentException>(e2 => e2
					.HaveParamName(nameof(innermostMessage)).And.HaveMessage($"{innermostMessage}*")
					.AsWildcard()));

		await That(result).Should().BeSameAs(exception);
	}

	public class CustomException(
		[CallerMemberName] string message = "",
		Exception? innerException = null)
		: Exception(message, innerException);

	public class OtherException(
		[CallerMemberName] string message = "",
		Exception? innerException = null)
		: Exception(message, innerException);

	public class OuterException(
		[CallerMemberName] string message = "",
		Exception? innerException = null)
		: Exception(message, innerException);

	public class SubCustomException(
		[CallerMemberName] string message = "",
		Exception? innerException = null)
		: CustomException(message, innerException);
}
