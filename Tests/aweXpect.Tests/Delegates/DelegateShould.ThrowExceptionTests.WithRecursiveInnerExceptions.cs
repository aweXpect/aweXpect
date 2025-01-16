namespace aweXpect.Tests.Delegates;

public sealed partial class DelegateShould
{
	public sealed partial class ThrowException
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
					=> await That(action).Does().ThrowException().WithRecursiveInnerExceptions(
						e => e.Should().HaveAtLeast(minimum, x => x.Be<CustomException>()));

				await That(Act).Does().Throw<XunitException>().OnlyIf(shouldThrow)
					.WithMessage($"""
					              Expected action to
					              throw an exception with recursive inner exceptions which should have at least {minimum} items be type CustomException,
					              but only 1 of 5 were
					              """);
			}

			[Fact]
			public async Task WhenAwaited_WithExpectations_ShouldReturnThrownException()
			{
				Exception exception = new OuterException(innerException: new CustomException());
				void Delegate() => throw exception;

				Exception? result = await That(Delegate)
					.Does().ThrowException().WithRecursiveInnerExceptions(
						e => e.Should().HaveNone(x => x.Satisfy(_ => false)));

				await That(result).Should().BeSameAs(exception);
			}

			[Fact]
			public async Task WhenInnerExceptionDoesNotMatch_ShouldFail()
			{
				Action action = () => throw new OuterException(innerException: new CustomException());

				async Task Act()
					=> await That(action).Does().ThrowException().WithRecursiveInnerExceptions(
						e => e.Should().HaveAll(x => x.Satisfy(_ => false)));

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected action to
					             throw an exception with recursive inner exceptions which should have all items satisfy _ => false,
					             but not all were
					             """);
			}

			[Fact]
			public async Task WhenNoInnerExceptionIsPresent_ShouldNotFailDirectly()
			{
				Action action = () => throw new OuterException();

				async Task Act()
					=> await That(action).Does().ThrowException().WithRecursiveInnerExceptions(
						e => e.Should().HaveAll(x => x.Satisfy(_ => false)));

				await That(Act).Does().NotThrow();
			}
		}
	}
}
