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

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has ParamName "somethingElse",
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

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				ArgumentException? subject = null;

				async Task Act()
					=> await That(subject).HasParamName("message");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has ParamName "message",
					             but it was <null>
					             """);
			}
		}
	}
}
