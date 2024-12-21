using aweXpect.Tests.TestHelpers;

namespace aweXpect.Tests.Delegates;

public sealed partial class DelegateShould
{
	public sealed partial class ThrowException
	{
		public class WithHResultTests
		{
			[Theory]
			[AutoData]
			public async Task WhenHResultMatchesExpected_ShouldSucceed(int hResult)
			{
				Exception exception = new HResultException(hResult);
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Should()
						.ThrowException().WithHResult(hResult);

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenHResultIsDifferent_ShouldFail(int hResult)
			{
				int expectedHResult = hResult + 1;
				Exception exception = new HResultException(hResult);
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Should()
						.ThrowException().WithHResult(expectedHResult);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected Delegate to
					              throw an Exception with HResult {expectedHResult},
					              but it had HResult {hResult}
					              """);
			}
		}
	}
}
