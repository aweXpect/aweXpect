namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class ThrowsException
	{
		public sealed class WithInnerExceptionTests
		{
			[Fact]
			public async Task WhenAwaited_WithExpectations_ShouldReturnThrownException()
			{
				Exception exception = new OuterException(innerException: new Exception("inner"));
				void Delegate() => throw exception;

				Exception result = await That(Delegate)
					.ThrowsException().WithInnerException(
						e => e.HasMessage("inner"));

				await That(result).IsSameAs(exception);
			}

			[Fact]
			public async Task WhenAwaited_WithoutExpectations_ShouldReturnThrownException()
			{
				Exception exception = new OuterException(innerException: new Exception("inner"));
				void Delegate() => throw exception;

				Exception result = await That(Delegate)
					.ThrowsException().WithInnerException();

				await That(result).IsSameAs(exception);
			}

			[Fact]
			public async Task WhenInnerExceptionIsPresent_ShouldSucceed()
			{
				Action action = () => throw new OuterException(innerException: new Exception());

				async Task Act()
					=> await That(action).ThrowsException().WithInnerException();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenNoInnerExceptionIsPresent_ShouldFail()
			{
				Action action = () => throw new OuterException();

				async Task Act()
					=> await That(action).ThrowsException().WithInnerException();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected action to
					             throw an exception with an inner exception,
					             but it was <null>
					             """);
			}
		}
	}
}
