namespace aweXpect.Tests.DateTimes;

public sealed partial class NullableDateTimeShould
{
	public sealed class HaveYear
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
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
			public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
			{
				DateTime? subject = null;
				int? expected = null;

				async Task Act()
					=> await That(subject).Should().HaveYear(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have year of <null>,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				DateTime? subject = null;
				int? expected = 1;

				async Task Act()
					=> await That(subject).Should().HaveYear(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have year of 1,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenYearOfSubjectIsDifferent_ShouldFail()
			{
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
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
				DateTime? subject = new(2010, 11, 12, 13, 14, 15, 167);
				int expected = 2010;

				async Task Act()
					=> await That(subject).Should().HaveYear(expected);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
