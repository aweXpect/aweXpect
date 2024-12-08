using aweXpect.Tests.TestHelpers;

namespace aweXpect.Tests.ThatTests.Exceptions;

public sealed partial class ExceptionShould
{
	public class HaveHResultTests
	{
		[Theory]
		[AutoData]
		public async Task WhenHResultMatchesExpected_ShouldSucceed(int hResult)
		{
			Exception subject = new HResultException(hResult);

			async Task Act()
				=> await That(subject).Should().HaveHResult(hResult);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task WhenHResultIsDifferent_ShouldFail(int hResult)
		{
			int expectedHResult = hResult + 1;
			Exception subject = new HResultException(hResult);

			async Task Act()
				=> await That(subject).Should().HaveHResult(expectedHResult);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have HResult {expectedHResult},
				              but it had HResult {hResult}
				              """);
		}
	}
}
