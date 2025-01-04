namespace aweXpect.Tests.Guids;

public sealed partial class GuidShould
{
	public sealed class BeEmpty
	{
		public sealed class Tests
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
	}
}
