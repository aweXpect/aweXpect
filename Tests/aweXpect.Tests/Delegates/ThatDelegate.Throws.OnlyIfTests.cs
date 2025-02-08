namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class Throws
	{
		public sealed class OnlyIfTests
		{
			[Fact]
			public async Task ShouldSupportChainedConstraints()
			{
				Action action = () => { };

				await That(action).Throws<Exception>()
					.OnlyIf(false)
					.WithMessage("foo");
			}

			[Fact]
			public async Task ShouldSupportChainedConstraintsForTypedException()
			{
				Action action = () => { };

				await That(action).Throws<ArgumentException>()
					.OnlyIf(false)
					.WithMessage("foo");
			}

			[Fact]
			public async Task WhenAwaited_OnlyIfFalse_ShouldReturnNull()
			{
				Action action = () => { };

				CustomException? result =
					await That(action).Throws<CustomException>().OnlyIf(false);

				await That(result).IsNull();
			}

			[Fact]
			public async Task WhenAwaited_OnlyIfTrue_ShouldReturnThrownException()
			{
				Exception exception = new CustomException();
				Action action = () => throw exception;

				CustomException? result =
					await That(action).Throws<CustomException>().OnlyIf(true);

				await That(result).IsSameAs(exception);
			}

			[Fact]
			public async Task WhenFalse_ShouldFailWhenAnExceptionWasThrown()
			{
				Exception exception = new("");
				Action action = () => throw exception;

				async Task Act()
					=> await That(action).Throws<Exception>().OnlyIf(false);

				await That(Act).ThrowsException()
					.WithMessage("""
					             Expected that action
					             not throw any exception,
					             but it did throw an Exception
					             """);
			}

			[Fact]
			public async Task WhenFalse_ShouldSucceedWhenNoExceptionWasThrown()
			{
				Action action = () => { };

				async Task Act()
					=> await That(action).Throws<Exception>().OnlyIf(false);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenTrue_ShouldFailWhenNoExceptionWasThrow()
			{
				Action action = () => { };

				async Task Act()
					=> await That(action).Throws<ArgumentException>().OnlyIf(true);

				await That(Act).ThrowsException()
					.WithMessage("""
					             Expected that action
					             throw an ArgumentException,
					             but it did not throw any exception
					             """);
			}

			[Fact]
			public async Task WhenTrue_ShouldSucceedWhenAnExceptionWasThrow()
			{
				Exception exception = new("");
				Action action = () => throw exception;

				async Task Act()
					=> await That(action).Throws<Exception>().OnlyIf(true);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
