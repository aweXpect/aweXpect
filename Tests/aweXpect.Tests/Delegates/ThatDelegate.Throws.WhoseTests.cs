namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class Throws
	{
		public class WhoseTests
		{
			[Theory(Skip = "TODO: Remove after core version update")]
			[AutoData]
			public async Task ShouldResetItAfterWhichClause(int hResult)
			{
				int otherHResult = hResult + 1;
				Exception exception = new HResultException(hResult);
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Throws<HResultException>()
						.Whose(e => e.HResult, h => h.IsEqualTo(hResult)).And
						.WithHResult(otherHResult);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that Delegate
					              throws a HResultException whose .HResult is equal to {hResult} and with HResult {otherHResult},
					              but it had HResult {hResult}
					              """);
			}

			[Theory(Skip = "TODO: Remove after core version update")]
			[AutoData]
			public async Task WhenMemberIsDifferent_ShouldFail(int hResult)
			{
				int expectedHResult = hResult + 1;
				Exception exception = new HResultException(hResult);
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Throws<HResultException>()
						.Whose(e => e.HResult, h => h.IsEqualTo(expectedHResult)).And
						.WithHResult(hResult);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that Delegate
					              throws a HResultException whose .HResult is equal to {expectedHResult} and with HResult {hResult},
					              but .HResult was {hResult} which differs by -1
					              """);
			}

			[Theory(Skip = "TODO: Remove after core version update")]
			[AutoData]
			public async Task WhenMemberMatchesExpected_ShouldSucceed(int hResult)
			{
				Exception exception = new HResultException(hResult);
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Throws<HResultException>()
						.Whose(e => e.HResult, h => h.IsEqualTo(hResult));

				await That(Act).DoesNotThrow();
			}
		}
	}
}
