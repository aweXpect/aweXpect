namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class DateTimeOffsetShould
{
	public sealed class BeOnOrAfter
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTimeOffset subject = CurrentTime();
				DateTimeOffset? expected = null;

				async Task Act()
					=> await That(subject).Should().BeOnOrAfter(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be on or after <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
			{
				DateTimeOffset subject = DateTimeOffset.MaxValue;
				DateTimeOffset expected = DateTimeOffset.MaxValue;

				async Task Act()
					=> await That(subject).Should().BeOnOrAfter(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
			{
				DateTimeOffset subject = DateTimeOffset.MinValue;
				DateTimeOffset expected = DateTimeOffset.MinValue;

				async Task Act()
					=> await That(subject).Should().BeOnOrAfter(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsEarlier_ShouldFail()
			{
				DateTimeOffset subject = EarlierTime();
				DateTimeOffset expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeOnOrAfter(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be on or after {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldSucceed()
			{
				DateTimeOffset subject = CurrentTime();
				DateTimeOffset expected = subject;

				async Task Act()
					=> await That(subject).Should().BeOnOrAfter(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectsIsLater_ShouldSucceed()
			{
				DateTimeOffset subject = LaterTime();
				DateTimeOffset expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeOnOrAfter(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task Within_WhenNullableExpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				DateTimeOffset subject = CurrentTime();
				DateTimeOffset? expected = EarlierTime(-4);

				async Task Act()
					=> await That(subject).Should().BeOnOrAfter(expected)
						.Within(TimeSpan.FromSeconds(3));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be on or after {Formatter.Format(expected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				DateTimeOffset subject = EarlierTime(4);
				DateTimeOffset expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeOnOrAfter(expected)
						.Within(TimeSpan.FromSeconds(3));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be on or after {Formatter.Format(expected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				DateTimeOffset subject = EarlierTime(3);
				DateTimeOffset expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeOnOrAfter(expected)
						.Within(TimeSpan.FromSeconds(3));

				await That(Act).Should().NotThrow();
			}
		}
	}
}
