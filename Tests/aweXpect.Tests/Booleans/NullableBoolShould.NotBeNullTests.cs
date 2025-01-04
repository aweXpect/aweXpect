namespace aweXpect.Tests.Booleans;

public sealed partial class NullableBoolShould
{
	public sealed class NotBeNull
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenNull_ShouldFail()
			{
				bool? subject = null;

				async Task Act()
					=> await That(subject).Should().NotBeNull().Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be <null>, because we want to test the failure,
					             but it was <null>
					             """);
			}

			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			public async Task WhenTrueOrFalse_ShouldSucceed(bool? subject)
			{
				async Task Act()
					=> await That(subject).Should().NotBeNull();

				await That(Act).Should().NotThrow();
			}
		}
	}
}
