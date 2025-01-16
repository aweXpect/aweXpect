#if NET8_0_OR_GREATER
namespace aweXpect.Tests.DateOnlys;

public sealed partial class NullableDateOnlyShould
{
	public sealed class BeAfter
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateOnly? subject = CurrentTime();
				DateOnly? expected = null;

				async Task Act()
					=> await That(subject).Should().BeAfter(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be after <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
			{
				DateOnly? subject = DateOnly.MaxValue;
				DateOnly expected = DateOnly.MaxValue;

				async Task Act()
					=> await That(subject).Should().BeAfter(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be after 9999-12-31,
					             but it was 9999-12-31
					             """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
			{
				DateOnly? subject = DateOnly.MinValue;
				DateOnly expected = DateOnly.MinValue;

				async Task Act()
					=> await That(subject).Should().BeAfter(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be after 0001-01-01,
					             but it was 0001-01-01
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsEarlier_ShouldFail()
			{
				DateOnly? subject = EarlierTime();
				DateOnly? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeAfter(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be after {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldFail()
			{
				DateOnly? subject = CurrentTime();
				DateOnly? expected = subject;

				async Task Act()
					=> await That(subject).Should().BeAfter(expected)
						.Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be after {Formatter.Format(expected)}, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectsIsLater_ShouldSucceed()
			{
				DateOnly? subject = LaterTime();
				DateOnly? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeAfter(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task Within_WhenNullableExpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				DateOnly? subject = CurrentTime();
				DateOnly? expected = LaterTime(4);

				async Task Act()
					=> await That(subject).Should().BeAfter(expected)
						.Within(3.Days());

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be after {Formatter.Format(expected)} ± 3 days,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				DateOnly? subject = EarlierTime(3);
				DateOnly? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeAfter(expected)
						.Within(3.Days())
						.Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be after {Formatter.Format(expected)} ± 3 days, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				DateOnly? subject = EarlierTime(2);
				DateOnly? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().BeAfter(expected)
						.Within(3.Days());

				await That(Act).Does().NotThrow();
			}
		}
	}
}
#endif
