namespace aweXpect.Tests.Strings;

public sealed partial class StringShould
{
	public sealed class NotBeNull
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActualIsEmpty_ShouldSucceed()
			{
				string subject = "";

				async Task Act()
					=> await That(subject).Should().NotBeNull();

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenActualIsNotNull_ShouldSucceed(string? subject)
			{
				async Task Act()
					=> await That(subject).Should().NotBeNull();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenActualIsNull_ShouldFail()
			{
				string? subject = null;

				async Task Act()
					=> await That(subject).Should().NotBeNull();

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
