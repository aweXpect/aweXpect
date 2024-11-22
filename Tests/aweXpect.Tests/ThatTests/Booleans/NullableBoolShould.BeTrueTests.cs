namespace Testably.Expectations.Tests.ThatTests.Booleans;

public sealed partial class NullableBoolShould
{
	public sealed class BeTrueTests
	{
		[Theory]
		[InlineData(false)]
		[InlineData(null)]
		public async Task WhenFalseOrNull_ShouldFail(bool? subject)
		{
			async Task Act()
				=> await That(subject).Should().BeTrue().Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be True, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenTrue_ShouldSucceed()
		{
			bool? subject = true;

			async Task Act()
				=> await That(subject).Should().BeTrue();

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeTrueTests
	{
		[Theory]
		[InlineData(false)]
		[InlineData(null)]
		public async Task WhenFalseOrNull_ShouldFail(bool? subject)
		{
			async Task Act()
				=> await That(subject).Should().NotBeTrue();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenTrue_ShouldFail()
		{
			bool? subject = true;

			async Task Act()
				=> await That(subject).Should().NotBeTrue().Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be True, because we want to test the failure,
				             but it was True
				             """);
		}
	}
}
