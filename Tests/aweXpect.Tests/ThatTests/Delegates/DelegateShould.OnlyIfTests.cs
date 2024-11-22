namespace Testably.Expectations.Tests.ThatTests.Delegates;

public sealed partial class DelegateShould
{
	public sealed class OnlyIfTests
	{
		[Fact]
		public async Task ShouldSupportChainedConstraints()
		{
			Action action = () => { };

			await That(action).Should().ThrowException()
				.OnlyIf(false)
				.WithMessage("foo");
		}

		[Fact]
		public async Task ShouldSupportChainedConstraintsForTypedException()
		{
			Action action = () => { };

			await That(action).Should().Throw<ArgumentException>()
				.OnlyIf(false)
				.WithMessage("foo");
		}

		[Fact]
		public async Task WhenAwaited_OnlyIfFalse_ShouldReturnNull()
		{
			Action action = () => { };

			CustomException? result =
				await That(action).Should().Throw<CustomException>().OnlyIf(false);

			await That(result).Should().BeNull();
		}

		[Fact]
		public async Task WhenAwaited_OnlyIfTrue_ShouldReturnThrownException()
		{
			Exception exception = new CustomException();
			Action action = () => throw exception;

			CustomException? result =
				await That(action).Should().Throw<CustomException>().OnlyIf(true);

			await That(result).Should().BeSameAs(exception);
		}

		[Fact]
		public async Task WhenFalse_ShouldFailWhenAnExceptionWasThrown()
		{
			Exception exception = new("");
			Action action = () => throw exception;

			async Task Act()
				=> await That(action).Should().ThrowException().OnlyIf(false);

			await That(Act).Should().ThrowException()
				.WithMessage("""
				             Expected action to
				             not throw any exception,
				             but it did throw an Exception
				             """);
		}

		[Fact]
		public async Task WhenFalse_ShouldSucceedWhenNoExceptionWasThrown()
		{
			Action action = () => { };

			async Task Act()
				=> await That(action).Should().ThrowException().OnlyIf(false);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenTrue_ShouldFailWhenNoExceptionWasThrow()
		{
			Action action = () => { };

			async Task Act()
				=> await That(action).Should().ThrowException().OnlyIf(true);

			await That(Act).Should().ThrowException()
				.WithMessage("""
				             Expected action to
				             throw an Exception,
				             but it did not throw any exception
				             """);
		}

		[Fact]
		public async Task WhenTrue_ShouldSucceedWhenAnExceptionWasThrow()
		{
			Exception exception = new("");
			Action action = () => throw exception;

			async Task Act()
				=> await That(action).Should().ThrowException().OnlyIf(true);

			await That(Act).Should().NotThrow();
		}
	}
}
