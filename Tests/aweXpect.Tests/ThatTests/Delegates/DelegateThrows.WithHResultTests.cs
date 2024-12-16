using aweXpect.Tests.TestHelpers;

namespace aweXpect.Tests.ThatTests.Delegates;

public sealed partial class DelegateThrows
{
	public class WithHResultTests
	{
		[Theory]
		[AutoData]
		public async Task WhenHResultMatchesExpected_ShouldSucceed(int hResult)
		{
			Exception exception = new HResultException(hResult);

			async Task Act()
				=> await That(() => throw exception).Should()
					.ThrowException().WithHResult(hResult);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task WhenHResultIsDifferent_ShouldFail(int hResult)
		{
			int expectedHResult = hResult + 1;
			Exception exception = new HResultException(hResult);

			async Task Act()
				=> await That(() => throw exception).Should()
					.ThrowException().WithHResult(expectedHResult);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected () => throw exception to
				              throw an Exception with HResult {expectedHResult},
				              but it had HResult {hResult}
				              """);
		}
	}
}
