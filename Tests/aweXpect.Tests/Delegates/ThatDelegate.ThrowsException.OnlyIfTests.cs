﻿namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class ThrowsException
	{
		public sealed class OnlyIfTests
		{
			[Fact]
			public async Task ShouldSupportChainedConstraints()
			{
				Action action = () => { };

				await That(action).ThrowsException()
					.OnlyIf(false)
					.WithMessage("foo");
			}

			[Fact]
			public async Task WhenAwaited_OnlyIfFalse_ShouldReturnNull()
			{
				Action action = () => { };

				Exception? result =
					await That(action).ThrowsException().OnlyIf(false);

				await That(result).IsNull();
			}

			[Fact]
			public async Task WhenAwaited_OnlyIfTrue_ShouldReturnThrownException()
			{
				Exception exception = new CustomException();
				Action action = () => throw exception;

				Exception? result =
					await That(action).ThrowsException().OnlyIf(true);

				await That(result).IsSameAs(exception);
			}

			[Fact]
			public async Task WhenFalse_ShouldFailWhenAnExceptionWasThrown()
			{
				Exception exception = new("");
				Action action = () => throw exception;

				async Task Act()
					=> await That(action).ThrowsException().OnlyIf(false);

				await That(Act).ThrowsException()
					.WithMessage("""
					             Expected that action
					             does not throw any exception,
					             but it did throw an Exception
					             """);
			}

			[Fact]
			public async Task WhenFalse_ShouldSucceedWhenNoExceptionWasThrown()
			{
				Action action = () => { };

				async Task Act()
					=> await That(action).ThrowsException().OnlyIf(false);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenTrue_ShouldFailWhenNoExceptionWasThrow()
			{
				Action action = () => { };

				async Task Act()
					=> await That(action).ThrowsException().OnlyIf(true);

				await That(Act).ThrowsException()
					.WithMessage("""
					             Expected that action
					             throws an exception,
					             but it did not throw any exception
					             """);
			}

			[Fact]
			public async Task WhenTrue_ShouldSucceedWhenAnExceptionWasThrow()
			{
				Exception exception = new("");
				Action action = () => throw exception;

				async Task Act()
					=> await That(action).ThrowsException().OnlyIf(true);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
