namespace aweXpect.Tests;

public sealed partial class ThatException
{
	public sealed class HasInner
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenInnerExceptionHasCorrectMessageButUnexpectedType_ShouldFail()
			{
				Exception subject = new("outer", new Exception("inner"));

				async Task Act()
					=> await That(subject).HasInner<CustomException>(e => e.HasMessage("inner"));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has an inner CustomException whose Message is equal to "inner",
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
					=> await That(subject).HasInner<CustomException>(e => e.HasMessage("inner"));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenInnerExceptionHasCorrectTypeButUnexpectedMessage_ShouldFail()
			{
				Exception subject = new("outer",
					new CustomException("inner"));

				async Task Act()
					=> await That(subject).HasInner<CustomException>(e => e.HasMessage("some other message"));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has an inner CustomException whose Message is equal to "some other message",
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
					=> await That(subject).HasInner<CustomException>();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has an inner CustomException,
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
					=> await That(subject).HasInner<CustomException>();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Exception? subject = null;

				async Task Act()
					=> await That(subject).HasInner<CustomException>();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has an inner CustomException,
					             but it was <null>
					             """);
			}
		}
	}
}
