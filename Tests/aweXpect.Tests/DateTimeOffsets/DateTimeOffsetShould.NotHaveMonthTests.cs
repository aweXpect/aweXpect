namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class DateTimeOffsetShould
{
	public sealed class NotHaveMonth
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenMonthOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int? unexpected = 12;

				async Task Act()
					=> await That(subject).Should().NotHaveMonth(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsTheSame_ShouldFail()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int unexpected = 11;

				async Task Act()
					=> await That(subject).Should().NotHaveMonth(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have month of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotHaveMonth(unexpected);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
