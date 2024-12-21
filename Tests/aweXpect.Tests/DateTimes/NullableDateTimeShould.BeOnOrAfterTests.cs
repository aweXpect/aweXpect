﻿namespace aweXpect.Tests.DateTimes;

public sealed partial class NullableDateTimeShould
{
	public sealed class BeOnOrAfter
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTime? subject = CurrentTime();
				DateTime? expected = null;

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
				DateTime? subject = DateTime.MaxValue;
				DateTime? expected = DateTime.MaxValue;

				async Task Act()
					=> await That(subject).Should().BeOnOrAfter(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
			{
				DateTime? subject = DateTime.MinValue;
				DateTime? expected = DateTime.MinValue;

				async Task Act()
					=> await That(subject).Should().BeOnOrAfter(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsEarlier_ShouldFail()
			{
				DateTime? subject = EarlierTime();
				DateTime? expected = CurrentTime();

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
				DateTime? subject = CurrentTime();
				DateTime? expected = subject;

				async Task Act()
					=> await That(subject).Should().BeOnOrAfter(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectsIsLater_ShouldSucceed()
			{
				DateTime? subject = LaterTime();
				DateTime? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeOnOrAfter(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task Within_WhenExpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				DateTime? subject = CurrentTime();
				DateTime expected = EarlierTime(-4);

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
				DateTime? subject = EarlierTime(4);
				DateTime? expected = CurrentTime();

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
				DateTime? subject = EarlierTime(3);
				DateTime? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeOnOrAfter(expected)
						.Within(TimeSpan.FromSeconds(3));

				await That(Act).Should().NotThrow();
			}
		}
	}
}
