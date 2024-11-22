#if NET6_0_OR_GREATER
namespace Testably.Expectations.Tests.ThatTests.TimeOnlys;

public sealed partial class NullableTimeOnlyShould
{
	public sealed class HaveHourTests
	{
		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			TimeOnly? subject = new(10, 11, 12);
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
			TimeOnly? subject = new(10, 11, 12);
			int? expected = 11;

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
			TimeOnly? subject = new(10, 11, 12);
			int expected = 10;

			async Task Act()
				=> await That(subject).Should().HaveHour(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
		{
			TimeOnly? subject = null;
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().HaveHour(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have hour of <null>,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			TimeOnly? subject = null;
			int? expected = 1;

			async Task Act()
				=> await That(subject).Should().HaveHour(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have hour of 1,
				             but it was <null>
				             """);
		}
	}

	public sealed class NotHaveHourTests
	{
		[Fact]
		public async Task WhenHourOfSubjectIsDifferent_ShouldSucceed()
		{
			TimeOnly? subject = new(10, 11, 12);
			int? unexpected = 11;

			async Task Act()
				=> await That(subject).Should().NotHaveHour(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenHourOfSubjectIsTheSame_ShouldFail()
		{
			TimeOnly? subject = new(10, 11, 12);
			int unexpected = 10;

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
		public async Task WhenSubjectAndUnexpectedIsNull_ShouldSucceed()
		{
			TimeOnly? subject = null;
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveHour(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldSucceed()
		{
			TimeOnly? subject = null;
			int? expected = 1;

			async Task Act()
				=> await That(subject).Should().NotHaveHour(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenUnexpectedIsNull_ShouldSucceed()
		{
			TimeOnly? subject = new(10, 11, 12);
			int? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveHour(unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
#endif
