namespace Testably.Expectations.Tests.ThatTests.Delegates;

public sealed partial class DelegateThrows
{
	public sealed class WithRecursiveInnerExceptionsTests
	{
		[Theory]
		[InlineData(1, false)]
		[InlineData(2, true)]
		public async Task WhenAnyInnerExceptionDoesMatch_ShouldSucceed(int minimum,
			bool shouldThrow)
		{
			Action action = () => throw new OuterException(
				innerException: new OtherException(
					innerException: new AggregateException(
						new OtherException(),
						new OtherException(
							innerException: new CustomException()))));

			async Task Act()
				=> await That(action).Should().ThrowException().WithRecursiveInnerExceptions(
					e => e.AtLeast(minimum).Be<CustomException>());

			await That(Act).Should().Throw<XunitException>().OnlyIf(shouldThrow)
				.WithMessage($"""
				              Expected action to
				              throw an Exception with recursive inner exceptions which have at least {minimum} items of type CustomException,
				              but only 1 of 5 items were
				              """);
		}

		[Fact]
		public async Task WhenAwaited_WithExpectations_ShouldReturnThrownException()
		{
			Exception exception = new OuterException(innerException: new CustomException());

			Exception? result = await That(() => throw exception)
				.Should().ThrowException().WithRecursiveInnerExceptions(
					e => e.None().Satisfy(_ => false));

			await That(result).Should().BeSameAs(exception);
		}

		[Fact]
		public async Task WhenInnerExceptionDoesNotMatch_ShouldFail()
		{
			Action action = () => throw new OuterException(innerException: new CustomException());

			async Task Act()
				=> await That(action).Should().ThrowException().WithRecursiveInnerExceptions(
					e => e.All().Satisfy(_ => false));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected action to
				             throw an Exception with recursive inner exceptions which all satisfy "_ => false",
				             but not all did
				             """);
		}

		[Fact]
		public async Task WhenNoInnerExceptionIsPresent_ShouldNotFailDirectly()
		{
			Action action = () => throw new OuterException();

			async Task Act()
				=> await That(action).Should().ThrowException().WithRecursiveInnerExceptions(
					e => e.All().Satisfy(_ => false));

			await That(Act).Should().NotThrow();
		}
	}
}
