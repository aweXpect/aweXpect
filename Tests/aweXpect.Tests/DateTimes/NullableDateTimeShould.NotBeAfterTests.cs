namespace aweXpect.Tests.DateTimes;

public sealed partial class NullableDateTimeShould
{
	public sealed class NotBeAfter
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
			{
				DateTime? subject = DateTime.MaxValue;
				DateTime? unexpected = DateTime.MaxValue;

				async Task Act()
					=> await That(subject).Should().NotBeAfter(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
			{
				DateTime? subject = DateTime.MinValue;
				DateTime? unexpected = DateTime.MinValue;

				async Task Act()
					=> await That(subject).Should().NotBeAfter(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsLater_ShouldFail()
			{
				DateTime? subject = LaterTime();
				DateTime? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeAfter(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be after {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldSucceed()
			{
				DateTime? subject = CurrentTime();
				DateTime? unexpected = subject;

				async Task Act()
					=> await That(subject).Should().NotBeAfter(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectsIsEarlier_ShouldSucceed()
			{
				DateTime? subject = EarlierTime();
				DateTime? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeAfter(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldFail()
			{
				DateTime? subject = CurrentTime();
				DateTime? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotBeAfter(unexpected)
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be after <null>, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenUnexpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				DateTime? subject = CurrentTime();
				DateTime unexpected = EarlierTime(4);

				async Task Act()
					=> await That(subject).Should().NotBeAfter(unexpected)
						.Within(TimeSpan.FromSeconds(3))
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be after {Formatter.Format(unexpected)} ± 0:03, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				DateTime? subject = LaterTime(4);
				DateTime? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeAfter(unexpected)
						.Within(TimeSpan.FromSeconds(3));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be after {Formatter.Format(unexpected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				DateTime? subject = LaterTime(3);
				DateTime? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeAfter(unexpected)
						.Within(TimeSpan.FromSeconds(3));

				await That(Act).Should().NotThrow();
			}
		}
	}
}
