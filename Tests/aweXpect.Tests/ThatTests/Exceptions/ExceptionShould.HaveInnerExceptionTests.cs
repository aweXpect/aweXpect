namespace Testably.Expectations.Tests.ThatTests.Exceptions;

public sealed partial class ExceptionShould
{
	public class HaveInnerExceptionTests
	{
		[Fact]
		public async Task FailsWhenInnerExceptionHasCorrectMessageButUnexpectedType()
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
		public async Task FailsWhenInnerExceptionHasCorrectTypeButUnexpectedMessage()
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
		public async Task FailsWhenInnerExceptionHasUnexpectedMessage()
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
		public async Task FailsWhenInnerExceptionIsNotOfTheExpectedType()
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
		public async Task FailsWhenInnerExceptionIsNotSet()
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
		public async Task SucceedsWhenInnerExceptionHasCorrectMessage()
		{
			Exception subject = new("outer",
				new CustomException("inner"));

			async Task Act()
				=> await That(subject).Should()
					.HaveInnerException(e => e.HaveMessage("inner"));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SucceedsWhenInnerExceptionHasCorrectTypeAndMessage()
		{
			Exception subject = new("outer",
				new CustomException("inner"));

			async Task Act()
				=> await That(subject).Should()
					.HaveInner<CustomException>(e => e.HaveMessage("inner"));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SucceedsWhenInnerExceptionIsSet()
		{
			Exception subject = new("outer",
				new Exception("inner"));

			async Task Act()
				=> await That(subject).Should().HaveInnerException();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SucceedsWhenInnerExceptionMeetsType()
		{
			Exception subject = new("outer",
				new CustomException("inner"));

			async Task Act()
				=> await That(subject).Should().HaveInner<CustomException>();

			await That(Act).Should().NotThrow();
		}
	}
}
