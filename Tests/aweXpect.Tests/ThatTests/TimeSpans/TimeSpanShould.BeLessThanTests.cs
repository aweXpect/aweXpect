namespace Testably.Expectations.Tests.ThatTests.TimeSpans;

public sealed partial class TimeSpanShould
{
	public sealed class BeLessThanTests
	{
		[Fact]
		public async Task WhenExpectedIsNull_ShouldFail()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan? expected = null;

			async Task Act()
				=> await That(subject).Should().BeLessThan(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
		{
			TimeSpan subject = TimeSpan.MaxValue;
			TimeSpan expected = TimeSpan.MaxValue;

			async Task Act()
				=> await That(subject).Should().BeLessThan(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be less than the maximum time span,
				             but it was the maximum time span
				             """);
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
		{
			TimeSpan subject = TimeSpan.MinValue;
			TimeSpan expected = TimeSpan.MinValue;

			async Task Act()
				=> await That(subject).Should().BeLessThan(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be less than the minimum time span,
				             but it was the minimum time span
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsLater_ShouldFail()
		{
			TimeSpan subject = LaterTime();
			TimeSpan expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().BeLessThan(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsSame_ShouldFail()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan expected = subject;

			async Task Act()
				=> await That(subject).Should().BeLessThan(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectsIsEarlier_ShouldSucceed()
		{
			TimeSpan subject = EarlierTime();
			TimeSpan expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().BeLessThan(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Within_WhenNullableExpectedValueIsOutsideTheTolerance_ShouldFail()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan? expected = LaterTime(-3);

			async Task Act()
				=> await That(subject).Should().BeLessThan(expected)
					.Within(TimeSpan.FromSeconds(3));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than {Formatter.Format(expected)} ± 0:03,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
		{
			TimeSpan subject = LaterTime(3);
			TimeSpan expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().BeLessThan(expected)
					.Within(TimeSpan.FromSeconds(3));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than {Formatter.Format(expected)} ± 0:03,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
		{
			TimeSpan subject = LaterTime(2);
			TimeSpan expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().BeLessThan(expected)
					.Within(TimeSpan.FromSeconds(3));

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeLessThanTests
	{
		[Fact]
		public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
		{
			TimeSpan subject = TimeSpan.MaxValue;
			TimeSpan unexpected = TimeSpan.MaxValue;

			async Task Act()
				=> await That(subject).Should().NotBeLessThan(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
		{
			TimeSpan subject = TimeSpan.MinValue;
			TimeSpan unexpected = TimeSpan.MinValue;

			async Task Act()
				=> await That(subject).Should().NotBeLessThan(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsEarlier_ShouldFail()
		{
			TimeSpan subject = EarlierTime();
			TimeSpan unexpected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().NotBeLessThan(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be less than {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsSame_ShouldSucceed()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan unexpected = subject;

			async Task Act()
				=> await That(subject).Should().NotBeLessThan(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectsIsLater_ShouldSucceed()
		{
			TimeSpan subject = LaterTime();
			TimeSpan unexpected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().NotBeLessThan(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenUnexpectedIsNull_ShouldFail()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBeLessThan(unexpected)
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be less than <null>, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenNullableUnexpectedValueIsOutsideTheTolerance_ShouldFail()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan? unexpected = LaterTime(4);

			async Task Act()
				=> await That(subject).Should().NotBeLessThan(unexpected)
					.Within(TimeSpan.FromSeconds(3))
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be less than {Formatter.Format(unexpected)} ± 0:03, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
		{
			TimeSpan subject = EarlierTime(4);
			TimeSpan unexpected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().NotBeLessThan(unexpected)
					.Within(TimeSpan.FromSeconds(3));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be less than {Formatter.Format(unexpected)} ± 0:03,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
		{
			TimeSpan subject = EarlierTime(3);
			TimeSpan unexpected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().NotBeLessThan(unexpected)
					.Within(TimeSpan.FromSeconds(3));

			await That(Act).Should().NotThrow();
		}
	}
}
