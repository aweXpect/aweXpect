namespace Testably.Expectations.Tests.ThatTests.DateTimes;

public sealed partial class DateTimeShould
{
	public sealed class HaveSecondTests
	{
		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().HaveSecond(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have second of <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSecondOfSubjectIsDifferent_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? expected = 14;

			async Task Act()
				=> await That(subject).Should().HaveSecond(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have second of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSecondOfSubjectIsTheSame_ShouldSucceed()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int expected = 15;

			async Task Act()
				=> await That(subject).Should().HaveSecond(expected);

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotHaveSecondTests
	{
		[Fact]
		public async Task WhenSecondOfSubjectIsDifferent_ShouldSucceed()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? unexpected = 14;

			async Task Act()
				=> await That(subject).Should().NotHaveSecond(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSecondOfSubjectIsTheSame_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int unexpected = 15;

			async Task Act()
				=> await That(subject).Should().NotHaveSecond(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not have second of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenUnexpectedIsNull_ShouldSucceed()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveSecond(unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
