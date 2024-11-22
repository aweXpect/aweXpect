namespace Testably.Expectations.Tests.ThatTests.DateTimes;

public sealed partial class DateTimeShould
{
	public sealed class HaveMinuteTests
	{
		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().HaveMinute(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have minute of <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenMinuteOfSubjectIsDifferent_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? expected = 13;

			async Task Act()
				=> await That(subject).Should().HaveMinute(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have minute of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenMinuteOfSubjectIsTheSame_ShouldSucceed()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int expected = 14;

			async Task Act()
				=> await That(subject).Should().HaveMinute(expected);

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotHaveMinuteTests
	{
		[Fact]
		public async Task WhenMinuteOfSubjectIsDifferent_ShouldSucceed()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? unexpected = 13;

			async Task Act()
				=> await That(subject).Should().NotHaveMinute(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenMinuteOfSubjectIsTheSame_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
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
		public async Task WhenUnexpectedIsNull_ShouldSucceed()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveMinute(unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
