﻿namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class Throws
	{
		public class WithParamNameTests
		{
			[Theory]
			[AutoData]
			public async Task WhenParamNameIsDifferent_ShouldFail(string message)
			{
				ArgumentException exception = new(message, nameof(message));
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Throws<ArgumentException>()
						.WithParamName("somethingElse");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected Delegate to
					             throw an ArgumentException with ParamName "somethingElse",
					             but it had ParamName "message"
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenParamNameMatchesExpected_ShouldSucceed(string message)
			{
				ArgumentException exception = new(message, nameof(message));
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Throws<ArgumentException>()
						.WithParamName("message");

				await That(Act).DoesNotThrow();
			}
		}
	}
}