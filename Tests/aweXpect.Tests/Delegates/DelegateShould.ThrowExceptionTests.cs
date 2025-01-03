namespace aweXpect.Tests.Delegates;

public sealed partial class DelegateShould
{
	public sealed partial class ThrowException
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenAwaited_ShouldReturnThrownException()
			{
				Exception exception = new CustomException();
				Action action = () => throw exception;

				Exception result = await That(action).Should().ThrowException();

				await That(result).Should().BeSameAs(exception);
			}

			[Fact]
			public async Task WhenChained_ShouldOnlyDisplayInformationAboutNotThrownException()
			{
				Action action = () => { };

				async Task<Exception> Act()
					=> await That(action).Should().ThrowException().WithMessage("foo");

				await That(Act).Should().ThrowException()
					.WithMessage("""
					             Expected action to
					             throw an exception with Message equal to "foo",
					             but it did not throw any exception
					             """);
			}

			[Fact]
			public async Task WhenExceptionIsThrown_ShouldSucceed()
			{
				Exception exception = new CustomException();
				Action action = () => throw exception;

				async Task<Exception> Act()
					=> await That(action).Should().ThrowException();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenNoExceptionIsThrown_ShouldFail()
			{
				Action action = () => { };

				async Task<Exception> Act()
					=> await That(action).Should().ThrowException();

				await That(Act).Should().ThrowException()
					.WithMessage("""
					             Expected action to
					             throw an exception,
					             but it did not throw any exception
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Action? subject = null;

				async Task Act()
					=> await That(subject!).Should().ThrowException();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             throw an exception,
					             but it was <null>
					             """);
			}
		}
	}
}
