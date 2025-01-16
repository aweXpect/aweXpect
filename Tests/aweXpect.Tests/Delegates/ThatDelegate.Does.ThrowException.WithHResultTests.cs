using aweXpect.Tests.TestHelpers;

namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class Does
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
						=> await That(Delegate).Does()
							.ThrowException().WithHResult(hResult);

					await That(Act).Does().NotThrow();
				}

				[Theory]
				[AutoData]
				public async Task WhenHResultIsDifferent_ShouldFail(int hResult)
				{
					int expectedHResult = hResult + 1;
					Exception exception = new HResultException(hResult);
					void Delegate() => throw exception;

					async Task Act()
						=> await That(Delegate).Does()
							.ThrowException().WithHResult(expectedHResult);

					await That(Act).Does().Throw<XunitException>()
						.WithMessage($"""
						              Expected Delegate to
						              throw an exception with HResult {expectedHResult},
						              but it had HResult {hResult}
						              """);
				}
			}
		}
	}
}
