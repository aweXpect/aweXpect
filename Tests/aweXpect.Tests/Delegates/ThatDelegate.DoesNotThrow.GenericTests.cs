﻿namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class DoesNotThrow
	{
		public sealed class ActionGenericTests
		{
			[Fact]
			public async Task WhenDelegateDoesNotThrow_ShouldSucceed()
			{
				Action @delegate = () => { };

				async Task Act()
					=> await That(@delegate).DoesNotThrow<CustomException>();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenDelegateThrowsMatchingException_ShouldFail(string message)
			{
				Exception exception = new CustomException(message);
				Action @delegate = () => throw exception;

				async Task Act()
					=> await That(@delegate).DoesNotThrow<CustomException>();

				await That(Act).ThrowsException()
					.WithMessage($"""
					              Expected that @delegate
					              does not throw a ThatDelegate.CustomException,
					              but it did throw a ThatDelegate.CustomException:
					                {message}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenDelegateThrowsOtherException_ShouldFail(string message)
			{
				Exception exception = new OtherException(message);
				Action @delegate = () => throw exception;

				async Task Act()
					=> await That(@delegate).DoesNotThrow<CustomException>();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenDelegateThrowsSubtypeOfException_ShouldFail(string message)
			{
				Exception exception = new SubCustomException(message);
				Action @delegate = () => throw exception;

				async Task Act()
					=> await That(@delegate).DoesNotThrow<CustomException>();

				await That(Act).ThrowsException()
					.WithMessage($"""
					              Expected that @delegate
					              does not throw a ThatDelegate.CustomException,
					              but it did throw a ThatDelegate.SubCustomException:
					                {message}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenDelegateThrowsSuperTypeOfException_ShouldSucceed(string message)
			{
				Exception exception = new CustomException(message);
				Action @delegate = () => throw exception;

				async Task Act()
					=> await That(@delegate).DoesNotThrow<SubCustomException>();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Action? subject = null;

				async Task Act()
					=> await That(subject!).DoesNotThrow<CustomException>();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not throw a ThatDelegate.CustomException,
					             but it was <null>
					             """);
			}
		}
		
		public sealed class FuncValueGenericTests
		{
			[Theory]
			[AutoData]
			public async Task WhenAwaited_ShouldReturnResultFromDelegate(int value)
			{
				Func<int> @delegate = () => value;

				int result = await That(@delegate).DoesNotThrow<CustomException>();

				await That(result).IsEqualTo(value);
			}

			[Fact]
			public async Task WhenDelegateDoesNotThrow_ShouldSucceed()
			{
				Func<int> @delegate = () => 1;

				async Task Act()
					=> await That(@delegate).DoesNotThrow<CustomException>();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenDelegateThrowsMatchingException_ShouldFail(string message)
			{
				Exception exception = new CustomException(message);
				Func<int> @delegate = () => throw exception;

				async Task Act()
					=> await That(@delegate).DoesNotThrow<CustomException>();

				await That(Act).ThrowsException()
					.WithMessage($"""
					              Expected that @delegate
					              does not throw a ThatDelegate.CustomException,
					              but it did throw a ThatDelegate.CustomException:
					                {message}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenDelegateThrowsOtherException_ShouldFail(string message)
			{
				Exception exception = new OtherException(message);
				Func<int> @delegate = () => throw exception;

				async Task Act()
					=> await That(@delegate).DoesNotThrow<CustomException>();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenDelegateThrowsSubtypeOfException_ShouldFail(string message)
			{
				Exception exception = new SubCustomException(message);
				Func<int> @delegate = () => throw exception;

				async Task Act()
					=> await That(@delegate).DoesNotThrow<CustomException>();

				await That(Act).ThrowsException()
					.WithMessage($"""
					              Expected that @delegate
					              does not throw a ThatDelegate.CustomException,
					              but it did throw a ThatDelegate.SubCustomException:
					                {message}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenDelegateThrowsSuperTypeOfException_ShouldSucceed(string message)
			{
				Exception exception = new CustomException(message);
				Func<int> @delegate = () => throw exception;

				async Task Act()
					=> await That(@delegate).DoesNotThrow<SubCustomException>();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<int>? subject = null;

				async Task Act()
					=> await That(subject!).DoesNotThrow<CustomException>();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not throw a ThatDelegate.CustomException,
					             but it was <null>
					             """);
			}
		}
	}
}
