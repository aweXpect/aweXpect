namespace Testably.Expectations.Tests.ThatTests.Delegates;

public sealed partial class DelegateThrows
{
	public sealed class WithInnerExceptionTests
	{
		[Fact]
		public async Task WhenAwaited_WithExpectations_ShouldReturnThrownException()
		{
			Exception exception = new OuterException(innerException: new Exception("inner"));

			Exception result = await That(() => throw exception)
				.Should().ThrowException().WithInnerException(
					e => e.HaveMessage("inner"));

			await That(result).Should().BeSameAs(exception);
		}

		[Fact]
		public async Task WhenAwaited_WithoutExpectations_ShouldReturnThrownException()
		{
			Exception exception = new OuterException(innerException: new Exception("inner"));

			Exception result = await That(() => throw exception)
				.Should().ThrowException().WithInnerException();

			await That(result).Should().BeSameAs(exception);
		}

		[Fact]
		public async Task WhenInnerExceptionIsPresent_ShouldSucceed()
		{
			Action action = () => throw new OuterException(innerException: new Exception());

			async Task Act()
				=> await That(action).Should().ThrowException().WithInnerException();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenNoInnerExceptionIsPresent_ShouldFail()
		{
			Action action = () => throw new OuterException();

			async Task Act()
				=> await That(action).Should().ThrowException().WithInnerException();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected action to
				             throw an Exception with an inner exception,
				             but it was <null>
				             """);
		}
	}
}
