﻿namespace aweXpect.Tests.Delegates;

public sealed partial class DelegateShould
{
	public sealed class NotThrow
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActionDoesNotThrow_ShouldSucceed()
			{
				Action action = () => { };

				async Task Act()
					=> await That(action).Should().NotThrow();

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenActionThrows_ShouldFail(string message)
			{
				Exception exception = new CustomException(message);
				Action action = () => throw exception;

				async Task Act()
					=> await That(action).Should().NotThrow();

				await That(Act).Should().ThrowException()
					.WithMessage($"""
					              Expected action to
					              not throw any exception,
					              but it did throw a CustomException:
					                {message}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenAwaited_ShouldReturnResultFromDelegate(int value)
			{
				Func<int> action = () => value;

				int result = await That(action).Should().NotThrow();

				await That(result).Should().Be(value);
			}
		}
	}
}
