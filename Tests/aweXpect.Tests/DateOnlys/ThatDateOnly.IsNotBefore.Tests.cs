﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatDateOnly
{
	public sealed class IsNotBefore
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
			{
				DateOnly subject = DateOnly.MaxValue;
				DateOnly unexpected = DateOnly.MaxValue;

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
			{
				DateOnly subject = DateOnly.MinValue;
				DateOnly unexpected = DateOnly.MinValue;

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsEarlier_ShouldFail()
			{
				DateOnly subject = EarlierTime();
				DateOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be before {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldSucceed()
			{
				DateOnly subject = CurrentTime();
				DateOnly unexpected = subject;

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectsIsLater_ShouldSucceed()
			{
				DateOnly subject = LaterTime();
				DateOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldFail()
			{
				DateOnly subject = CurrentTime();
				DateOnly? unexpected = null;

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected)
						.Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be before <null>, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenNullableUnexpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				DateOnly subject = CurrentTime();
				DateOnly? unexpected = LaterTime(4);

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected)
						.Within(3.Days())
						.Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be before {Formatter.Format(unexpected)} ± 3 days, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				DateOnly subject = EarlierTime(4);
				DateOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected)
						.Within(3.Days());

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be before {Formatter.Format(unexpected)} ± 3 days,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				DateOnly subject = EarlierTime(3);
				DateOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotBefore(unexpected)
						.Within(3.Days());

				await That(Act).Does().NotThrow();
			}
		}
	}
}
#endif