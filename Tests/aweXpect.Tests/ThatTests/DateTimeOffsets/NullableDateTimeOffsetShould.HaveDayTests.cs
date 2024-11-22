namespace Testably.Expectations.Tests.ThatTests.DateTimeOffsets;

public sealed partial class NullableDateTimeOffsetShould
{
	public sealed class HaveDayTests
	{
		[Fact]
		public async Task WhenDayOfSubjectIsDifferent_ShouldFail()
		{
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
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
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
			int expected = 12;

			async Task Act()
				=> await That(subject).Should().HaveDay(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
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

		[Fact]
		public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
		{
			DateTimeOffset? subject = null;
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().HaveDay(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have day of <null>,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			DateTimeOffset? subject = null;
			int? expected = 1;

			async Task Act()
				=> await That(subject).Should().HaveDay(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have day of 1,
				             but it was <null>
				             """);
		}
	}

	public sealed class NotHaveDayTests
	{
		[Fact]
		public async Task WhenDayOfSubjectIsDifferent_ShouldSucceed()
		{
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
			int? unexpected = 11;

			async Task Act()
				=> await That(subject).Should().NotHaveDay(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenDayOfSubjectIsTheSame_ShouldFail()
		{
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
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
		public async Task WhenSubjectAndUnexpectedIsNull_ShouldSucceed()
		{
			DateTimeOffset? subject = null;
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveDay(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldSucceed()
		{
			DateTimeOffset? subject = null;
			int? expected = 1;

			async Task Act()
				=> await That(subject).Should().NotHaveDay(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenUnexpectedIsNull_ShouldSucceed()
		{
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
			int? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveDay(unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
