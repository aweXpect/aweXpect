namespace aweXpect.Tests;

public sealed partial class ThatTimeSpan
{
	public sealed class Is
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
			{
				TimeSpan subject = TimeSpan.MaxValue;
				TimeSpan expected = TimeSpan.MaxValue;

				async Task Act()
					=> await That(subject).Is(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
			{
				TimeSpan subject = TimeSpan.MinValue;
				TimeSpan expected = TimeSpan.MinValue;

				async Task Act()
					=> await That(subject).Is(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsDifferent_ShouldFail()
			{
				TimeSpan subject = CurrentTime();
				TimeSpan? expected = LaterTime();

				async Task Act()
					=> await That(subject).Is(expected).Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).Is(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task Within_NegativeTolerance_ShouldThrowArgumentOutOfRangeException()
			{
				TimeSpan subject = CurrentTime();
				TimeSpan? expected = LaterTime(4);

				async Task Act()
					=> await That(subject).Is(expected).Within(-1.Seconds());

				await That(Act).Does().Throw<ArgumentOutOfRangeException>()
					.WithParamName("tolerance").And
					.WithMessage("*Tolerance must be non-negative*").AsWildcard();
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				TimeSpan subject = CurrentTime();
				TimeSpan? expected = LaterTime(4);

				async Task Act()
					=> await That(subject).Is(expected).Within(3.Seconds())
						.Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).Is(expected).Within(3.Seconds());

				await That(Act).Does().NotThrow();
			}
		}
	}
}
