namespace Testably.Expectations.Tests.ThatTests.DateTimes;

public sealed partial class NullableDateTimeShould
{
	public sealed class HaveMillisecondTests
	{
		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().HaveMillisecond(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have millisecond of <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenMillisecondOfSubjectIsDifferent_ShouldFail()
		{
			DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? expected = 15;

			async Task Act()
				=> await That(subject).Should().HaveMillisecond(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have millisecond of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenMillisecondOfSubjectIsTheSame_ShouldSucceed()
		{
			DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
			int expected = 167;

			async Task Act()
				=> await That(subject).Should().HaveMillisecond(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
		{
			DateTime? subject = null;
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().HaveMillisecond(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have millisecond of <null>,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			DateTime? subject = null;
			int? expected = 1;

			async Task Act()
				=> await That(subject).Should().HaveMillisecond(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have millisecond of 1,
				             but it was <null>
				             """);
		}
	}

	public sealed class NotHaveMillisecondTests
	{
		[Fact]
		public async Task WhenMillisecondOfSubjectIsDifferent_ShouldSucceed()
		{
			DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? unexpected = 15;

			async Task Act()
				=> await That(subject).Should().NotHaveMillisecond(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenMillisecondOfSubjectIsTheSame_ShouldFail()
		{
			DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
			int unexpected = 167;

			async Task Act()
				=> await That(subject).Should().NotHaveMillisecond(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not have millisecond of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectAndUnexpectedIsNull_ShouldSucceed()
		{
			DateTime? subject = null;
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveMillisecond(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldSucceed()
		{
			DateTime? subject = null;
			int? expected = 1;

			async Task Act()
				=> await That(subject).Should().NotHaveMillisecond(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenUnexpectedIsNull_ShouldSucceed()
		{
			DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
			int? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveMillisecond(unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
