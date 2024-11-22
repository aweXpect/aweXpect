namespace Testably.Expectations.Tests.ThatTests.DateTimeOffsets;

public sealed partial class NullableDateTimeOffsetShould
{
	public sealed class HaveMinuteTests
	{
		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
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
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
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
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
			int expected = 14;

			async Task Act()
				=> await That(subject).Should().HaveMinute(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
		{
			DateTimeOffset? subject = null;
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().HaveMinute(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have minute of <null>,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			DateTimeOffset? subject = null;
			int? expected = 1;

			async Task Act()
				=> await That(subject).Should().HaveMinute(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have minute of 1,
				             but it was <null>
				             """);
		}
	}

	public sealed class NotHaveMinuteTests
	{
		[Fact]
		public async Task WhenMinuteOfSubjectIsDifferent_ShouldSucceed()
		{
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
			int? unexpected = 13;

			async Task Act()
				=> await That(subject).Should().NotHaveMinute(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenMinuteOfSubjectIsTheSame_ShouldFail()
		{
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
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
			DateTimeOffset? subject = null;
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveMinute(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldSucceed()
		{
			DateTimeOffset? subject = null;
			int? expected = 1;

			async Task Act()
				=> await That(subject).Should().NotHaveMinute(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenUnexpectedIsNull_ShouldSucceed()
		{
			DateTimeOffset? subject = new(2010, 11, 12, 13, 14, 15, 167, TimeSpan.FromHours(2));
			int? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveMinute(unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
