namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class NullableDateTimeOffsetShould
{
	public sealed class BeAfter
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTimeOffset? subject = CurrentTime();
				DateTimeOffset? expected = null;

				async Task Act()
					=> await That(subject).Should().BeAfter(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be after <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
			{
				DateTimeOffset? subject = DateTimeOffset.MaxValue;
				DateTimeOffset? expected = DateTimeOffset.MaxValue;

				async Task Act()
					=> await That(subject).Should().BeAfter(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be after 9999-12-31T23:59:59.9999999+00:00,
					             but it was 9999-12-31T23:59:59.9999999+00:00
					             """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
			{
				DateTimeOffset? subject = DateTimeOffset.MinValue;
				DateTimeOffset? expected = DateTimeOffset.MinValue;

				async Task Act()
					=> await That(subject).Should().BeAfter(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be after 0001-01-01T00:00:00.0000000+00:00,
					             but it was 0001-01-01T00:00:00.0000000+00:00
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsEarlier_ShouldFail()
			{
				DateTimeOffset? subject = EarlierTime();
				DateTimeOffset? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeAfter(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be after {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldFail()
			{
				DateTimeOffset? subject = CurrentTime();
				DateTimeOffset? expected = subject;

				async Task Act()
					=> await That(subject).Should().BeAfter(expected)
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be after {Formatter.Format(expected)}, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectsIsLater_ShouldSucceed()
			{
				DateTimeOffset? subject = LaterTime();
				DateTimeOffset? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeAfter(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task Within_WhenExpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				DateTimeOffset? subject = CurrentTime();
				DateTimeOffset? expected = EarlierTime(-3);

				async Task Act()
					=> await That(subject).Should().BeAfter(expected)
						.Within(3.Seconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be after {Formatter.Format(expected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				DateTimeOffset? subject = EarlierTime(3);
				DateTimeOffset? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeAfter(expected)
						.Within(3.Seconds())
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be after {Formatter.Format(expected)} ± 0:03, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				DateTimeOffset? subject = EarlierTime(2);
				DateTimeOffset? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeAfter(expected)
						.Within(3.Seconds());

				await That(Act).Should().NotThrow();
			}
		}
	}
}
