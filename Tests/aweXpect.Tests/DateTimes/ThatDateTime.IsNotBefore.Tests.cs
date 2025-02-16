﻿namespace aweXpect.Tests;

public sealed partial class ThatDateTime
{
	public sealed class IsNotBefore
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
			{
				DateTime subject = DateTime.MaxValue;
				DateTime unexpected = DateTime.MaxValue;

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
			{
				DateTime subject = DateTime.MinValue;
				DateTime unexpected = DateTime.MinValue;

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsEarlier_ShouldFail()
			{
				DateTime subject = EarlierTime();
				DateTime unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not before {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldSucceed()
			{
				DateTime subject = CurrentTime();
				DateTime unexpected = subject;

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectsIsLater_ShouldSucceed()
			{
				DateTime subject = LaterTime();
				DateTime unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldFail()
			{
				DateTime subject = CurrentTime();
				DateTime? unexpected = null;

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected)
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not before <null>, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenNullableUnexpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				DateTime subject = CurrentTime();
				DateTime? unexpected = LaterTime(4);

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected)
						.Within(3.Seconds())
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not before {Formatter.Format(unexpected)} ± 0:03, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				DateTime subject = EarlierTime(4);
				DateTime unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected)
						.Within(3.Seconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not before {Formatter.Format(unexpected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				DateTime subject = EarlierTime(3);
				DateTime unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected)
						.Within(3.Seconds());

				await That(Act).DoesNotThrow();
			}
		}
	}
}
