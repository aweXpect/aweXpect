namespace Testably.Expectations.Tests.ThatTests.TimeSpans;

public sealed partial class TimeSpanShould
{
	public sealed class BeTests
	{
		[Fact]
		public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
		{
			TimeSpan subject = TimeSpan.MaxValue;
			TimeSpan expected = TimeSpan.MaxValue;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
		{
			TimeSpan subject = TimeSpan.MinValue;
			TimeSpan expected = TimeSpan.MinValue;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsDifferent_ShouldFail()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan? expected = LaterTime();

			async Task Act()
				=> await That(subject).Should().Be(expected).Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)}, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsTheExpectedValue_ShouldSucceed()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan? expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Within_NegativeTolerance_ShouldThrowArgumentOutOfRangeException()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan? expected = LaterTime(4);

			async Task Act()
				=> await That(subject).Should().Be(expected).Within(TimeSpan.FromSeconds(-1));

			await That(Act).Should().Throw<ArgumentOutOfRangeException>()
				.WithParamName("tolerance").And
				.WithMessage("*Tolerance must be non-negative*").AsWildcard();
		}

		[Fact]
		public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan? expected = LaterTime(4);

			async Task Act()
				=> await That(subject).Should().Be(expected).Within(TimeSpan.FromSeconds(3))
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)} ± 0:03, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan? expected = LaterTime(3);

			async Task Act()
				=> await That(subject).Should().Be(expected).Within(TimeSpan.FromSeconds(3));

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeTests
	{
		[Fact]
		public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
		{
			TimeSpan subject = TimeSpan.MaxValue;
			TimeSpan unexpected = TimeSpan.MaxValue;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected)
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be the maximum time span, because we want to test the failure,
				             but it was the maximum time span
				             """);
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
		{
			TimeSpan subject = TimeSpan.MinValue;
			TimeSpan unexpected = TimeSpan.MinValue;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected)
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be the minimum time span, because we want to test the failure,
				             but it was the minimum time span
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsDifferent_ShouldSucceed()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan? unexpected = LaterTime();

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsTheSame_ShouldFail()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan? unexpected = subject;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected)
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)}, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task Within_NegativeTolerance_ShouldThrowArgumentOutOfRangeException()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan? unexpected = LaterTime(4);

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected).Within(TimeSpan.FromSeconds(-1));

			await That(Act).Should().Throw<ArgumentOutOfRangeException>()
				.WithParamName("tolerance").And
				.WithMessage("*Tolerance must be non-negative*").AsWildcard();
		}

		[Fact]
		public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldSucceed()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan? unexpected = LaterTime(4);

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected)
					.Within(TimeSpan.FromSeconds(3));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Within_WhenValuesAreWithinTheTolerance_ShouldFail()
		{
			TimeSpan subject = CurrentTime();
			TimeSpan? unexpected = LaterTime(3);

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected).Within(TimeSpan.FromSeconds(3))
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)} ± 0:03, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}
	}
}
