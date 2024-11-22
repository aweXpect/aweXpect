namespace Testably.Expectations.Tests.ThatTests.Strings;

public sealed partial class StringShould
{
	public class BeTests
	{
		[Theory]
		[InlineData("some message", "*me me*", true)]
		[InlineData("some message", "*ME ME*", false)]
		[InlineData("some message", "some?message", true)]
		[InlineData("some message", "some*message", true)]
		[InlineData("some message", "some me?age", false)]
		[InlineData("some message", "some me??age", true)]
		public async Task AsWildcard_ShouldDefaultToCaseSensitiveMatch(
			string subject, string pattern, bool expectMatch)
		{
			async Task Act()
				=> await That(subject).Should().Be(pattern).AsWildcard();

			await That(Act).Should().ThrowException().OnlyIf(!expectMatch);
		}

		[Theory]
		[AutoData]
		public async Task WhenStringsAreTheSame_ShouldSucceed(string subject)
		{
			string expected = subject;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await Act();
		}

		[Fact]
		public async Task WhenStringsDiffer_ShouldFail()
		{
			string subject = "actual text";
			string expected = "expected other text";

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be equal to "expected other text",
				             but it was "actual text" which differs at index 0:
				                ↓ (actual)
				               "actual text"
				               "expected other text"
				                ↑ (expected)
				             """);
		}
	}
}
