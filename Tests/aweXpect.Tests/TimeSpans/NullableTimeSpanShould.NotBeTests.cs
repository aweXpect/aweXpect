﻿namespace aweXpect.Tests.TimeSpans;

public sealed partial class NullableTimeSpanShould
{
	public sealed class NotBe
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
			{
				TimeSpan? subject = TimeSpan.MaxValue;
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
				TimeSpan? subject = TimeSpan.MinValue;
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
				TimeSpan? subject = CurrentTime();
				TimeSpan? unexpected = LaterTime();

				async Task Act()
					=> await That(subject).Should().NotBe(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsTheSame_ShouldFail()
			{
				TimeSpan? subject = CurrentTime();
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
				TimeSpan? subject = CurrentTime();
				TimeSpan? unexpected = LaterTime(4);

				async Task Act()
					=> await That(subject).Should().NotBe(unexpected).Within(-1.Seconds());

				await That(Act).Should().Throw<ArgumentOutOfRangeException>()
					.WithParamName("tolerance").And
					.WithMessage("*Tolerance must be non-negative*").AsWildcard();
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldSucceed()
			{
				TimeSpan? subject = CurrentTime();
				TimeSpan? unexpected = LaterTime(4);

				async Task Act()
					=> await That(subject).Should().NotBe(unexpected)
						.Within(3.Seconds());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldFail()
			{
				TimeSpan? subject = CurrentTime();
				TimeSpan? unexpected = LaterTime(3);

				async Task Act()
					=> await That(subject).Should().NotBe(unexpected).Within(3.Seconds())
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
}
