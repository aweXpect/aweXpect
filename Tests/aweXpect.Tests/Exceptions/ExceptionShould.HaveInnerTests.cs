namespace aweXpect.Tests.Exceptions;

public sealed partial class ExceptionShould
{
	public sealed class HaveInner
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenInnerExceptionHasCorrectMessageButUnexpectedType_ShouldFail()
			{
				Exception subject = new("outer", new Exception("inner"));

				async Task Act()
					=> await That(subject).Should()
						.HaveInner<CustomException>(e => e.HaveMessage("inner"));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have an inner CustomException which should have Message equal to "inner",
					             but it was an Exception:
					               inner
					             """);
			}

			[Fact]
			public async Task WhenInnerExceptionHasCorrectTypeAndMessage_ShouldSucceed()
			{
				Exception subject = new("outer",
					new CustomException("inner"));

				async Task Act()
					=> await That(subject).Should()
						.HaveInner<CustomException>(e => e.HaveMessage("inner"));

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenInnerExceptionHasCorrectTypeButUnexpectedMessage_ShouldFail()
			{
				Exception subject = new("outer",
					new CustomException("inner"));

				async Task Act()
					=> await That(subject).Should()
						.HaveInner<CustomException>(e => e.HaveMessage("some other message"));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have an inner CustomException which should have Message equal to "some other message",
					             but it was "inner" which differs at index 0:
					                ↓ (actual)
					               "inner"
					               "some other message"
					                ↑ (expected)
					             """);
			}

			[Fact]
			public async Task WhenInnerExceptionIsNotOfTheExpectedType_ShouldFail()
			{
				Exception subject = new("outer",
					new Exception("inner"));

				async Task Act()
					=> await That(subject).Should().HaveInner<CustomException>();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have an inner CustomException,
					             but it was an Exception:
					               inner
					             """);
			}


			[Fact]
			public async Task WhenInnerExceptionMeetsType_ShouldSucceed()
			{
				Exception subject = new("outer",
					new CustomException("inner"));

				async Task Act()
					=> await That(subject).Should().HaveInner<CustomException>();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Exception? subject = null;

				async Task Act()
					=> await That(subject).Should().HaveInner<CustomException>();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have an inner CustomException,
					             but it was <null>
					             """);
			}
		}
	}
}
