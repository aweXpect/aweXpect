namespace Testably.Expectations.Tests.ThatTests.Delegates;

public sealed partial class DelegateThrows
{
	public sealed class WithInnerTests
	{
		[Theory]
		[AutoData]
		public async Task ForGeneric_WhenAwaited_WithExpectations_ShouldReturnThrownException(
			string message)
		{
			Exception exception = new OuterException(innerException: new CustomException(message));

			Exception result = await That(() => throw exception)
				.Should().ThrowException().WithInner<CustomException>(
					e => e.HaveMessage(message));

			await That(result).Should().BeSameAs(exception);
		}

		[Fact]
		public async Task ForGeneric_WhenAwaited_WithoutExpectations_ShouldReturnThrownException()
		{
			Exception exception = new OuterException(innerException: new CustomException());

			Exception result = await That(() => throw exception)
				.Should().ThrowException().WithInner<CustomException>();

			await That(result).Should().BeSameAs(exception);
		}

		[Theory]
		[AutoData]
		public async Task ForGeneric_WhenInnerExceptionHasSuperType_ShouldFail(string message)
		{
			Action action = ()
				=> throw new OuterException(innerException: new CustomException(message));

			async Task Act()
				=> await That(action).Should().ThrowException().WithInner<SubCustomException>();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected action to
				              throw an Exception with an inner SubCustomException,
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
				=> await That(action).Should().ThrowException().WithInner<CustomException>();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected action to
				              throw an Exception with an inner CustomException,
				              but it was an OtherException:
				                {message}
				              """);
		}

		[Fact]
		public async Task ForGeneric_WhenInnerExceptionIsPresent_ShouldSucceed()
		{
			Action action = () => throw new OuterException(innerException: new CustomException());

			async Task Act()
				=> await That(action).Should().ThrowException().WithInner<CustomException>();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForGeneric_WhenInnerExceptionIsSubType_ShouldSucceed()
		{
			Action action = ()
				=> throw new OuterException(innerException: new SubCustomException());

			async Task Act()
				=> await That(action).Should().ThrowException().WithInner<CustomException>();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForGeneric_WhenNoInnerExceptionIsPresent_ShouldFail()
		{
			Action action = () => throw new OuterException();

			async Task Act()
				=> await That(action).Should().ThrowException().WithInner<CustomException>();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected action to
				             throw an Exception with an inner CustomException,
				             but it was <null>
				             """);
		}

		[Theory]
		[AutoData]
		public async Task ForType_WhenAwaited_WithExpectations_ShouldReturnThrownException(
			string message)
		{
			Exception exception = new OuterException(innerException: new CustomException(message));

			Exception result = await That(() => throw exception)
				.Should().ThrowException().WithInner(typeof(CustomException),
					e => e.HaveMessage(message));

			await That(result).Should().BeSameAs(exception);
		}

		[Fact]
		public async Task ForType_WhenAwaited_WithoutExpectations_ShouldReturnThrownException()
		{
			Exception exception = new OuterException(innerException: new CustomException());

			Exception result = await That(() => throw exception)
				.Should().ThrowException().WithInner(typeof(CustomException));

			await That(result).Should().BeSameAs(exception);
		}

		[Theory]
		[AutoData]
		public async Task ForType_WhenInnerExceptionHasSuperType_ShouldFail(string message)
		{
			Action action = ()
				=> throw new OuterException(innerException: new CustomException(message));

			async Task Act()
				=> await That(action).Should().ThrowException()
					.WithInner(typeof(SubCustomException));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected action to
				              throw an Exception with an inner SubCustomException,
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
				=> await That(action).Should().ThrowException().WithInner(typeof(CustomException));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected action to
				              throw an Exception with an inner CustomException,
				              but it was an OtherException:
				                {message}
				              """);
		}

		[Fact]
		public async Task ForType_WhenInnerExceptionIsPresent_ShouldSucceed()
		{
			Action action = () => throw new OuterException(innerException: new CustomException());

			async Task Act()
				=> await That(action).Should().ThrowException().WithInner(typeof(CustomException));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForType_WhenInnerExceptionIsSubType_ShouldSucceed()
		{
			Action action = ()
				=> throw new OuterException(innerException: new SubCustomException());

			async Task Act()
				=> await That(action).Should().ThrowException().WithInner(typeof(CustomException));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForType_WhenNoInnerExceptionIsPresent_ShouldFail()
		{
			Action action = () => throw new OuterException();

			async Task Act()
				=> await That(action).Should().ThrowException().WithInner(typeof(CustomException));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected action to
				             throw an Exception with an inner CustomException,
				             but it was <null>
				             """);
		}
	}
}
