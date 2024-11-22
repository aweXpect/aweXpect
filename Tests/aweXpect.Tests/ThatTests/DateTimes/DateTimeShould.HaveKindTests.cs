namespace Testably.Expectations.Tests.ThatTests.DateTimes;

public sealed partial class DateTimeShould
{
	public sealed class HaveKindTests
	{
		[Fact]
		public async Task WhenKindOfSubjectIsDifferent_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167, DateTimeKind.Utc);
			DateTimeKind expected = DateTimeKind.Local;

			async Task Act()
				=> await That(subject).Should().HaveKind(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              have kind of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenKindOfSubjectIsTheSame_ShouldSucceed()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167, DateTimeKind.Utc);
			DateTimeKind expected = DateTimeKind.Utc;

			async Task Act()
				=> await That(subject).Should().HaveKind(expected);

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotHaveKindTests
	{
		[Fact]
		public async Task WhenKindOfSubjectIsDifferent_ShouldSucceed()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167, DateTimeKind.Utc);
			DateTimeKind unexpected = DateTimeKind.Local;

			async Task Act()
				=> await That(subject).Should().NotHaveKind(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenKindOfSubjectIsTheSame_ShouldFail()
		{
			DateTime subject = new(2010, 11, 12, 13, 14, 15, 167, DateTimeKind.Utc);
			DateTimeKind unexpected = DateTimeKind.Utc;

			async Task Act()
				=> await That(subject).Should().NotHaveKind(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not have kind of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}
	}
}
