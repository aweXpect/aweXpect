namespace aweXpect.Tests.DateTimes;

public sealed partial class NullableDateTimeShould
{
	public sealed class NotBeOnOrBefore
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
			{
				DateTime? subject = DateTime.MaxValue;
				DateTime? unexpected = DateTime.MaxValue;

				async Task Act()
					=> await That(subject).Should().NotBeOnOrBefore(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be on or before 9999-12-31T23:59:59.9999999,
					             but it was 9999-12-31T23:59:59.9999999
					             """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
			{
				DateTime? subject = DateTime.MinValue;
				DateTime? unexpected = DateTime.MinValue;

				async Task Act()
					=> await That(subject).Should().NotBeOnOrBefore(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be on or before 0001-01-01T00:00:00.0000000,
					             but it was 0001-01-01T00:00:00.0000000
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsEarlier_ShouldFail()
			{
				DateTime? subject = EarlierTime();
				DateTime? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeOnOrBefore(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or before {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldFail()
			{
				DateTime? subject = CurrentTime();
				DateTime? unexpected = subject;

				async Task Act()
					=> await That(subject).Should().NotBeOnOrBefore(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or before {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectsIsLater_ShouldSucceed()
			{
				DateTime? subject = LaterTime();
				DateTime? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeOnOrBefore(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldFail()
			{
				DateTime? subject = CurrentTime();
				DateTime? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotBeOnOrBefore(unexpected)
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or before <null>, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenUnexpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				DateTime? subject = CurrentTime();
				DateTime unexpected = LaterTime(4);

				async Task Act()
					=> await That(subject).Should().NotBeOnOrBefore(unexpected)
						.Within(3.Seconds())
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or before {Formatter.Format(unexpected)} ± 0:03, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				DateTime? subject = EarlierTime(3);
				DateTime? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeOnOrBefore(unexpected)
						.Within(3.Seconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or before {Formatter.Format(unexpected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				DateTime subject = EarlierTime(2);
				DateTime unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeOnOrBefore(unexpected)
						.Within(3.Seconds());

				await That(Act).Should().NotThrow();
			}
		}
	}
}
