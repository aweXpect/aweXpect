﻿namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class ThrowsException
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
					=> await That(Delegate).ThrowsException()
						.Which(e => e.HResult, h => h.Is(expectedHResult));

				await That(Act).Throws<XunitException>()
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
					=> await That(Delegate).ThrowsException()
						.Which(e => e.HResult, h => h.Is(hResult));

				await That(Act).DoesNotThrow();
			}
		}
	}
}
