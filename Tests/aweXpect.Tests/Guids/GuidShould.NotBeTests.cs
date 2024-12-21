namespace aweXpect.Tests.Guids;

public sealed partial class GuidShould
{
	public sealed class NotBe
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsDifferent_ShouldSucceed()
			{
				Guid subject = FixedGuid();
				Guid unexpected = OtherGuid();

				async Task Act()
					=> await That(subject).Should().NotBe(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsTheSame_ShouldFail()
			{
				Guid subject = FixedGuid();
				Guid unexpected = subject;

				async Task Act()
					=> await That(subject).Should().NotBe(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				Guid subject = FixedGuid();
				Guid? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotBe(unexpected);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
