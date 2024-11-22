namespace Testably.Expectations.Tests.ThatTests.DateTimes;

public sealed partial class DateTimeShould
{
	public sealed class HaveMonthTests
	{
		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
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
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
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
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int expected = 11;

			async Task Act()
				=> await That(subject).Should().HaveMonth(expected);

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotHaveMonthTests
	{
		[Fact]
		public async Task WhenMonthOfSubjectIsDifferent_ShouldSucceed()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? unexpected = 12;

			async Task Act()
				=> await That(subject).Should().NotHaveMonth(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenMonthOfSubjectIsTheSame_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
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
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveMonth(unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
