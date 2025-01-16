namespace aweXpect.Tests.TimeSpans;

public sealed partial class NullableTimeSpanShould
{
	public sealed class NotBeGreaterThan
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
			{
				TimeSpan? subject = TimeSpan.MaxValue;
				TimeSpan unexpected = TimeSpan.MaxValue;

				async Task Act()
					=> await That(subject).Should().NotBeGreaterThan(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
			{
				TimeSpan? subject = TimeSpan.MinValue;
				TimeSpan unexpected = TimeSpan.MinValue;

				async Task Act()
					=> await That(subject).Should().NotBeGreaterThan(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsLater_ShouldFail()
			{
				TimeSpan? subject = LaterTime();
				TimeSpan? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeGreaterThan(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be greater than {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldSucceed()
			{
				TimeSpan? subject = CurrentTime();
				TimeSpan? unexpected = subject;

				async Task Act()
					=> await That(subject).Should().NotBeGreaterThan(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectsIsEarlier_ShouldSucceed()
			{
				TimeSpan? subject = EarlierTime();
				TimeSpan? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeGreaterThan(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldFail()
			{
				TimeSpan? subject = CurrentTime();
				TimeSpan? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotBeGreaterThan(unexpected)
						.Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be greater than <null>, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenNullableUnexpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				TimeSpan? subject = CurrentTime();
				TimeSpan? unexpected = EarlierTime(4);

				async Task Act()
					=> await That(subject).Should().NotBeGreaterThan(unexpected)
						.Within(3.Seconds())
						.Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be greater than {Formatter.Format(unexpected)} ± 0:03, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				TimeSpan? subject = LaterTime(4);
				TimeSpan? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeGreaterThan(unexpected)
						.Within(3.Seconds());

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be greater than {Formatter.Format(unexpected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				TimeSpan? subject = LaterTime(3);
				TimeSpan? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeGreaterThan(unexpected)
						.Within(3.Seconds());

				await That(Act).Does().NotThrow();
			}
		}
	}
}
