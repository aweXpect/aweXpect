namespace aweXpect.Tests.ThatTests.Exceptions;

public sealed partial class ExceptionShould
{
	public class HaveParamNameTests
	{
		[Theory]
		[AutoData]
		public async Task WhenParamNameMatchesExpected_ShouldSucceed(string message)
		{
			ArgumentException subject = new(message, nameof(message));

			async Task Act()
				=> await That(subject).Should().HaveParamName("message");

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task WhenParamNameIsDifferent_ShouldFail(string message)
		{
			ArgumentException subject = new(message, nameof(message));

			async Task Act()
				=> await That(subject).Should().HaveParamName("somethingElse");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have ParamName "somethingElse",
				             but it had ParamName "message"
				             """);
		}
	}
}
