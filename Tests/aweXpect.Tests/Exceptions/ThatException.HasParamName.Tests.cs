namespace aweXpect.Tests;

public sealed partial class ThatException
{
	public class HasParamName
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task WhenParamNameIsDifferent_ShouldFail(string message)
			{
				ArgumentException subject = new(message, nameof(message));

				async Task Act()
					=> await That(subject).HasParamName("somethingElse");

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).HasParamName("message");

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				ArgumentException? subject = null;

				async Task Act()
					=> await That(subject).HasParamName("message");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have ParamName "message",
					             but it was <null>
					             """);
			}
		}
	}
}
