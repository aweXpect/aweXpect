#if NET6_0_OR_GREATER
namespace Testably.Expectations.Tests.ThatTests.TimeOnlys;

public sealed partial class TimeOnlyShould
{
	public sealed class BeBeforeTests
	{
		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			TimeOnly subject = CurrentTime();
			TimeOnly? expected = null;

			async Task Act()
				=> await That(subject).Should().BeBefore(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be before <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
		{
			TimeOnly subject = TimeOnly.MaxValue;
			TimeOnly expected = TimeOnly.MaxValue;

			async Task Act()
				=> await That(subject).Should().BeBefore(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be before 23:59:59.9999999,
				             but it was 23:59:59.9999999
				             """);
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
		{
			TimeOnly subject = TimeOnly.MinValue;
			TimeOnly expected = TimeOnly.MinValue;

			async Task Act()
				=> await That(subject).Should().BeBefore(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be before 00:00:00.0000000,
				             but it was 00:00:00.0000000
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsLater_ShouldFail()
		{
			TimeOnly subject = LaterTime();
			TimeOnly expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().BeBefore(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be before {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsSame_ShouldFail()
		{
			TimeOnly subject = CurrentTime();
			TimeOnly expected = subject;

			async Task Act()
				=> await That(subject).Should().BeBefore(expected)
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be before {Formatter.Format(expected)}, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectsIsEarlier_ShouldSucceed()
		{
			TimeOnly subject = EarlierTime();
			TimeOnly expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().BeBefore(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Within_WhenNullableExpectedValueIsOutsideTheTolerance_ShouldFail()
		{
			TimeOnly subject = CurrentTime();
			TimeOnly? expected = EarlierTime(4);

			async Task Act()
				=> await That(subject).Should().BeBefore(expected)
					.Within(TimeSpan.FromSeconds(3));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be before {Formatter.Format(expected)} ± 0:03,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
		{
			TimeOnly subject = LaterTime(3);
			TimeOnly expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().BeBefore(expected)
					.Within(TimeSpan.FromSeconds(3))
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be before {Formatter.Format(expected)} ± 0:03, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
		{
			TimeOnly subject = LaterTime(2);
			TimeOnly expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().BeBefore(expected)
					.Within(TimeSpan.FromSeconds(3));

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeBeforeTests
	{
		[Fact]
		public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
		{
			TimeOnly subject = TimeOnly.MaxValue;
			TimeOnly unexpected = TimeOnly.MaxValue;

			async Task Act()
				=> await That(subject).Should().NotBeBefore(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
		{
			TimeOnly subject = TimeOnly.MinValue;
			TimeOnly unexpected = TimeOnly.MinValue;

			async Task Act()
				=> await That(subject).Should().NotBeBefore(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsEarlier_ShouldFail()
		{
			TimeOnly subject = EarlierTime();
			TimeOnly unexpected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().NotBeBefore(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be before {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsSame_ShouldSucceed()
		{
			TimeOnly subject = CurrentTime();
			TimeOnly unexpected = subject;

			async Task Act()
				=> await That(subject).Should().NotBeBefore(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectsIsLater_ShouldSucceed()
		{
			TimeOnly subject = LaterTime();
			TimeOnly unexpected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().NotBeBefore(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenUnexpectedIsNull_ShouldFail()
		{
			TimeOnly subject = CurrentTime();
			TimeOnly? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBeBefore(unexpected)
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be before <null>, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenNullableUnexpectedValueIsOutsideTheTolerance_ShouldFail()
		{
			TimeOnly subject = CurrentTime();
			TimeOnly? unexpected = LaterTime(4);

			async Task Act()
				=> await That(subject).Should().NotBeBefore(unexpected)
					.Within(TimeSpan.FromSeconds(3))
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be before {Formatter.Format(unexpected)} ± 0:03, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
		{
			TimeOnly subject = EarlierTime(4);
			TimeOnly unexpected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().NotBeBefore(unexpected)
					.Within(TimeSpan.FromSeconds(3));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be before {Formatter.Format(unexpected)} ± 0:03,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
		{
			TimeOnly subject = EarlierTime(3);
			TimeOnly unexpected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().NotBeBefore(unexpected)
					.Within(TimeSpan.FromSeconds(3));

			await That(Act).Should().NotThrow();
		}
	}
}
#endif
