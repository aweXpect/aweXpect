#if NET6_0_OR_GREATER
namespace Testably.Expectations.Tests.ThatTests.TimeOnlys;

public sealed partial class TimeOnlyShould
{
	public sealed class HaveMinuteTests
	{
		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			TimeOnly subject = new(10, 11, 12);
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
			TimeOnly subject = new(10, 11, 12);
			int? expected = 12;

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
			TimeOnly subject = new(10, 11, 12);
			int expected = 11;

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
			TimeOnly subject = new(10, 11, 12);
			int? unexpected = 12;

			async Task Act()
				=> await That(subject).Should().NotHaveMinute(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenMinuteOfSubjectIsTheSame_ShouldFail()
		{
			TimeOnly subject = new(10, 11, 12);
			int unexpected = 11;

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
			TimeOnly subject = new(10, 11, 12);
			int? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveMinute(unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
#endif
