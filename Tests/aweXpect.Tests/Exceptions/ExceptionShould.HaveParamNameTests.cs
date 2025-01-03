namespace aweXpect.Tests.Exceptions;

public sealed partial class ExceptionShould
{
	public class HaveParamName
	{
		public sealed class Tests
		{
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

			[Theory]
			[AutoData]
			public async Task WhenParamNameMatchesExpected_ShouldSucceed(string message)
			{
				ArgumentException subject = new(message, nameof(message));

				async Task Act()
					=> await That(subject).Should().HaveParamName("message");

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				ArgumentException? subject = null;

				async Task Act()
					=> await That(subject).Should().HaveParamName("message");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have ParamName "message",
					             but it was <null>
					             """);
			}
		}
	}
}
