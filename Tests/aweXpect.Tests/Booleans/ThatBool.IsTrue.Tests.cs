namespace aweXpect.Tests;

public sealed partial class ThatBool
{
	public sealed class IsTrue
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenFalse_ShouldFail()
			{
				bool subject = false;

				async Task Act()
					=> await That(subject).IsTrue();

				await That(Act).Throws<XunitException>()
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
					=> await That(subject).IsTrue().Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
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
					=> await That(subject).IsTrue();

				await That(Act).DoesNotThrow();
			}
		}
	}
}
