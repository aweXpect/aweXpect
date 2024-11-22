namespace Testably.Expectations.Tests.ThatTests.Exceptions;

public sealed partial class ExceptionShould
{
	public sealed class HaveMessageTests
	{
		[Fact]
		public async Task FailsForDifferentStrings()
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

		[Theory]
		[AutoData]
		public async Task SucceedsForSameStrings(string actual)
		{
			Exception subject = new(actual);

			async Task Act()
				=> await That(subject).Should().HaveMessage(actual);

			await That(Act).Should().NotThrow();
		}
	}
}
