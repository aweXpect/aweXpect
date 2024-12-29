namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class DateTimeOffsetShould
{
	public sealed class NotHaveHour
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenHourOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, 2.Hours());
				int? unexpected = 12;

				async Task Act()
					=> await That(subject).Should().NotHaveHour(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenHourOfSubjectIsTheSame_ShouldFail()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, 2.Hours());
				int unexpected = 13;

				async Task Act()
					=> await That(subject).Should().NotHaveHour(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have hour of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, 2.Hours());
				int? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotHaveHour(unexpected);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
