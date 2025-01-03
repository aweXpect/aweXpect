namespace aweXpect.Tests.Exceptions;

public sealed partial class ExceptionShould
{
	public sealed class HaveMessage
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task WhenStringsAreEqual_ShouldSucceed(string actual)
			{
				Exception subject = new(actual);

				async Task Act()
					=> await That(subject).Should().HaveMessage(actual);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenStringsDiffer_ShouldFail()
			{
				string actual = "actual text";
				string expected = "expected other text";
				Exception subject = new(actual);

				async Task Act()
					=> await That(subject).Should().HaveMessage(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have Message equal to "expected other text",
					             but it was "actual text" which differs at index 0:
					                ↓ (actual)
					               "actual text"
					               "expected other text"
					                ↑ (expected)
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Exception? subject = null;

				async Task Act()
					=> await That(subject).Should().HaveMessage("expected text");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have Message equal to "expected text",
					             but it was <null>
					             """);
			}
		}
	}
}
