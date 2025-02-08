﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatNullableTimeOnly
{
	public sealed class IsNotOnOrAfter
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
			{
				TimeOnly? subject = TimeOnly.MaxValue;
				TimeOnly unexpected = TimeOnly.MaxValue;

				async Task Act()
					=> await That(subject).IsNotOnOrAfter(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not on or after {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
			{
				TimeOnly? subject = TimeOnly.MinValue;
				TimeOnly unexpected = TimeOnly.MinValue;

				async Task Act()
					=> await That(subject).IsNotOnOrAfter(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not on or after {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsLater_ShouldFail()
			{
				TimeOnly? subject = LaterTime();
				TimeOnly? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotOnOrAfter(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not on or after {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldFail()
			{
				TimeOnly? subject = CurrentTime();
				TimeOnly? unexpected = subject;

				async Task Act()
					=> await That(subject).IsNotOnOrAfter(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not on or after {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectsIsEarlier_ShouldSucceed()
			{
				TimeOnly? subject = EarlierTime();
				TimeOnly? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotOnOrAfter(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldFail()
			{
				TimeOnly? subject = CurrentTime();
				TimeOnly? unexpected = null;

				async Task Act()
					=> await That(subject).IsNotOnOrAfter(unexpected)
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not on or after <null>, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenNullableUnexpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				TimeOnly? subject = CurrentTime();
				TimeOnly? unexpected = EarlierTime(3);

				async Task Act()
					=> await That(subject).IsNotOnOrAfter(unexpected)
						.Within(3.Seconds())
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not on or after {Formatter.Format(unexpected)} ± 0:03, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				TimeOnly? subject = LaterTime(3);
				TimeOnly? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotOnOrAfter(unexpected)
						.Within(3.Seconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not on or after {Formatter.Format(unexpected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				TimeOnly? subject = LaterTime(2);
				TimeOnly? unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotOnOrAfter(unexpected)
						.Within(3.Seconds());

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
