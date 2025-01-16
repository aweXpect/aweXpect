namespace aweXpect.Tests.Guids;

public sealed partial class NullableGuidShould
{
	public sealed class Be
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreNull_ShouldSucceed()
			{
				Guid? subject = null;

				async Task Act()
					=> await That(subject).Should().Be(null);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsDifferent_ShouldFail()
			{
				Guid? subject = FixedGuid();
				Guid? expected = OtherGuid();

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
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Guid? subject = null;

				async Task Act()
					=> await That(subject).Should().Be(FixedGuid());

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be {FixedGuid()},
					              but it was <null>
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsTheSame_ShouldSucceed()
			{
				Guid? subject = FixedGuid();
				Guid? expected = subject;

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
