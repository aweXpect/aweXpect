﻿namespace aweXpect.Tests;

public sealed partial class ThatTimeSpan
{
	public sealed partial class Nullable
	{
		public sealed class IsNotEqualTo
		{
			public sealed class Tests
			{
				[Fact]
				public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
				{
					TimeSpan? subject = TimeSpan.MaxValue;
					TimeSpan unexpected = TimeSpan.MaxValue;

					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected)
							.Because("we want to test the failure");

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is not equal to the maximum time span, because we want to test the failure,
						             but it was the maximum time span
						             """);
				}

				[Fact]
				public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
				{
					TimeSpan? subject = TimeSpan.MinValue;
					TimeSpan unexpected = TimeSpan.MinValue;

					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected)
							.Because("we want to test the failure");

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is not equal to the minimum time span, because we want to test the failure,
						             but it was the minimum time span
						             """);
				}

				[Fact]
				public async Task WhenSubjectAndUnexpectedAreNull_ShouldFail()
				{
					TimeSpan? subject = null;
					TimeSpan? unexpected = null;

					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is not equal to <null>,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WhenSubjectIsDifferent_ShouldSucceed()
				{
					TimeSpan? subject = CurrentTime();
					TimeSpan? unexpected = LaterTime();

					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldSucceed()
				{
					TimeSpan? subject = null;

					async Task Act()
						=> await That(subject).IsNotEqualTo(TimeSpan.Zero);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsTheSame_ShouldFail()
				{
					TimeSpan? subject = CurrentTime();
					TimeSpan? unexpected = subject;

					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected)
							.Because("we want to test the failure");

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not equal to {Formatter.Format(unexpected)}, because we want to test the failure,
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task Within_NegativeTolerance_ShouldThrowArgumentOutOfRangeException()
				{
					TimeSpan? subject = CurrentTime();
					TimeSpan? unexpected = LaterTime(4);

					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected).Within(-1.Seconds());

					await That(Act).Throws<ArgumentOutOfRangeException>()
						.WithMessage("*Tolerance must be non-negative*").AsWildcard().And
						.WithParamName("tolerance");
				}

				[Fact]
				public async Task Within_WhenValuesAreEarlierWithinTheTolerance_ShouldFail()
				{
					TimeSpan? subject = CurrentTime();
					TimeSpan? unexpected = EarlierTime(3);

					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected).Within(3.Seconds())
							.Because("we want to test the failure");

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not equal to {Formatter.Format(unexpected)} ± 0:03, because we want to test the failure,
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task Within_WhenValuesAreLaterWithinTheTolerance_ShouldFail()
				{
					TimeSpan? subject = CurrentTime();
					TimeSpan? unexpected = LaterTime(3);

					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected).Within(3.Seconds())
							.Because("we want to test the failure");

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not equal to {Formatter.Format(unexpected)} ± 0:03, because we want to test the failure,
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldSucceed()
				{
					TimeSpan? subject = CurrentTime();
					TimeSpan? unexpected = LaterTime(4);

					async Task Act()
						=> await That(subject).IsNotEqualTo(unexpected)
							.Within(3.Seconds());

					await That(Act).DoesNotThrow();
				}
			}
		}
	}
}
