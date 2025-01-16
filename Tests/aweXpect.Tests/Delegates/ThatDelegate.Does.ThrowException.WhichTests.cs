using aweXpect.Tests.TestHelpers;

namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class Does
	{
		public sealed partial class ThrowException
		{
			public class WhichTests
			{
				[Theory]
				[AutoData]
				public async Task WhenMemberIsDifferent_ShouldFail(int hResult)
				{
					int expectedHResult = hResult + 1;
					Exception exception = new HResultException(hResult);
					void Delegate() => throw exception;

					async Task Act()
						=> await That(Delegate).Does().ThrowException()
							.Which(e => e.HResult, h => h.Should().Be(expectedHResult));

					await That(Act).Does().Throw<XunitException>()
						.WithMessage($"""
						              Expected Delegate to
						              throw an exception which .HResult should be equal to {expectedHResult},
						              but .HResult was {hResult}
						              """);
				}

				[Theory]
				[AutoData]
				public async Task WhenMemberMatchesExpected_ShouldSucceed(int hResult)
				{
					Exception exception = new HResultException(hResult);
					void Delegate() => throw exception;

					async Task Act()
						=> await That(Delegate).Does().ThrowException()
							.Which(e => e.HResult, h => h.Should().Be(hResult));

					await That(Act).Does().NotThrow();
				}
			}
		}
	}
}
