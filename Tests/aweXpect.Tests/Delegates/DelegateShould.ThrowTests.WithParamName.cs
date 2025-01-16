namespace aweXpect.Tests.Delegates;

public sealed partial class DelegateShould
{
	public sealed partial class Throw
	{
		public class WithParamNameTests
		{
			[Theory]
			[AutoData]
			public async Task WhenParamNameMatchesExpected_ShouldSucceed(string message)
			{
				ArgumentException exception = new(message, nameof(message));
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Does()
						.Throw<ArgumentException>().WithParamName("message");

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenParamNameIsDifferent_ShouldFail(string message)
			{
				ArgumentException exception = new(message, nameof(message));
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Does()
						.Throw<ArgumentException>().WithParamName("somethingElse");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected Delegate to
					             throw an ArgumentException with ParamName "somethingElse",
					             but it had ParamName "message"
					             """);
			}
		}
	}
}
