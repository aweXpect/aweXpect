using aweXpect.Tests.TestHelpers;

namespace aweXpect.Tests.Exceptions;

public sealed partial class ExceptionShould
{
	public class HaveHResult
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task WhenHResultIsDifferent_ShouldFail(int hResult)
			{
				int expectedHResult = hResult + 1;
				Exception subject = new HResultException(hResult);

				async Task Act()
					=> await That(subject).Should().HaveHResult(expectedHResult);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have HResult {expectedHResult},
					              but it had HResult {hResult}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenHResultMatchesExpected_ShouldSucceed(int hResult)
			{
				Exception subject = new HResultException(hResult);

				async Task Act()
					=> await That(subject).Should().HaveHResult(hResult);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				HResultException? subject = null;

				async Task Act()
					=> await That(subject).Should().HaveHResult(1);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have HResult 1,
					             but it was <null>
					             """);
			}
		}
	}
}
