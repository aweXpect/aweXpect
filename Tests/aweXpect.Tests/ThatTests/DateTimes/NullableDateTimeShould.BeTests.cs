namespace Testably.Expectations.Tests.ThatTests.DateTimes;

public sealed partial class NullableDateTimeShould
{
	public sealed class BeTests
	{
		[Fact]
		public async Task WhenNullableSubjectIsTheExpectedValue_ShouldSucceed()
		{
			DateTime? subject = CurrentTime();
			DateTime expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenNullableSubjectIsTheNullableExpectedValue_ShouldSucceed()
		{
			DateTime? subject = CurrentTime();
			DateTime? expected = CurrentTime();

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
		{
			DateTime? subject = DateTime.MinValue;
			DateTime? expected = DateTime.MinValue;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsDifferent_ShouldFail()
		{
			DateTime? subject = CurrentTime();
			DateTime? expected = LaterTime();

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
		public async Task WhenSubjectOnlyDiffersInKind_ShouldFail()
		{
			DateTime? subject = new(2024, 11, 1, 14, 15, 0, DateTimeKind.Utc);
			DateTime? expected = new(2024, 11, 1, 14, 15, 0, DateTimeKind.Local);

			async Task Act()
				=> await That(subject).Should().Be(expected)
					.Because("we also test the kind property");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)}, because we also test the kind property,
				              but it differed in the Kind property
				              """);
		}

		[Fact]
		public async Task Within_NegativeTolerance_ShouldThrowArgumentOutOfRangeException()
		{
			DateTime? subject = CurrentTime();
			DateTime? expected = LaterTime(4);

			async Task Act()
				=> await That(subject).Should().Be(expected).Within(TimeSpan.FromSeconds(-1));

			await That(Act).Should().Throw<ArgumentOutOfRangeException>()
				.WithParamName("tolerance").And
				.WithMessage("*Tolerance must be non-negative*").AsWildcard();
		}

		[Fact]
		public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
		{
			DateTime? subject = CurrentTime();
			DateTime? expected = LaterTime(4);

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
			DateTime? subject = CurrentTime();
			DateTime? expected = LaterTime(3);

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
			DateTime? subject = DateTime.MaxValue;
			DateTime? unexpected = DateTime.MaxValue;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected)
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be 9999-12-31T23:59:59.9999999, because we want to test the failure,
				             but it was 9999-12-31T23:59:59.9999999
				             """);
		}

		[Fact]
		public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
		{
			DateTime? subject = DateTime.MinValue;
			DateTime? unexpected = DateTime.MinValue;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected)
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be 0001-01-01T00:00:00.0000000, because we want to test the failure,
				             but it was 0001-01-01T00:00:00.0000000
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsDifferent_ShouldSucceed()
		{
			DateTime? subject = CurrentTime();
			DateTime? unexpected = LaterTime();

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsTheSame_ShouldFail()
		{
			DateTime? subject = CurrentTime();
			DateTime? unexpected = subject;

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
		public async Task WhenSubjectOnlyDiffersInKind_ShouldSucceed()
		{
			DateTime? subject = new(2024, 11, 1, 14, 15, 0, DateTimeKind.Utc);
			DateTime? unexpected = new(2024, 11, 1, 14, 15, 0, DateTimeKind.Local);

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected)
					.Because("we also test the kind property");

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Within_NegativeTolerance_ShouldThrowArgumentOutOfRangeException()
		{
			DateTime? subject = CurrentTime();
			DateTime? expected = LaterTime(4);

			async Task Act()
				=> await That(subject).Should().NotBe(expected).Within(TimeSpan.FromSeconds(-1));

			await That(Act).Should().Throw<ArgumentOutOfRangeException>()
				.WithParamName("tolerance").And
				.WithMessage("*Tolerance must be non-negative*").AsWildcard();
		}

		[Fact]
		public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldSucceed()
		{
			DateTime? subject = CurrentTime();
			DateTime? expected = LaterTime(4);

			async Task Act()
				=> await That(subject).Should().NotBe(expected)
					.Within(TimeSpan.FromSeconds(3));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Within_WhenValuesAreWithinTheTolerance_ShouldFail()
		{
			DateTime? subject = CurrentTime();
			DateTime? expected = LaterTime(3);

			async Task Act()
				=> await That(subject).Should().NotBe(expected).Within(TimeSpan.FromSeconds(3))
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(expected)} ± 0:03, because we want to test the failure,
				              but it was {Formatter.Format(subject)}
				              """);
		}
	}
}
