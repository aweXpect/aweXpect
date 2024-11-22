namespace Testably.Expectations.Tests.ThatTests.DateTimes;

public sealed partial class DateTimeShould
{
	public sealed class HaveHourTests
	{
		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().HaveHour(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have hour of <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenHourOfSubjectIsDifferent_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? expected = 12;

			async Task Act()
				=> await That(subject).Should().HaveHour(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have hour of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenHourOfSubjectIsTheSame_ShouldSucceed()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int expected = 13;

			async Task Act()
				=> await That(subject).Should().HaveHour(expected);

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotHaveHourTests
	{
		[Fact]
		public async Task WhenHourOfSubjectIsDifferent_ShouldSucceed()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? unexpected = 12;

			async Task Act()
				=> await That(subject).Should().NotHaveHour(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenHourOfSubjectIsTheSame_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
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
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveHour(unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
