namespace aweXpect.Tests.Exceptions;

public sealed partial class ExceptionShould
{
	public sealed class HaveInnerException
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenInnerExceptionHasCorrectMessage_ShouldSucceed()
			{
				Exception subject = new("outer",
					new CustomException("inner"));

				async Task Act()
					=> await That(subject).Should()
						.HaveInnerException(e => e.HaveMessage("inner"));

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenInnerExceptionHasUnexpectedMessage_ShouldFail()
			{
				Exception subject = new("outer",
					new Exception("inner"));

				async Task Act()
					=> await That(subject).Should()
						.HaveInnerException(e => e.HaveMessage("some other message"));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have an inner exception which should have Message equal to "some other message",
					             but it was "inner" which differs at index 0:
					                ↓ (actual)
					               "inner"
					               "some other message"
					                ↑ (expected)
					             """);
			}

			[Fact]
			public async Task WhenInnerExceptionIsNotSet_ShouldFail()
			{
				Exception subject = new("outer");

				async Task Act()
					=> await That(subject).Should().HaveInnerException();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have an inner exception,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenInnerExceptionIsSet_ShouldSucceed()
			{
				Exception subject = new("outer",
					new Exception("inner"));

				async Task Act()
					=> await That(subject).Should().HaveInnerException();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Exception? subject = null;

				async Task Act()
					=> await That(subject).Should().HaveInnerException();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have an inner exception,
					             but it was <null>
					             """);
			}
		}
	}
}
