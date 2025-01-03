﻿namespace aweXpect.Tests.TimeSpans;

public sealed partial class TimeSpanShould
{
	public sealed class BeGreaterThanOrEqualTo
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				TimeSpan subject = CurrentTime();
				TimeSpan? expected = null;

				async Task Act()
					=> await That(subject).Should().BeGreaterThanOrEqualTo(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be greater than or equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
			{
				TimeSpan subject = TimeSpan.MaxValue;
				TimeSpan expected = TimeSpan.MaxValue;

				async Task Act()
					=> await That(subject).Should().BeGreaterThanOrEqualTo(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
			{
				TimeSpan subject = TimeSpan.MinValue;
				TimeSpan expected = TimeSpan.MinValue;

				async Task Act()
					=> await That(subject).Should().BeGreaterThanOrEqualTo(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsEarlier_ShouldFail()
			{
				TimeSpan subject = EarlierTime();
				TimeSpan expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeGreaterThanOrEqualTo(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be greater than or equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldSucceed()
			{
				TimeSpan subject = CurrentTime();
				TimeSpan expected = subject;

				async Task Act()
					=> await That(subject).Should().BeGreaterThanOrEqualTo(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectsIsLater_ShouldSucceed()
			{
				TimeSpan subject = LaterTime();
				TimeSpan expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeGreaterThanOrEqualTo(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task Within_WhenNullableExpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				TimeSpan subject = CurrentTime();
				TimeSpan? expected = EarlierTime(-4);

				async Task Act()
					=> await That(subject).Should().BeGreaterThanOrEqualTo(expected)
						.Within(3.Seconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be greater than or equal to {Formatter.Format(expected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				TimeSpan subject = EarlierTime(4);
				TimeSpan expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeGreaterThanOrEqualTo(expected)
						.Within(3.Seconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be greater than or equal to {Formatter.Format(expected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				TimeSpan subject = EarlierTime(3);
				TimeSpan expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeGreaterThanOrEqualTo(expected)
						.Within(3.Seconds());

				await That(Act).Should().NotThrow();
			}
		}
	}
}