﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatDateOnly
{
	public sealed partial class Nullable
	{
		public sealed class IsNotOnOrAfter
		{
			public sealed class Tests
			{
				[Fact]
				public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
				{
					DateOnly? subject = DateOnly.MaxValue;
					DateOnly unexpected = DateOnly.MaxValue;

					async Task Act()
						=> await That(subject).IsNotOnOrAfter(unexpected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not on or after {Formatter.Format(unexpected)},
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
				{
					DateOnly? subject = DateOnly.MinValue;
					DateOnly unexpected = DateOnly.MinValue;

					async Task Act()
						=> await That(subject).IsNotOnOrAfter(unexpected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not on or after {Formatter.Format(unexpected)},
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenSubjectIsLater_ShouldFail()
				{
					DateOnly? subject = LaterTime();
					DateOnly? unexpected = CurrentTime();

					async Task Act()
						=> await That(subject).IsNotOnOrAfter(unexpected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not on or after {Formatter.Format(unexpected)},
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					DateOnly? expected = CurrentTime();
					DateOnly? subject = null;

					async Task Act()
						=> await That(subject).IsNotOnOrAfter(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not on or after {Formatter.Format(expected)},
						              but it was <null>
						              """);
				}

				[Fact]
				public async Task WhenSubjectIsSame_ShouldFail()
				{
					DateOnly? subject = CurrentTime();
					DateOnly? unexpected = subject;

					async Task Act()
						=> await That(subject).IsNotOnOrAfter(unexpected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not on or after {Formatter.Format(unexpected)},
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenSubjectsIsEarlier_ShouldSucceed()
				{
					DateOnly? subject = EarlierTime();
					DateOnly? unexpected = CurrentTime();

					async Task Act()
						=> await That(subject).IsNotOnOrAfter(unexpected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenUnexpectedIsNull_ShouldFail()
				{
					DateOnly? subject = CurrentTime();
					DateOnly? unexpected = null;

					async Task Act()
						=> await That(subject).IsNotOnOrAfter(unexpected)
							.Because("we want to test the failure");

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not on or after <null>, because we want to test the failure,
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task Within_WhenNullableUnexpectedValueIsOutsideTheTolerance_ShouldFail()
				{
					DateOnly? subject = CurrentTime();
					DateOnly? unexpected = EarlierTime(3);

					async Task Act()
						=> await That(subject).IsNotOnOrAfter(unexpected)
							.Within(3.Days())
							.Because("we want to test the failure");

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not on or after {Formatter.Format(unexpected)} ± 3 days, because we want to test the failure,
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
				{
					DateOnly? subject = LaterTime(3);
					DateOnly? unexpected = CurrentTime();

					async Task Act()
						=> await That(subject).IsNotOnOrAfter(unexpected)
							.Within(3.Days());

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not on or after {Formatter.Format(unexpected)} ± 3 days,
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
				{
					DateOnly? subject = LaterTime(2);
					DateOnly? unexpected = CurrentTime();

					async Task Act()
						=> await That(subject).IsNotOnOrAfter(unexpected)
							.Within(3.Days());

					await That(Act).DoesNotThrow();
				}
			}
		}
	}
}
#endif
