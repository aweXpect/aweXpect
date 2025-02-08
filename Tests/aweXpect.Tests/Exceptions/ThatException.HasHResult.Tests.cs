namespace aweXpect.Tests;

public sealed partial class ThatException
{
	public class HasHResult
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
					=> await That(subject).HasHResult(expectedHResult);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
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
					=> await That(subject).HasHResult(hResult);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				HResultException? subject = null;

				async Task Act()
					=> await That(subject).HasHResult(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             have HResult 1,
					             but it was <null>
					             """);
			}
		}
	}
}
