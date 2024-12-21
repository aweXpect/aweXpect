namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class DateTimeOffsetShould
{
	public sealed class HaveMonth
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int? expected = null;

				async Task Act()
					=> await That(subject).Should().HaveMonth(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have month of <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsDifferent_ShouldFail()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int? expected = 12;

				async Task Act()
					=> await That(subject).Should().HaveMonth(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have month of {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMonthOfSubjectIsTheSame_ShouldSucceed()
			{
				DateTimeOffset subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int expected = 11;

				async Task Act()
					=> await That(subject).Should().HaveMonth(expected);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
