﻿namespace aweXpect.Tests.DateTimes;

public sealed partial class NullableDateTimeShould
{
	public sealed class BeAfter
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTime? subject = CurrentTime();
				DateTime? expected = null;

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
				DateTime? subject = DateTime.MaxValue;
				DateTime? expected = DateTime.MaxValue;

				async Task Act()
					=> await That(subject).Should().BeAfter(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be after 9999-12-31T23:59:59.9999999,
					             but it was 9999-12-31T23:59:59.9999999
					             """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
			{
				DateTime? subject = DateTime.MinValue;
				DateTime? expected = DateTime.MinValue;

				async Task Act()
					=> await That(subject).Should().BeAfter(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be after 0001-01-01T00:00:00.0000000,
					             but it was 0001-01-01T00:00:00.0000000
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsEarlier_ShouldFail()
			{
				DateTime? subject = EarlierTime();
				DateTime? expected = CurrentTime();

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
				DateTime? subject = CurrentTime();
				DateTime? expected = subject;

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
				DateTime? subject = LaterTime();
				DateTime? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeAfter(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task Within_WhenExpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				DateTime? subject = CurrentTime();
				DateTime expected = EarlierTime(-3);

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
				DateTime? subject = EarlierTime(3);
				DateTime? expected = CurrentTime();

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
				DateTime? subject = EarlierTime(2);
				DateTime? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeAfter(expected)
						.Within(3.Seconds());

				await That(Act).Should().NotThrow();
			}
		}
	}
}
