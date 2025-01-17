namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public sealed class IsNotNull
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActualIsEmpty_ShouldSucceed()
			{
				string subject = "";

				async Task Act()
					=> await That(subject).IsNotNull();

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenActualIsNotNull_ShouldSucceed(string? subject)
			{
				async Task Act()
					=> await That(subject).IsNotNull();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenActualIsNull_ShouldFail()
			{
				string? subject = null;

				async Task Act()
					=> await That(subject).IsNotNull();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be null,
					             but it was
					             """);
			}
		}
	}
}
