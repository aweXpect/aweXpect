﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests.TimeOnlys;

public sealed partial class TimeOnlyShould
{
	public sealed class NotBeOnOrAfter
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
			{
				TimeOnly subject = TimeOnly.MaxValue;
				TimeOnly unexpected = TimeOnly.MaxValue;

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or after {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
			{
				TimeOnly subject = TimeOnly.MinValue;
				TimeOnly unexpected = TimeOnly.MinValue;

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or after {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsLater_ShouldFail()
			{
				TimeOnly subject = LaterTime();
				TimeOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or after {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldFail()
			{
				TimeOnly subject = CurrentTime();
				TimeOnly unexpected = subject;

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or after {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectsIsEarlier_ShouldSucceed()
			{
				TimeOnly subject = EarlierTime();
				TimeOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldFail()
			{
				TimeOnly subject = CurrentTime();
				TimeOnly? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected)
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or after <null>, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenNullableUnexpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				TimeOnly subject = CurrentTime();
				TimeOnly? unexpected = EarlierTime(3);

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected)
						.Within(TimeSpan.FromSeconds(3))
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or after {Formatter.Format(unexpected)} ± 0:03, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				TimeOnly subject = LaterTime(3);
				TimeOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected)
						.Within(TimeSpan.FromSeconds(3));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or after {Formatter.Format(unexpected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				TimeOnly subject = LaterTime(2);
				TimeOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected)
						.Within(TimeSpan.FromSeconds(3));

				await That(Act).Should().NotThrow();
			}
		}
	}
}
#endif
