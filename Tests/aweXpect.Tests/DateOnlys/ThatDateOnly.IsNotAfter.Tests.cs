#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatDateOnly
{
	public sealed class IsNotAfter
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
			{
				DateOnly subject = DateOnly.MaxValue;
				DateOnly unexpected = DateOnly.MaxValue;

				async Task Act()
					=> await That(subject).IsNotAfter(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
			{
				DateOnly subject = DateOnly.MinValue;
				DateOnly unexpected = DateOnly.MinValue;

				async Task Act()
					=> await That(subject).IsNotAfter(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsLater_ShouldFail()
			{
				DateOnly subject = LaterTime();
				DateOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotAfter(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be after {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldSucceed()
			{
				DateOnly subject = CurrentTime();
				DateOnly unexpected = subject;

				async Task Act()
					=> await That(subject).IsNotAfter(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectsIsEarlier_ShouldSucceed()
			{
				DateOnly subject = EarlierTime();
				DateOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotAfter(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldFail()
			{
				DateOnly subject = CurrentTime();
				DateOnly? unexpected = null;

				async Task Act()
					=> await That(subject).IsNotAfter(unexpected)
						.Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be after <null>, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenNullableUnexpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				DateOnly subject = CurrentTime();
				DateOnly? unexpected = EarlierTime(4);

				async Task Act()
					=> await That(subject).IsNotAfter(unexpected)
						.Within(3.Days())
						.Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be after {Formatter.Format(unexpected)} ± 3 days, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				DateOnly subject = LaterTime(4);
				DateOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotAfter(unexpected)
						.Within(3.Days());

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be after {Formatter.Format(unexpected)} ± 3 days,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				DateOnly subject = LaterTime(3);
				DateOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotAfter(unexpected)
						.Within(3.Days());

				await That(Act).Does().NotThrow();
			}
		}
	}
}
#endif
