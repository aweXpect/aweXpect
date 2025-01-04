namespace aweXpect.Tests.Delegates;

public sealed partial class DelegateShould
{
	public sealed class NotThrow
	{
		public sealed class ActionTests
		{
			[Fact]
			public async Task WhenDelegateDoesNotThrow_ShouldSucceed()
			{
				Action @delegate = () => { };

				async Task Act()
					=> await That(@delegate).Should().NotThrow();

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenDelegateThrows_ShouldFail(string message)
			{
				Exception exception = new CustomException(message);
				Action @delegate = () => throw exception;

				async Task Act()
					=> await That(@delegate).Should().NotThrow();

				await That(Act).Should().ThrowException()
					.WithMessage($"""
					              Expected @delegate to
					              not throw any exception,
					              but it did throw a CustomException:
					                {message}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Action? subject = null;

				async Task Act()
					=> await That(subject!).Should().NotThrow();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not throw any exception,
					             but it was <null>
					             """);
			}
		}

		public sealed class FuncValueTests
		{
			[Theory]
			[AutoData]
			public async Task WhenAwaited_ShouldReturnResultFromDelegate(int value)
			{
				Func<int> @delegate = () => value;

				int result = await That(@delegate).Should().NotThrow();

				await That(result).Should().Be(value);
			}

			[Fact]
			public async Task WhenDelegateDoesNotThrow_ShouldSucceed()
			{
				Func<int> @delegate = () => 1;

				async Task Act()
					=> await That(@delegate).Should().NotThrow();

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenDelegateThrows_ShouldFail(string message)
			{
				Exception exception = new CustomException(message);
				Func<int> @delegate = () => throw exception;

				async Task Act()
					=> await That(@delegate).Should().NotThrow();

				await That(Act).Should().ThrowException()
					.WithMessage($"""
					              Expected @delegate to
					              not throw any exception,
					              but it did throw a CustomException:
					                {message}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().NotThrow();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not throw any exception,
					             but it was <null>
					             """);
			}
		}
	}
}
