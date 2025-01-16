namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class NullableDateTimeOffsetShould
{
	public sealed class Be
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsDifferent_ShouldFail()
			{
				DateTimeOffset? subject = CurrentTime();
				DateTimeOffset? expected = LaterTime();

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
				DateTimeOffset? subject = CurrentTime();
				DateTimeOffset? expected = subject;

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Does().NotThrow();
			}
		}
	}
}
