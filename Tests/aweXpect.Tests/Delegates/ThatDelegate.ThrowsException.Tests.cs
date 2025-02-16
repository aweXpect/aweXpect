namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class ThrowsException
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenAwaited_ShouldReturnThrownException()
			{
				Exception exception = new CustomException();
				Action action = () => throw exception;

				Exception result = await That(action).ThrowsException();

				await That(result).IsSameAs(exception);
			}

			[Fact]
			public async Task WhenChained_ShouldOnlyDisplayInformationAboutNotThrownException()
			{
				Action action = () => { };

				async Task<Exception> Act()
					=> await That(action).ThrowsException().WithMessage("foo");

				await That(Act).ThrowsException()
					.WithMessage("""
					             Expected that action
					             throws an exception with Message equal to "foo",
					             but it did not throw any exception
					             """);
			}

			[Fact]
			public async Task WhenExceptionIsThrown_ShouldSucceed()
			{
				Exception exception = new CustomException();
				Action action = () => throw exception;

				async Task<Exception> Act()
					=> await That(action).ThrowsException();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenNoExceptionIsThrown_ShouldFail()
			{
				Action action = () => { };

				async Task<Exception> Act()
					=> await That(action).ThrowsException();

				await That(Act).ThrowsException()
					.WithMessage("""
					             Expected that action
					             throws an exception,
					             but it did not throw any exception
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Action? subject = null;

				async Task Act()
					=> await That(subject!).ThrowsException();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             throws an exception,
					             but it was <null>
					             """);
			}
		}
	}
}
