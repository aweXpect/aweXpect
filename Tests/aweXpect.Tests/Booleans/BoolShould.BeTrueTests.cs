namespace aweXpect.Tests.Booleans;

public sealed partial class BoolShould
{
	public sealed class BeTrue
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenFalse_ShouldFail()
			{
				bool subject = false;

				async Task Act()
					=> await That(subject).Should().BeTrue();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be True,
					             but it was False
					             """);
			}

			[Fact]
			public async Task WhenFalse_ShouldFailWithDescriptiveMessage()
			{
				bool subject = false;

				async Task Act()
					=> await That(subject).Should().BeTrue().Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be True, because we want to test the failure,
					             but it was False
					             """);
			}

			[Fact]
			public async Task WhenTrue_ShouldSucceed()
			{
				bool subject = true;

				async Task Act()
					=> await That(subject).Should().BeTrue();

				await That(Act).Does().NotThrow();
			}
		}
	}
}
