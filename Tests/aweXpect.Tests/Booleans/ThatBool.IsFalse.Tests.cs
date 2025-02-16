namespace aweXpect.Tests;

public sealed partial class ThatBool
{
	public sealed class IsFalse
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenFalse_ShouldSucceed()
			{
				bool subject = false;

				async Task Act()
					=> await That(subject).IsFalse();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenTrue_ShouldFail()
			{
				bool subject = true;

				async Task Act()
					=> await That(subject).IsFalse();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is False,
					             but it was True
					             """);
			}

			[Fact]
			public async Task WhenTrue_ShouldFailWithDescriptiveMessage()
			{
				bool subject = true;

				async Task Act()
					=> await That(subject).IsFalse().Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is False, because we want to test the failure,
					             but it was True
					             """);
			}
		}
	}
}
