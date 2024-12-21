namespace aweXpect.Tests.Guids;

public sealed partial class GuidShould
{
	public sealed class NotBeEmpty
	{
		public sealed class Tests
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
}
