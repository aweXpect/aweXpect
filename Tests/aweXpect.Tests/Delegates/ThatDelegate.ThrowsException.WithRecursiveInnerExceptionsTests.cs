namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class ThrowsException
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
					=> await That(action).ThrowsException().WithRecursiveInnerExceptions(
						e => e.AtLeast(minimum).Are<CustomException>());

				await That(Act).Throws<XunitException>().OnlyIf(shouldThrow)
					.WithMessage($"""
					              Expected that action
					              throw an exception with recursive inner exceptions which should have at least {minimum} items be of type CustomException,
					              but only 1 of 5 were
					              """);
			}

			[Fact]
			public async Task WhenAwaited_WithExpectations_ShouldReturnThrownException()
			{
				Exception exception = new OuterException(innerException: new CustomException());
				void Delegate() => throw exception;

				Exception? result = await That(Delegate)
					.ThrowsException().WithRecursiveInnerExceptions(
						e => e.All().Satisfy(_ => true));

				await That(result).IsSameAs(exception);
			}

			[Fact]
			public async Task WhenInnerExceptionDoesNotMatch_ShouldFail()
			{
				Action action = () => throw new OuterException(innerException: new CustomException());

				async Task Act()
					=> await That(action).ThrowsException().WithRecursiveInnerExceptions(
						e => e.All().Satisfy(_ => false));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that action
					             throw an exception with recursive inner exceptions which should have all items satisfy _ => false,
					             but not all did
					             """);
			}

			[Fact]
			public async Task WhenNoInnerExceptionIsPresent_ShouldNotFailDirectly()
			{
				Action action = () => throw new OuterException();

				async Task Act()
					=> await That(action).ThrowsException().WithRecursiveInnerExceptions(
						e => e.All().Satisfy(_ => false));

				await That(Act).DoesNotThrow();
			}
		}
	}
}
