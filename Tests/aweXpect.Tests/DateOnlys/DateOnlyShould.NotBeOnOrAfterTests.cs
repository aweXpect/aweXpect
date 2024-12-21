#if NET8_0_OR_GREATER
namespace aweXpect.Tests.DateOnlys;

public sealed partial class DateOnlyShould
{
	public sealed class NotBeOnOrAfter
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
			{
				DateOnly subject = DateOnly.MaxValue;
				DateOnly unexpected = DateOnly.MaxValue;

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
				DateOnly subject = DateOnly.MinValue;
				DateOnly unexpected = DateOnly.MinValue;

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
				DateOnly subject = LaterTime();
				DateOnly unexpected = CurrentTime();

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
				DateOnly subject = CurrentTime();
				DateOnly unexpected = subject;

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
				DateOnly subject = EarlierTime();
				DateOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldFail()
			{
				DateOnly subject = CurrentTime();
				DateOnly? unexpected = null;

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
				DateOnly subject = CurrentTime();
				DateOnly? unexpected = EarlierTime(3);

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected)
						.Within(TimeSpan.FromDays(3))
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or after {Formatter.Format(unexpected)} ± 3 days, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				DateOnly subject = LaterTime(3);
				DateOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected)
						.Within(TimeSpan.FromDays(3));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or after {Formatter.Format(unexpected)} ± 3 days,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				DateOnly subject = LaterTime(2);
				DateOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected)
						.Within(TimeSpan.FromDays(3));

				await That(Act).Should().NotThrow();
			}
		}
	}
}
#endif
