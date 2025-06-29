namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class ThrowsException
	{
		public sealed class Within
		{
			public sealed class Tests
			{
				[Fact]
				public async Task ShouldSupportChainedConstraints()
				{
					Action action = () => { };

					async Task Act()
						=> await That(action).ThrowsException().Within(5.Seconds()).WithMessage("foo");

					await That(Act).ThrowsException()
						.WithMessage("""
						             Expected that action
						             throws an exception within 0:05 with Message equal to "foo",
						             but it did not throw any exception
						             """);
				}

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
						await That(action).ThrowsException().Within(5.Seconds());

					await That(result).IsSameAs(exception);
				}

				[Fact]
				public async Task WhenExceptionTypeIsThrownInTime_ShouldSucceed()
				{
					Exception exception = new CustomException();
					Action action = () => throw exception;

					async Task<Exception> Act()
						=> await That(action).ThrowsException().Within(5.Seconds());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenExceptionIsThrownTooLate_ShouldFail()
				{
					Exception exception = new CustomException();
					Action action = () =>
					{
						Task.Delay(50.Milliseconds()).Wait();
						throw exception;
					};

					async Task<Exception> Act()
						=> await That(action).ThrowsException().Within(5.Milliseconds());

					await That(Act).ThrowsException()
						.WithMessage("""
						             Expected that action
						             throws an exception within 0:00.005,
						             but it took *
						             """).AsWildcard();
				}

				[Fact]
				public async Task WhenNoExceptionIsThrownAndExecutionTimeIsTooLarge_ShouldFail()
				{
					Action action = () =>
					{
						Task.Delay(50.Milliseconds()).Wait();
					};

					async Task<Exception> Act()
						=> await That(action).ThrowsException().Within(5.Milliseconds());

					await That(Act).ThrowsException()
						.WithMessage("""
						             Expected that action
						             throws an exception within 0:00.005,
						             but it took *
						             """).AsWildcard();
				}

				[Fact]
				public async Task WhenNoExceptionIsThrownInTime_ShouldFail()
				{
					Action action = () => { };

					async Task<Exception> Act()
						=> await That(action).ThrowsException().Within(5.Seconds()).Because("it should");

					await That(Act).ThrowsException()
						.WithMessage("""
						             Expected that action
						             throws an exception within 0:05, because it should,
						             but it did not throw any exception
						             """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					Action? subject = null;

					async Task Act()
						=> await That(subject!).ThrowsException().Within(5.Seconds());

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             throws an exception within 0:05,
						             but it was <null>
						             """);
				}
			}
		}
	}
}
