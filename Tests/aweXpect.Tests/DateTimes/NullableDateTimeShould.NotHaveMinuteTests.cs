namespace aweXpect.Tests.DateTimes;

public sealed partial class NullableDateTimeShould
{
	public sealed class NotHaveMinute
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenMinuteOfSubjectIsDifferent_ShouldSucceed()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = 13;

				async Task Act()
					=> await That(subject).Should().NotHaveMinute(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenMinuteOfSubjectIsTheSame_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int unexpected = 14;

				async Task Act()
					=> await That(subject).Should().NotHaveMinute(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have minute of {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndUnexpectedIsNull_ShouldSucceed()
			{
				DateTime? subject = null;
				int? expected = null;

				async Task Act()
					=> await That(subject).Should().NotHaveMinute(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				DateTime? subject = null;
				int? expected = 1;

				async Task Act()
					=> await That(subject).Should().NotHaveMinute(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotHaveMinute(unexpected);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
