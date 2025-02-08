namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class ThrowsException
	{
		public class WithHResultTests
		{
			[Theory]
			[AutoData]
			public async Task WhenHResultIsDifferent_ShouldFail(int hResult)
			{
				int expectedHResult = hResult + 1;
				Exception exception = new HResultException(hResult);
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).ThrowsException().WithHResult(expectedHResult);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that Delegate
					              throw an exception with HResult {expectedHResult},
					              but it had HResult {hResult}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenHResultMatchesExpected_ShouldSucceed(int hResult)
			{
				Exception exception = new HResultException(hResult);
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).ThrowsException().WithHResult(hResult);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
