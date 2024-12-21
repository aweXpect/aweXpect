namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class NullableDateTimeOffsetShould
{
	public sealed class NotHaveYear
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndUnexpectedIsNull_ShouldSucceed()
			{
				DateTimeOffset? subject = null;
				int? expected = null;

				async Task Act()
					=> await That(subject).Should().NotHaveYear(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				DateTimeOffset? subject = null;
				int? expected = 1;

				async Task Act()
					=> await That(subject).Should().NotHaveYear(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotHaveYear(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenYearOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int? unexpected = 2011;

				async Task Act()
					=> await That(subject).Should().NotHaveYear(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenYearOfSubjectIsTheSame_ShouldFail()
			{
				DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
				int unexpected = 2010;

				async Task Act()
					=> await That(subject).Should().NotHaveYear(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have year of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
