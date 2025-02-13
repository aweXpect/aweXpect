﻿namespace aweXpect.Tests;

public sealed partial class ThatNullableTimeSpan
{
	public sealed class IsNotLessThanOrEqualTo
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
			{
				TimeSpan? subject = TimeSpan.MaxValue;
				TimeSpan unexpected = TimeSpan.MaxValue;

				async Task Act()
					=> await That(subject).IsNotLessThanOrEqualTo(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not less than or equal to the maximum time span,
					             but it was the maximum time span
					             """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
			{
				TimeSpan? subject = TimeSpan.MinValue;
				TimeSpan unexpected = TimeSpan.MinValue;

				async Task Act()
					=> await That(subject).IsNotLessThanOrEqualTo(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not less than or equal to the minimum time span,
					             but it was the minimum time span
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsEarlier_ShouldFail()
			{
				TimeSpan? subject = EarlierTime();
				TimeSpan? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotLessThanOrEqualTo(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not less than or equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				TimeSpan? subject = null;

				async Task Act()
					=> await That(subject).IsNotLessThanOrEqualTo(TimeSpan.Zero);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is not less than or equal to 0:00,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldFail()
			{
				TimeSpan? subject = CurrentTime();
				TimeSpan? unexpected = subject;

				async Task Act()
					=> await That(subject).IsNotLessThanOrEqualTo(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not less than or equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectsIsLater_ShouldSucceed()
			{
				TimeSpan? subject = LaterTime();
				TimeSpan? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotLessThanOrEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldFail()
			{
				TimeSpan? subject = CurrentTime();
				TimeSpan? unexpected = null;

				async Task Act()
					=> await That(subject).IsNotLessThanOrEqualTo(unexpected)
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not less than or equal to <null>, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenNullableUnexpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				TimeSpan? subject = CurrentTime();
				TimeSpan? unexpected = LaterTime(3);

				async Task Act()
					=> await That(subject).IsNotLessThanOrEqualTo(unexpected)
						.Within(3.Seconds())
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not less than or equal to {Formatter.Format(unexpected)} ± 0:03, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				TimeSpan? subject = EarlierTime(3);
				TimeSpan? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotLessThanOrEqualTo(unexpected)
						.Within(3.Seconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not less than or equal to {Formatter.Format(unexpected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				TimeSpan? subject = EarlierTime(2);
				TimeSpan? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotLessThanOrEqualTo(unexpected)
						.Within(3.Seconds());

				await That(Act).DoesNotThrow();
			}
		}
	}
}
