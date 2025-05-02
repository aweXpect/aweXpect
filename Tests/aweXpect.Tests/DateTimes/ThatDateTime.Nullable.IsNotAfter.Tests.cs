﻿namespace aweXpect.Tests;

public sealed partial class ThatDateTime
{
	public sealed partial class Nullable
	{
		public sealed class IsNotAfter
		{
			public sealed class Tests
			{
				[Fact]
				public async Task WhenSubjectAndExpectedAreMaxValue_ShouldSucceed()
				{
					DateTime? subject = DateTime.MaxValue;
					DateTime? unexpected = DateTime.MaxValue;

					async Task Act()
						=> await That(subject).IsNotAfter(unexpected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectAndExpectedAreMinValue_ShouldSucceed()
				{
					DateTime? subject = DateTime.MinValue;
					DateTime? unexpected = DateTime.MinValue;

					async Task Act()
						=> await That(subject).IsNotAfter(unexpected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsLater_ShouldFail()
				{
					DateTime? subject = LaterTime();
					DateTime? unexpected = CurrentTime();

					async Task Act()
						=> await That(subject).IsNotAfter(unexpected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not after {Formatter.Format(unexpected)},
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					DateTime? subject = null;
					DateTime? expected = CurrentTime();

					async Task Act()
						=> await That(subject).IsNotAfter(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not after {Formatter.Format(expected)},
						              but it was <null>
						              """);
				}

				[Fact]
				public async Task WhenSubjectIsSame_ShouldSucceed()
				{
					DateTime? subject = CurrentTime();
					DateTime? unexpected = subject;

					async Task Act()
						=> await That(subject).IsNotAfter(unexpected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectsIsEarlier_ShouldSucceed()
				{
					DateTime? subject = EarlierTime();
					DateTime? unexpected = CurrentTime();

					async Task Act()
						=> await That(subject).IsNotAfter(unexpected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenUnexpectedIsNull_ShouldFail()
				{
					DateTime? subject = CurrentTime();
					DateTime? unexpected = null;

					async Task Act()
						=> await That(subject).IsNotAfter(unexpected)
							.Because("we want to test the failure");

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not after <null>, because we want to test the failure,
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task Within_WhenUnexpectedValueIsOutsideTheTolerance_ShouldFail()
				{
					DateTime? subject = CurrentTime();
					DateTime unexpected = EarlierTime(4)!.Value;

					async Task Act()
						=> await That(subject).IsNotAfter(unexpected)
							.Within(3.Seconds())
							.Because("we want to test the failure");

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not after {Formatter.Format(unexpected)} ± 0:03, because we want to test the failure,
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
				{
					DateTime? subject = LaterTime(4);
					DateTime? unexpected = CurrentTime();

					async Task Act()
						=> await That(subject).IsNotAfter(unexpected)
							.Within(3.Seconds());

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not after {Formatter.Format(unexpected)} ± 0:03,
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
				{
					DateTime? subject = LaterTime(3);
					DateTime? unexpected = CurrentTime();

					async Task Act()
						=> await That(subject).IsNotAfter(unexpected)
							.Within(3.Seconds());

					await That(Act).DoesNotThrow();
				}
			}
		}
	}
}
