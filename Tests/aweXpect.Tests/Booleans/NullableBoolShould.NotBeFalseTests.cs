namespace aweXpect.Tests.Booleans;

public sealed partial class NullableBoolShould
{
	public sealed class NotBeFalse
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenFalse_ShouldFail()
			{
				bool? subject = false;

				async Task Act()
					=> await That(subject).Should().NotBeFalse().Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be False, because we want to test the failure,
					             but it was False
					             """);
			}

			[Theory]
			[InlineData(true)]
			[InlineData(null)]
			public async Task WhenTrueOrNull_ShouldSucceed(bool? subject)
			{
				async Task Act()
					=> await That(subject).Should().NotBeFalse();

				await That(Act).Does().NotThrow();
			}
		}
	}
}
