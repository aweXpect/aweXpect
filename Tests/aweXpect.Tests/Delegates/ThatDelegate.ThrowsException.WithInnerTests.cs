namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class ThrowsException
	{
		public sealed class WithInnerTests
		{
			[Theory]
			[AutoData]
			public async Task ForGeneric_WhenAwaited_WithExpectations_ShouldReturnThrownException(
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
			public async Task ForGeneric_WhenAwaited_WithoutExpectations_ShouldReturnThrownException()
			{
				Exception exception = new OuterException(innerException: new CustomException());
				void Delegate() => throw exception;

				Exception result = await That(Delegate)
					.ThrowsException().WithInner<CustomException>();

				await That(result).IsSameAs(exception);
			}

			[Fact(Skip="Temporarily skip until next Core update")]
			public async Task ForGeneric_WhenInnerExceptionDoesNotMatchCriteria_ShouldFail()
			{
				string message = "bar";
				Action action = ()
					=> throw new OuterException(innerException: new CustomException(message));

				async Task Act()
					=> await That(action).ThrowsException().WithInner<Exception>(x => x.HasMessage("foo"));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected action to
					             throw an exception with an inner exception which should have Message equal to "foo",
					             but it was "bar" which differs at index 0:
					                ↓ (actual)
					               "bar"
					               "foo"
					                ↑ (expected)
					             """);
			}

			[Theory]
			[AutoData]
			public async Task ForGeneric_WhenInnerExceptionHasSuperType_ShouldFail(string message)
			{
				Action action = ()
					=> throw new OuterException(innerException: new CustomException(message));

				async Task Act()
					=> await That(action).ThrowsException().WithInner<SubCustomException>();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected action to
					              throw an exception with an inner SubCustomException,
					              but it was a CustomException:
					                {message}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForGeneric_WhenInnerExceptionHasWrongType_ShouldFail(string message)
			{
				Action action = ()
					=> throw new OuterException(innerException: new OtherException(message));

				async Task Act()
					=> await That(action).ThrowsException().WithInner<CustomException>();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected action to
					              throw an exception with an inner CustomException,
					              but it was an OtherException:
					                {message}
					              """);
			}

			[Fact]
			public async Task ForGeneric_WhenInnerExceptionIsNotPresent_ShouldFail()
			{
				Action action = () => throw new OuterException();

				async Task Act()
					=> await That(action).ThrowsException().WithInner<Exception>();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected action to
					             throw an exception with an inner exception,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task ForGeneric_WhenInnerExceptionIsPresent_ShouldSucceed()
			{
				Action action = () => throw new OuterException(innerException: new CustomException());

				async Task Act()
					=> await That(action).ThrowsException().WithInner<CustomException>();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ForGeneric_WhenInnerExceptionIsSubType_ShouldSucceed()
			{
				Action action = ()
					=> throw new OuterException(innerException: new SubCustomException());

				async Task Act()
					=> await That(action).ThrowsException().WithInner<CustomException>();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ForGeneric_WhenNoInnerExceptionIsPresent_ShouldFail()
			{
				Action action = () => throw new OuterException();

				async Task Act()
					=> await That(action).ThrowsException().WithInner<CustomException>();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected action to
					             throw an exception with an inner CustomException,
					             but it was <null>
					             """);
			}

			[Theory]
			[AutoData]
			public async Task ForType_WhenAwaited_WithExpectations_ShouldReturnThrownException(
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
			public async Task ForType_WhenAwaited_WithoutExpectations_ShouldReturnThrownException()
			{
				Exception exception = new OuterException(innerException: new CustomException());
				void Delegate() => throw exception;

				Exception result = await That(Delegate)
					.ThrowsException().WithInner(typeof(CustomException));

				await That(result).IsSameAs(exception);
			}

			[Fact(Skip="Temporarily skip until next Core update")]
			public async Task ForType_WhenInnerExceptionDoesNotMatchCriteria_ShouldFail()
			{
				string message = "bar";
				Action action = ()
					=> throw new OuterException(innerException: new CustomException(message));

				async Task Act()
					=> await That(action).ThrowsException()
						.WithInner(typeof(Exception), x => x.HasMessage("foo"));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected action to
					             throw an exception with an inner exception which should have Message equal to "foo",
					             but it was "bar" which differs at index 0:
					                ↓ (actual)
					               "bar"
					               "foo"
					                ↑ (expected)
					             """);
			}

			[Theory]
			[AutoData]
			public async Task ForType_WhenInnerExceptionHasSuperType_ShouldFail(string message)
			{
				Action action = ()
					=> throw new OuterException(innerException: new CustomException(message));

				async Task Act()
					=> await That(action).ThrowsException()
						.WithInner(typeof(SubCustomException));

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected action to
					              throw an exception with an inner SubCustomException,
					              but it was a CustomException:
					                {message}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForType_WhenInnerExceptionHasWrongType_ShouldFail(string message)
			{
				Action action = ()
					=> throw new OuterException(innerException: new OtherException(message));

				async Task Act()
					=> await That(action).ThrowsException().WithInner(typeof(CustomException));

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected action to
					              throw an exception with an inner CustomException,
					              but it was an OtherException:
					                {message}
					              """);
			}

			[Fact]
			public async Task ForType_WhenInnerExceptionIsNotPresent_ShouldFail()
			{
				Action action = () => throw new OuterException();

				async Task Act()
					=> await That(action).ThrowsException().WithInner(typeof(Exception));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected action to
					             throw an exception with an inner exception,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task ForType_WhenInnerExceptionIsPresent_ShouldSucceed()
			{
				Action action = () => throw new OuterException(innerException: new CustomException());

				async Task Act()
					=> await That(action).ThrowsException().WithInner(typeof(CustomException));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ForType_WhenInnerExceptionIsSubType_ShouldSucceed()
			{
				Action action = ()
					=> throw new OuterException(innerException: new SubCustomException());

				async Task Act()
					=> await That(action).ThrowsException().WithInner(typeof(CustomException));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ForType_WhenNoInnerExceptionIsPresent_ShouldFail()
			{
				Action action = () => throw new OuterException();

				async Task Act()
					=> await That(action).ThrowsException().WithInner(typeof(CustomException));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected action to
					             throw an exception with an inner CustomException,
					             but it was <null>
					             """);
			}
		}
	}
}
