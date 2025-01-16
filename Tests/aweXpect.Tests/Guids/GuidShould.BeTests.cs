namespace aweXpect.Tests.Guids;

public sealed partial class GuidShould
{
	public sealed class Be
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				Guid subject = FixedGuid();
				Guid? expected = null;

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsDifferent_ShouldFail()
			{
				Guid subject = FixedGuid();
				Guid expected = OtherGuid();

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsTheSame_ShouldSucceed()
			{
				Guid subject = FixedGuid();
				Guid expected = subject;

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
