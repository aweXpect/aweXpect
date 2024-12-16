using aweXpect.Tests.TestHelpers;

namespace aweXpect.Tests.ThatTests.Delegates;

public sealed partial class DelegateThrows
{
	public class WhichTests
	{
		[Theory]
		[AutoData]
		public async Task WhenMemberIsDifferent_ShouldFail(int hResult)
		{
			int expectedHResult = hResult + 1;
			Exception exception = new HResultException(hResult);

			async Task Act()
				=> await That(() => throw exception).Should().ThrowException()
					.Which(e => e.HResult, h => h.Should().Be(expectedHResult));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected () => throw exception to
				              throw an Exception which .HResult should be equal to {expectedHResult},
				              but .HResult was {hResult}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task WhenMemberMatchesExpected_ShouldSucceed(int hResult)
		{
			Exception exception = new HResultException(hResult);

			async Task Act()
				=> await That(() => throw exception).Should().ThrowException()
					.Which(e => e.HResult, h => h.Should().Be(hResult));

			await That(Act).Should().NotThrow();
		}
	}
}
