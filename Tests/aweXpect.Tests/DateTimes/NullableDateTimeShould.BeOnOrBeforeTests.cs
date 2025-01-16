namespace aweXpect.Tests.DateTimes;

public sealed partial class NullableDateTimeShould
{
	public sealed class BeOnOrBefore
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTime? subject = CurrentTime();
				DateTime? expected = null;

				async Task Act()
					=> await That(subject).Should().BeOnOrBefore(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be on or before <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
			{
				DateTime? subject = DateTime.MaxValue;
				DateTime? expected = DateTime.MaxValue;

				async Task Act()
					=> await That(subject).Should().BeOnOrBefore(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
			{
				DateTime? subject = DateTime.MinValue;
				DateTime? expected = DateTime.MinValue;

				async Task Act()
					=> await That(subject).Should().BeOnOrBefore(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsLater_ShouldFail()
			{
				DateTime? subject = LaterTime();
				DateTime? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeOnOrBefore(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be on or before {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldSucceed()
			{
				DateTime? subject = CurrentTime();
				DateTime? expected = subject;

				async Task Act()
					=> await That(subject).Should().BeOnOrBefore(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectsIsEarlier_ShouldSucceed()
			{
				DateTime? subject = EarlierTime();
				DateTime? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeOnOrBefore(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task Within_WhenExpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				DateTime? subject = CurrentTime();
				DateTime expected = LaterTime(-4);

				async Task Act()
					=> await That(subject).Should().BeOnOrBefore(expected)
						.Within(3.Seconds());

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be on or before {Formatter.Format(expected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				DateTime? subject = LaterTime(4);
				DateTime? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeOnOrBefore(expected)
						.Within(3.Seconds());

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be on or before {Formatter.Format(expected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				DateTime? subject = LaterTime(3);
				DateTime? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeOnOrBefore(expected)
						.Within(3.Seconds());

				await That(Act).Does().NotThrow();
			}
		}
	}
}
