namespace Testably.Expectations.Tests.ThatTests.Booleans;

public sealed partial class NullableBoolShould
{
	public sealed class BeFalseTests
	{
		[Fact]
		public async Task WhenFalse_ShouldSucceed()
		{
			bool? subject = false;

			async Task Act()
				=> await That(subject).Should().BeFalse();

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(true)]
		[InlineData(null)]
		public async Task WhenTrueOrNull_ShouldFail(bool? subject)
		{
			async Task Act()
				=> await That(subject).Should().BeFalse().Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be False, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}
	}

	public sealed class NotBeFalseTests
	{
		[Fact]
		public async Task WhenFalse_ShouldFail()
		{
			bool? subject = false;

			async Task Act()
				=> await That(subject).Should().NotBeFalse().Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
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

			await That(Act).Should().NotThrow();
		}
	}
}
