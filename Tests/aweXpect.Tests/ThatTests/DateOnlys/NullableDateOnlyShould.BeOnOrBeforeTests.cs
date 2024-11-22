#if NET6_0_OR_GREATER
namespace Testably.Expectations.Tests.ThatTests.DateOnlys;

public sealed partial class NullableDateOnlyShould
{
	public sealed class BeOnOrBeforeTests
	{
		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			DateOnly? subject = CurrentTime();
			DateOnly? expected = null;

			async Task Act()
				=> await That(subject).Should().BeOnOrBefore(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be on or before <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
		{
			DateOnly? subject = DateOnly.MaxValue;
			DateOnly expected = DateOnly.MaxValue;

			async Task Act()
				=> await That(subject).Should().BeOnOrBefore(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
		{
			DateOnly? subject = DateOnly.MinValue;
			DateOnly expected = DateOnly.MinValue;

			async Task Act()
				=> await That(subject).Should().BeOnOrBefore(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsLater_ShouldFail()
		{
			DateOnly? subject = LaterTime();
			DateOnly? expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().BeOnOrBefore(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be on or before {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsSame_ShouldSucceed()
		{
			DateOnly? subject = CurrentTime();
			DateOnly? expected = subject;

			async Task Act()
				=> await That(subject).Should().BeOnOrBefore(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectsIsEarlier_ShouldSucceed()
		{
			DateOnly? subject = EarlierTime();
			DateOnly? expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().BeOnOrBefore(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Within_WhenNullableExpectedValueIsOutsideTheTolerance_ShouldFail()
		{
			DateOnly? subject = CurrentTime();
			DateOnly? expected = EarlierTime(4);

			async Task Act()
				=> await That(subject).Should().BeOnOrBefore(expected)
					.Within(TimeSpan.FromDays(3));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be on or before {Formatter.Format(expected)} ± 3 days,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
		{
			DateOnly? subject = LaterTime(4);
			DateOnly? expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().BeOnOrBefore(expected)
					.Within(TimeSpan.FromDays(3))
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be on or before {Formatter.Format(expected)} ± 3 days, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
		{
			DateOnly? subject = LaterTime(3);
			DateOnly? expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().BeOnOrBefore(expected)
					.Within(TimeSpan.FromDays(3));

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeOnOrBeforeTests
	{
		[Fact]
		public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
		{
			DateOnly? subject = DateOnly.MaxValue;
			DateOnly unexpected = DateOnly.MaxValue;

			async Task Act()
				=> await That(subject).Should().NotBeOnOrBefore(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be on or before {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
		{
			DateOnly? subject = DateOnly.MinValue;
			DateOnly unexpected = DateOnly.MinValue;

			async Task Act()
				=> await That(subject).Should().NotBeOnOrBefore(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be on or before {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsEarlier_ShouldFail()
		{
			DateOnly? subject = EarlierTime();
			DateOnly? unexpected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().NotBeOnOrBefore(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be on or before {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsSame_ShouldFail()
		{
			DateOnly? subject = CurrentTime();
			DateOnly? unexpected = subject;

			async Task Act()
				=> await That(subject).Should().NotBeOnOrBefore(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be on or before {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectsIsLater_ShouldSucceed()
		{
			DateOnly? subject = LaterTime();
			DateOnly? unexpected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().NotBeOnOrBefore(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenUnexpectedIsNull_ShouldFail()
		{
			DateOnly? subject = CurrentTime();
			DateOnly? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBeOnOrBefore(unexpected)
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be on or before <null>, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenNullableUnexpectedValueIsOutsideTheTolerance_ShouldFail()
		{
			DateOnly? subject = CurrentTime();
			DateOnly? unexpected = LaterTime(3);

			async Task Act()
				=> await That(subject).Should().NotBeOnOrBefore(unexpected)
					.Within(TimeSpan.FromDays(3))
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be on or before {Formatter.Format(unexpected)} ± 3 days, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
		{
			DateOnly? subject = EarlierTime(3);
			DateOnly? unexpected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().NotBeOnOrBefore(unexpected)
					.Within(TimeSpan.FromDays(3));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be on or before {Formatter.Format(unexpected)} ± 3 days,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
		{
			DateOnly? subject = EarlierTime(2);
			DateOnly? unexpected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().NotBeOnOrBefore(unexpected)
					.Within(TimeSpan.FromDays(3));

			await That(Act).Should().NotThrow();
		}
	}
}
#endif
