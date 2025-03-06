namespace aweXpect.Tests;

public sealed partial class ThatNullableBool
{
	public sealed class IsNotFalse
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenFalse_ShouldFail()
			{
				bool? subject = false;

				async Task Act()
					=> await That(subject).IsNotFalse().Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not False, because we want to test the failure,
					             but it was
					             """);
			}

			[Theory]
			[InlineData(true)]
			[InlineData(null)]
			public async Task WhenTrueOrNull_ShouldSucceed(bool? subject)
			{
				async Task Act()
					=> await That(subject).IsNotFalse();

				await That(Act).DoesNotThrow();
			}
		}
	}
}
