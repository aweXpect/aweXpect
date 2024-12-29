namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class DateTimeOffsetShould
{
	public sealed class Be
	{
		public sealed class Tests
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
					=> await That(subject).Should().Be(expected).Within(-1.Seconds());

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
					=> await That(subject).Should().Be(expected).Within(3.Seconds())
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
					=> await That(subject).Should().Be(expected).Within(3.Seconds());

				await That(Act).Should().NotThrow();
			}
		}
	}
}
