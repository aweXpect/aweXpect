namespace Testably.Expectations.Tests.ThatTests.Guids;

public sealed partial class GuidShould
{
	public sealed class BeEmptyTests
	{
		[Fact]
		public async Task WhenSubjectIsEmpty_ShouldSucceed()
		{
			Guid subject = Guid.Empty;

			async Task Act()
				=> await That(subject).Should().BeEmpty();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNotEmpty_ShouldFail()
		{
			Guid subject = OtherGuid();

			async Task Act()
				=> await That(subject).Should().BeEmpty();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be empty,
				              but it was {Formatter.Format(subject)}
				              """);
		}
	}

	public sealed class NotBeEmptyTests
	{
		[Fact]
		public async Task WhenSubjectIsEmpty_ShouldFail()
		{
			Guid subject = Guid.Empty;

			async Task Act()
				=> await That(subject).Should().NotBeEmpty();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be empty,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsNotEmpty_ShouldSucceed()
		{
			Guid subject = OtherGuid();

			async Task Act()
				=> await That(subject).Should().NotBeEmpty();

			await That(Act).Should().NotThrow();
		}
	}
}
