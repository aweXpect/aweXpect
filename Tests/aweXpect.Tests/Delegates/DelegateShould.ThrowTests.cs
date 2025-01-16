namespace aweXpect.Tests.Delegates;

public sealed partial class DelegateShould
{
	public sealed partial class Throw
	{
		public sealed class GenericTests
		{
			[Theory]
			[AutoData]
			public async Task WhenAwaited_ShouldReturnThrownException(string value)
			{
				Exception exception = new CustomException
				{
					Value = value
				};
				Action action = () => throw exception;

				CustomException result =
					await That(action).Does().Throw<CustomException>();

				await That(result.Value).Should().Be(value);
				await That(result).Should().BeSameAs(exception);
			}

			[Fact]
			public async Task WhenExactExceptionTypeIsThrown_ShouldSucceed()
			{
				Exception exception = new CustomException();
				Action action = () => throw exception;

				async Task<CustomException> Act()
					=> await That(action).Does().Throw<CustomException>();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenNoExceptionIsThrown_ShouldFail()
			{
				Action action = () => { };

				async Task<CustomException> Act()
					=> await That(action).Does().Throw<CustomException>();

				await That(Act).Does().ThrowException()
					.WithMessage("""
					             Expected action to
					             throw a CustomException,
					             but it did not throw any exception
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenOtherExceptionIsThrown_ShouldFail(string message)
			{
				Exception exception = new OtherException(message);
				Action action = () => throw exception;

				async Task<CustomException> Act()
					=> await That(action).Does().Throw<CustomException>();

				await That(Act).Does().ThrowException()
					.WithMessage($"""
					              Expected action to
					              throw a CustomException,
					              but it did throw an OtherException:
					                {message}
					              """);
			}

			[Fact]
			public async Task WhenSubCustomExceptionIsThrown_ShouldSucceed()
			{
				Exception exception = new SubCustomException();
				Action action = () => throw exception;

				async Task<CustomException> Act()
					=> await That(action).Does().Throw<CustomException>();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Action? subject = null;

				async Task Act()
					=> await That(subject!).Does().Throw<CustomException>();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             throw a CustomException,
					             but it was <null>
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenSupertypeExceptionIsThrown_ShouldFail(string message)
			{
				Exception exception = new CustomException(message);
				Action action = () => throw exception;

				async Task<SubCustomException> Act()
					=> await That(action).Does().Throw<SubCustomException>();

				await That(Act).Does().ThrowException()
					.WithMessage($"""
					              Expected action to
					              throw a SubCustomException,
					              but it did throw a CustomException:
					                {message}
					              """);
			}
		}

		public sealed class TypeTests
		{
			[Theory]
			[AutoData]
			public async Task WhenAwaited_ShouldReturnThrownException(string value)
			{
				Exception exception = new CustomException
				{
					Value = value
				};
				Action action = () => throw exception;

				Exception result =
					await That(action).Does().Throw(typeof(CustomException));

				await That(result).Should().BeSameAs(exception);
			}

			[Fact]
			public async Task WhenExactExceptionTypeIsThrown_ShouldSucceed()
			{
				Exception exception = new CustomException();
				Action action = () => throw exception;

				async Task<Exception> Act()
					=> await That(action).Does().Throw(typeof(CustomException));

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenNoExceptionIsThrown_ShouldFail()
			{
				Action action = () => { };

				async Task<Exception> Act()
					=> await That(action).Does().Throw(typeof(CustomException));

				await That(Act).Does().ThrowException()
					.WithMessage("""
					             Expected action to
					             throw a CustomException,
					             but it did not
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenOtherExceptionIsThrown_ShouldFail(string message)
			{
				Exception exception = new OtherException(message);
				Action action = () => throw exception;

				async Task<Exception> Act()
					=> await That(action).Does().Throw(typeof(CustomException));

				await That(Act).Does().ThrowException()
					.WithMessage($"""
					              Expected action to
					              throw a CustomException,
					              but it did throw an OtherException:
					                {message}
					              """);
			}

			[Fact]
			public async Task WhenSubCustomExceptionIsThrown_ShouldSucceed()
			{
				Exception exception = new SubCustomException();
				Action action = () => throw exception;

				async Task<Exception> Act()
					=> await That(action).Does().Throw(typeof(CustomException));

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Action? subject = null;

				async Task Act()
					=> await That(subject!).Does().Throw(typeof(CustomException));

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             throw a CustomException,
					             but it was <null>
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenSupertypeExceptionIsThrown_ShouldFail(string message)
			{
				Exception exception = new CustomException(message);
				Action action = () => throw exception;

				async Task<Exception> Act()
					=> await That(action).Does().Throw(typeof(SubCustomException));

				await That(Act).Does().ThrowException()
					.WithMessage($"""
					              Expected action to
					              throw a SubCustomException,
					              but it did throw a CustomException:
					                {message}
					              """);
			}
		}
	}
}
