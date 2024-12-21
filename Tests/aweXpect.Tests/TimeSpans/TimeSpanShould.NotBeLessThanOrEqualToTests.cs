﻿namespace aweXpect.Tests.TimeSpans;

public sealed partial class TimeSpanShould
{
	public sealed class NotBeLessThanOrEqualTo
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
			{
				TimeSpan subject = TimeSpan.MaxValue;
				TimeSpan unexpected = TimeSpan.MaxValue;

				async Task Act()
					=> await That(subject).Should().NotBeLessThanOrEqualTo(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be less than or equal to the maximum time span,
					             but it was the maximum time span
					             """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
			{
				TimeSpan subject = TimeSpan.MinValue;
				TimeSpan unexpected = TimeSpan.MinValue;

				async Task Act()
					=> await That(subject).Should().NotBeLessThanOrEqualTo(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be less than or equal to the minimum time span,
					             but it was the minimum time span
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsEarlier_ShouldFail()
			{
				TimeSpan subject = EarlierTime();
				TimeSpan unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeLessThanOrEqualTo(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be less than or equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldFail()
			{
				TimeSpan subject = CurrentTime();
				TimeSpan unexpected = subject;

				async Task Act()
					=> await That(subject).Should().NotBeLessThanOrEqualTo(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be less than or equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectsIsLater_ShouldSucceed()
			{
				TimeSpan subject = LaterTime();
				TimeSpan unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeLessThanOrEqualTo(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldFail()
			{
				TimeSpan subject = CurrentTime();
				TimeSpan? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotBeLessThanOrEqualTo(unexpected)
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be less than or equal to <null>, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenNullableUnexpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				TimeSpan subject = CurrentTime();
				TimeSpan? unexpected = LaterTime(3);

				async Task Act()
					=> await That(subject).Should().NotBeLessThanOrEqualTo(unexpected)
						.Within(TimeSpan.FromSeconds(3))
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be less than or equal to {Formatter.Format(unexpected)} ± 0:03, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				TimeSpan subject = EarlierTime(3);
				TimeSpan unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeLessThanOrEqualTo(unexpected)
						.Within(TimeSpan.FromSeconds(3));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be less than or equal to {Formatter.Format(unexpected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				TimeSpan subject = EarlierTime(2);
				TimeSpan unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeLessThanOrEqualTo(unexpected)
						.Within(TimeSpan.FromSeconds(3));

				await That(Act).Should().NotThrow();
			}
		}
	}
}
