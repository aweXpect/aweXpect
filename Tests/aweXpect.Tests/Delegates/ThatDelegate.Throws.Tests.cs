﻿namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class Throws
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
					await That(action).Throws<CustomException>();

				await That(result.Value).IsEqualTo(value);
				await That(result).IsSameAs(exception);
			}

			[Fact]
			public async Task WhenExactExceptionTypeIsThrown_ShouldSucceed()
			{
				Exception exception = new CustomException();
				Action action = () => throw exception;

				async Task<CustomException> Act()
					=> await That(action).Throws<CustomException>();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenNoExceptionIsThrown_ShouldFail()
			{
				Action action = () => { };

				async Task<CustomException> Act()
					=> await That(action).Throws<CustomException>();

				await That(Act).ThrowsException()
					.WithMessage("""
					             Expected that action
					             throws a CustomException,
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
					=> await That(action).Throws<CustomException>();

				await That(Act).ThrowsException()
					.WithMessage($"""
					              Expected that action
					              throws a CustomException,
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
					=> await That(action).Throws<CustomException>();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Action? subject = null;

				async Task Act()
					=> await That(subject!).Throws<CustomException>();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             throws a CustomException,
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
					=> await That(action).Throws<SubCustomException>();

				await That(Act).ThrowsException()
					.WithMessage($"""
					              Expected that action
					              throws a SubCustomException,
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
					await That(action).Throws(typeof(CustomException));

				await That(result).IsSameAs(exception);
			}

			[Fact]
			public async Task WhenExactExceptionTypeIsThrown_ShouldSucceed()
			{
				Exception exception = new CustomException();
				Action action = () => throw exception;

				async Task<Exception> Act()
					=> await That(action).Throws(typeof(CustomException));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenNoExceptionIsThrown_ShouldFail()
			{
				Action action = () => { };

				async Task<Exception> Act()
					=> await That(action).Throws(typeof(CustomException));

				await That(Act).ThrowsException()
					.WithMessage("""
					             Expected that action
					             throws a CustomException,
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
					=> await That(action).Throws(typeof(CustomException));

				await That(Act).ThrowsException()
					.WithMessage($"""
					              Expected that action
					              throws a CustomException,
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
					=> await That(action).Throws(typeof(CustomException));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Action? subject = null;

				async Task Act()
					=> await That(subject!).Throws(typeof(CustomException));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             throws a CustomException,
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
					=> await That(action).Throws(typeof(SubCustomException));

				await That(Act).ThrowsException()
					.WithMessage($"""
					              Expected that action
					              throws a SubCustomException,
					              but it did throw a CustomException:
					                {message}
					              """);
			}
		}
	}
}
