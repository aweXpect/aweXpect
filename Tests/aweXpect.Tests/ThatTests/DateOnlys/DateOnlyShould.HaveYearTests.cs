#if NET6_0_OR_GREATER
namespace Testably.Expectations.Tests.ThatTests.DateOnlys;

public sealed partial class DateOnlyShould
{
	public sealed class HaveYearTests
	{
		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			DateOnly subject = new(2010, 11, 12);
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().HaveYear(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have year of <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenYearOfSubjectIsDifferent_ShouldFail()
		{
			DateOnly subject = new(2010, 11, 12);
			int? expected = 2011;

			async Task Act()
				=> await That(subject).Should().HaveYear(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have year of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenYearOfSubjectIsTheSame_ShouldSucceed()
		{
			DateOnly subject = new(2010, 11, 12);
			int expected = 2010;

			async Task Act()
				=> await That(subject).Should().HaveYear(expected);

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotHaveYearTests
	{
		[Fact]
		public async Task WhenUnexpectedIsNull_ShouldSucceed()
		{
			DateOnly subject = new(2010, 11, 12);
			int? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotHaveYear(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenYearOfSubjectIsDifferent_ShouldSucceed()
		{
			DateOnly subject = new(2010, 11, 12);
			int? unexpected = 2011;

			async Task Act()
				=> await That(subject).Should().NotHaveYear(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenYearOfSubjectIsTheSame_ShouldFail()
		{
			DateOnly subject = new(2010, 11, 12);
			int unexpected = 2010;

			async Task Act()
				=> await That(subject).Should().NotHaveYear(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not have year of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}
	}
}
#endif
