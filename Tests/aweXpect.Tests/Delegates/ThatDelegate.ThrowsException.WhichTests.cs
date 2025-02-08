namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class ThrowsException
	{
		public class WhichTests
		{
			[Theory]
			[AutoData]
			public async Task ShouldResetItAfterWhichClause(int hResult)
			{
				int otherHResult = hResult + 1;
				Exception exception = new HResultException(hResult);
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).ThrowsException()
						.Which(e => e.HResult, h => h.IsEqualTo(hResult)).And
						.WithHResult(otherHResult);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that Delegate
					              throw an exception which .HResult should be equal to {hResult} and with HResult {otherHResult},
					              but it had HResult {hResult}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenMemberIsDifferent_ShouldFail(int hResult)
			{
				int expectedHResult = hResult + 1;
				Exception exception = new HResultException(hResult);
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).ThrowsException()
						.Which(e => e.HResult, h => h.IsEqualTo(expectedHResult)).And
						.WithHResult(hResult);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that Delegate
					              throw an exception which .HResult should be equal to {expectedHResult} and with HResult {hResult},
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
					=> await That(Delegate).ThrowsException()
						.Which(e => e.HResult, h => h.IsEqualTo(hResult));

				await That(Act).DoesNotThrow();
			}
		}
	}
}
