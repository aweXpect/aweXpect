namespace Testably.Expectations.Tests.ThatTests.DateTimeOffsets;

public sealed partial class DateTimeOffsetShould
{
	public sealed class BeTests
	{
		[Fact]
		public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
		{
			DateTimeOffset subject = DateTimeOffset.MaxValue;
			DateTimeOffset expected = DateTimeOffset.MaxValue;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
		{
			DateTimeOffset subject = DateTimeOffset.MinValue;
			DateTimeOffset expected = DateTimeOffset.MinValue;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsDifferent_ShouldFail()
		{
			DateTimeOffset subject = CurrentTime();
			DateTimeOffset? expected = LaterTime();

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
			DateTimeOffset subject = CurrentTime();
			DateTimeOffset? expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Within_NegativeTolerance_ShouldThrowArgumentOutOfRangeException()
		{
			DateTimeOffset subject = CurrentTime();
			DateTimeOffset? expected = LaterTime(4);

			async Task Act()
				=> await That(subject).Should().Be(expected).Within(TimeSpan.FromSeconds(-1));

			await That(Act).Should().Throw<ArgumentOutOfRangeException>()
				.WithParamName("tolerance").And
				.WithMessage("*Tolerance must be non-negative*").AsWildcard();
		}

		[Fact]
		public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
		{
			DateTimeOffset subject = CurrentTime();
			DateTimeOffset? expected = LaterTime(4);

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
			DateTimeOffset subject = CurrentTime();
			DateTimeOffset? expected = LaterTime(3);

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
			DateTimeOffset subject = DateTimeOffset.MaxValue;
			DateTimeOffset unexpected = DateTimeOffset.MaxValue;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected)
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be 9999-12-31T23:59:59.9999999+00:00, because we want to test the failure,
				             but it was 9999-12-31T23:59:59.9999999+00:00
				             """);
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
		{
			DateTimeOffset subject = DateTimeOffset.MinValue;
			DateTimeOffset unexpected = DateTimeOffset.MinValue;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected)
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be 0001-01-01T00:00:00.0000000+00:00, because we want to test the failure,
				             but it was 0001-01-01T00:00:00.0000000+00:00
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsDifferent_ShouldSucceed()
		{
			DateTimeOffset subject = CurrentTime();
			DateTimeOffset? unexpected = LaterTime();

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsTheSame_ShouldFail()
		{
			DateTimeOffset subject = CurrentTime();
			DateTimeOffset? unexpected = subject;

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
			DateTimeOffset subject = CurrentTime();
			DateTimeOffset? unexpected = LaterTime(4);

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected).Within(TimeSpan.FromSeconds(-1));

			await That(Act).Should().Throw<ArgumentOutOfRangeException>()
				.WithParamName("tolerance").And
				.WithMessage("*Tolerance must be non-negative*").AsWildcard();
		}

		[Fact]
		public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldSucceed()
		{
			DateTimeOffset subject = CurrentTime();
			DateTimeOffset? unexpected = LaterTime(4);

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected)
					.Within(TimeSpan.FromSeconds(3));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Within_WhenValuesAreWithinTheTolerance_ShouldFail()
		{
			DateTimeOffset subject = CurrentTime();
			DateTimeOffset? unexpected = LaterTime(3);

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
