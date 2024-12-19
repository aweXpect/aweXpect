namespace aweXpect.Tests.ThatTests.Delegates;

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
					e => e.HaveAtLeast(minimum, x => x.Be<CustomException>()));

			await That(Act).Should().Throw<XunitException>().OnlyIf(shouldThrow)
				.WithMessage($"""
				              Expected action to
				              throw an Exception with recursive inner exceptions which have at least {minimum} items be type CustomException,
				              but only 1 of 5 were
				              """);
		}

		[Fact]
		public async Task WhenAwaited_WithExpectations_ShouldReturnThrownException()
		{
			Exception exception = new OuterException(innerException: new CustomException());
			void Delegate() => throw exception;

			Exception? result = await That(Delegate)
				.Should().ThrowException().WithRecursiveInnerExceptions(
					e => e.HaveNone(x => x.Satisfy(_ => false)));

			await That(result).Should().BeSameAs(exception);
		}

		[Fact]
		public async Task WhenInnerExceptionDoesNotMatch_ShouldFail()
		{
			Action action = () => throw new OuterException(innerException: new CustomException());

			async Task Act()
				=> await That(action).Should().ThrowException().WithRecursiveInnerExceptions(
					e => e.HaveAll(x => x.Satisfy(_ => false)));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected action to
				             throw an Exception with recursive inner exceptions which have all items satisfy _ => false,
				             but not all were
				             """);
		}

		[Fact]
		public async Task WhenNoInnerExceptionIsPresent_ShouldNotFailDirectly()
		{
			Action action = () => throw new OuterException();

			async Task Act()
				=> await That(action).Should().ThrowException().WithRecursiveInnerExceptions(
					e => e.HaveAll(x => x.Satisfy(_ => false)));

			await That(Act).Should().NotThrow();
		}
	}
}
