namespace aweXpect.Tests;

public sealed partial class NullableBoolShould
{
	public sealed class NotBeTrue
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(false)]
			[InlineData(null)]
			public async Task WhenFalseOrNull_ShouldFail(bool? subject)
			{
				async Task Act()
					=> await That(subject).Should().NotBeTrue();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenTrue_ShouldFail()
			{
				bool? subject = true;

				async Task Act()
					=> await That(subject).Should().NotBeTrue().Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be True, because we want to test the failure,
					             but it was True
					             """);
			}
		}
	}
}
