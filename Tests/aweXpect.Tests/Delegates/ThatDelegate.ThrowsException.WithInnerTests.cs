﻿namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class ThrowsException
	{
		public sealed class WithInner
		{
			public sealed class GenericTests
			{
				[Fact]
				public async Task CustomException_WhenInnerExceptionDoesNotMatchCriteria_ShouldFail()
				{
					string message = "bar";
					Action action = ()
						=> throw new OuterException(innerException: new CustomException(message));

					async Task Act()
						=> await That(action).ThrowsException().WithInner<CustomException>(x => x.HasMessage("foo"));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that action
						             throws an exception with an inner CustomException whose Message is equal to "foo",
						             but it was "bar" which differs at index 0:
						                ↓ (actual)
						               "bar"
						               "foo"
						                ↑ (expected)
						             """);
				}

				[Fact]
				public async Task Exception_WhenInnerExceptionDoesNotMatchCriteria_ShouldFail()
				{
					string message = "bar";
					Action action = ()
						=> throw new OuterException(innerException: new CustomException(message));

					async Task Act()
						=> await That(action).ThrowsException().WithInner<Exception>(x => x.HasMessage("foo"));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that action
						             throws an exception with an inner exception whose Message is equal to "foo",
						             but it was "bar" which differs at index 0:
						                ↓ (actual)
						               "bar"
						               "foo"
						                ↑ (expected)
						             """);
				}

				[Theory]
				[AutoData]
				public async Task WhenAwaited_WithExpectations_ShouldReturnThrownException(
					string message)
				{
					Exception exception = new OuterException(innerException: new CustomException(message));
					void Delegate() => throw exception;

					Exception result = await That(Delegate)
						.ThrowsException().WithInner<CustomException>(
							e => e.HasMessage(message));

					await That(result).IsSameAs(exception);
				}

				[Fact]
				public async Task WhenAwaited_WithoutExpectations_ShouldReturnThrownException()
				{
					Exception exception = new OuterException(innerException: new CustomException());
					void Delegate() => throw exception;

					Exception result = await That(Delegate)
						.ThrowsException().WithInner<CustomException>();

					await That(result).IsSameAs(exception);
				}

				[Theory]
				[AutoData]
				public async Task WhenInnerExceptionHasSuperType_ShouldFail(string message)
				{
					Action action = ()
						=> throw new OuterException(innerException: new CustomException(message));

					async Task Act()
						=> await That(action).ThrowsException().WithInner<SubCustomException>();

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that action
						              throws an exception with an inner SubCustomException,
						              but it was a CustomException:
						                {message}
						              """);
				}

				[Theory]
				[AutoData]
				public async Task WhenInnerExceptionHasWrongType_ShouldFail(string message)
				{
					Action action = ()
						=> throw new OuterException(innerException: new OtherException(message));

					async Task Act()
						=> await That(action).ThrowsException().WithInner<CustomException>();

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that action
						              throws an exception with an inner CustomException,
						              but it was an OtherException:
						                {message}
						              """);
				}

				[Fact]
				public async Task WhenInnerExceptionIsNotPresent_ShouldFail()
				{
					Action action = () => throw new OuterException();

					async Task Act()
						=> await That(action).ThrowsException().WithInner<Exception>();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that action
						             throws an exception with an inner exception,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WhenInnerExceptionIsPresent_ShouldSucceed()
				{
					Action action = () => throw new OuterException(innerException: new CustomException());

					async Task Act()
						=> await That(action).ThrowsException().WithInner<CustomException>();

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenInnerExceptionIsSubType_ShouldSucceed()
				{
					Action action = ()
						=> throw new OuterException(innerException: new SubCustomException());

					async Task Act()
						=> await That(action).ThrowsException().WithInner<CustomException>();

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenNoInnerExceptionIsPresent_ShouldFail()
				{
					Action action = () => throw new OuterException();

					async Task Act()
						=> await That(action).ThrowsException().WithInner<CustomException>();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that action
						             throws an exception with an inner CustomException,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WithInnerExpectations_WhenInnerExceptionDoesNotMatchCriteria_ShouldFail()
				{
					string message = "bar";
					Action action = ()
						=> throw new OuterException(innerException: new CustomException(message));

					async Task Act()
						=> await That(action).ThrowsException().WithInner<Exception>(x => x.HasMessage("foo"));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that action
						             throws an exception with an inner exception whose Message is equal to "foo",
						             but it was "bar" which differs at index 0:
						                ↓ (actual)
						               "bar"
						               "foo"
						                ↑ (expected)
						             """);
				}

				[Fact]
				public async Task WithInnerExpectations_WhenInnerExceptionIsNotPresent_ShouldFail()
				{
					Action action = () => throw new OuterException();

					async Task Act()
						=> await That(action).ThrowsException()
							.WithInner<Exception>(x => x.HasMessage("foo"));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that action
						             throws an exception with an inner exception whose Message is equal to "foo",
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WithInnerExpectations_WhenNoExceptionIsThrown_ShouldFail()
				{
					Action action = () => { };

					async Task Act()
						=> await That(action).ThrowsException()
							.WithInner<CustomException>(x => x.HasMessage("foo"));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that action
						             throws an exception with an inner CustomException whose Message is equal to "foo",
						             but it did not throw any exception
						             """);
				}
			}

			public sealed class TypeTests
			{
				[Fact]
				public async Task CustomException_WhenInnerExceptionDoesNotMatchCriteria_ShouldFail()
				{
					string message = "bar";
					Action action = ()
						=> throw new OuterException(innerException: new CustomException(message));

					async Task Act()
						=> await That(action).ThrowsException()
							.WithInner(typeof(CustomException), x => x.HasMessage("foo"));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that action
						             throws an exception with an inner CustomException whose Message is equal to "foo",
						             but it was "bar" which differs at index 0:
						                ↓ (actual)
						               "bar"
						               "foo"
						                ↑ (expected)
						             """);
				}

				[Fact]
				public async Task Exception_WhenInnerExceptionDoesNotMatchCriteria_ShouldFail()
				{
					string message = "bar";
					Action action = ()
						=> throw new OuterException(innerException: new CustomException(message));

					async Task Act()
						=> await That(action).ThrowsException()
							.WithInner(typeof(Exception), x => x.HasMessage("foo"));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that action
						             throws an exception with an inner exception whose Message is equal to "foo",
						             but it was "bar" which differs at index 0:
						                ↓ (actual)
						               "bar"
						               "foo"
						                ↑ (expected)
						             """);
				}

				[Theory]
				[AutoData]
				public async Task WhenAwaited_WithExpectations_ShouldReturnThrownException(
					string message)
				{
					Exception exception = new OuterException(innerException: new CustomException(message));
					void Delegate() => throw exception;

					Exception result = await That(Delegate)
						.ThrowsException().WithInner(typeof(CustomException),
							e => e.HasMessage(message));

					await That(result).IsSameAs(exception);
				}

				[Fact]
				public async Task WhenAwaited_WithoutExpectations_ShouldReturnThrownException()
				{
					Exception exception = new OuterException(innerException: new CustomException());
					void Delegate() => throw exception;

					Exception result = await That(Delegate)
						.ThrowsException().WithInner(typeof(CustomException));

					await That(result).IsSameAs(exception);
				}

				[Theory]
				[AutoData]
				public async Task WhenInnerExceptionHasSuperType_ShouldFail(string message)
				{
					Action action = ()
						=> throw new OuterException(innerException: new CustomException(message));

					async Task Act()
						=> await That(action).ThrowsException()
							.WithInner(typeof(SubCustomException));

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that action
						              throws an exception with an inner SubCustomException,
						              but it was a CustomException:
						                {message}
						              """);
				}

				[Theory]
				[AutoData]
				public async Task WhenInnerExceptionHasWrongType_ShouldFail(string message)
				{
					Action action = ()
						=> throw new OuterException(innerException: new OtherException(message));

					async Task Act()
						=> await That(action).ThrowsException().WithInner(typeof(CustomException));

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that action
						              throws an exception with an inner CustomException,
						              but it was an OtherException:
						                {message}
						              """);
				}

				[Fact]
				public async Task WhenInnerExceptionIsNotPresent_ShouldFail()
				{
					Action action = () => throw new OuterException();

					async Task Act()
						=> await That(action).ThrowsException().WithInner(typeof(Exception));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that action
						             throws an exception with an inner exception,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WhenInnerExceptionIsPresent_ShouldSucceed()
				{
					Action action = () => throw new OuterException(innerException: new CustomException());

					async Task Act()
						=> await That(action).ThrowsException().WithInner(typeof(CustomException));

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenInnerExceptionIsSubType_ShouldSucceed()
				{
					Action action = ()
						=> throw new OuterException(innerException: new SubCustomException());

					async Task Act()
						=> await That(action).ThrowsException().WithInner(typeof(CustomException));

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenNoInnerExceptionIsPresent_ShouldFail()
				{
					Action action = () => throw new OuterException();

					async Task Act()
						=> await That(action).ThrowsException().WithInner(typeof(CustomException));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that action
						             throws an exception with an inner CustomException,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WithInnerExpectations_WhenInnerExceptionDoesNotMatchCriteria_ShouldFail()
				{
					string message = "bar";
					Action action = ()
						=> throw new OuterException(innerException: new CustomException(message));

					async Task Act()
						=> await That(action).ThrowsException()
							.WithInner(typeof(Exception), x => x.HasMessage("foo"));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that action
						             throws an exception with an inner exception whose Message is equal to "foo",
						             but it was "bar" which differs at index 0:
						                ↓ (actual)
						               "bar"
						               "foo"
						                ↑ (expected)
						             """);
				}

				[Fact]
				public async Task WithInnerExpectations_WhenNoExceptionIsThrown_ShouldFail()
				{
					Action action = () => { };

					async Task Act()
						=> await That(action).ThrowsException()
							.WithInner(typeof(CustomException), x => x.HasMessage("foo"));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that action
						             throws an exception with an inner CustomException whose Message is equal to "foo",
						             but it did not throw any exception
						             """);
				}
			}
		}
	}
}
