namespace Testably.Expectations.Tests.ThatTests.DateTimes;

public sealed partial class DateTimeShould
{
	public sealed class HaveDayTests
	{
		[Fact]
		public async Task WhenDayOfSubjectIsDifferent_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? expected = 11;

			async Task Act()
				=> await That(subject).Should().HaveDay(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have day of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenDayOfSubjectIsTheSame_ShouldSucceed()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int expected = 12;

			async Task Act()
				=> await That(subject).Should().HaveDay(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().HaveDay(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have day of <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}
	}

	public sealed class NotHaveDayTests
	{
		[Fact]
		public async Task WhenDayOfSubjectIsDifferent_ShouldSucceed()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? unexpected = 11;

			async Task Act()
				=> await That(subject).Should().NotHaveDay(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenDayOfSubjectIsTheSame_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int unexpected = 12;

			async Task Act()
				=> await That(subject).Should().NotHaveDay(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not have day of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenUnexpectedIsNull_ShouldSucceed()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveDay(unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
