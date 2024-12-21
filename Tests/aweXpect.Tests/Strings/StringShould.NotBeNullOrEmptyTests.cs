namespace aweXpect.Tests.Strings;

public sealed partial class StringShould
{
	public sealed class NotBeNullOrEmpty
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActualIsEmpty_ShouldFail()
			{
				string subject = "";

				async Task Act()
					=> await That(subject).Should().NotBeNullOrEmpty();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be null or empty,
					             but it was ""
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenActualIsNotEmpty_ShouldSucceed(string? subject)
			{
				async Task Act()
					=> await That(subject).Should().NotBeNullOrEmpty();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenActualIsNull_ShouldSucceed()
			{
				string? subject = null;

				async Task Act()
					=> await That(subject).Should().NotBeNullOrEmpty();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be null or empty,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenActualIsWhitespace_ShouldSucceed()
			{
				string subject = " \t ";

				async Task Act()
					=> await That(subject).Should().NotBeNullOrEmpty();

				await That(Act).Should().NotThrow();
			}
		}
	}
}
