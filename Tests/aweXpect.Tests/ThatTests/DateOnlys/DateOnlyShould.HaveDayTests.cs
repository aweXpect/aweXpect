#if NET6_0_OR_GREATER
namespace Testably.Expectations.Tests.ThatTests.DateOnlys;

public sealed partial class DateOnlyShould
{
	public sealed class HaveDayTests
	{
		[Fact]
		public async Task WhenDayOfSubjectIsDifferent_ShouldFail()
		{
			DateOnly subject = new(2010, 11, 12);
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
			DateOnly subject = new(2010, 11, 12);
			int expected = 12;

			async Task Act()
				=> await That(subject).Should().HaveDay(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			DateOnly subject = new(2010, 11, 12);
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
			DateOnly subject = new(2010, 11, 12);
			int? unexpected = 11;

			async Task Act()
				=> await That(subject).Should().NotHaveDay(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenDayOfSubjectIsTheSame_ShouldFail()
		{
			DateOnly subject = new(2010, 11, 12);
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
			DateOnly subject = new(2010, 11, 12);
			int? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveDay(unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
#endif
