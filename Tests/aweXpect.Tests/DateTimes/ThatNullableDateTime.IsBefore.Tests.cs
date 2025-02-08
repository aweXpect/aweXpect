﻿namespace aweXpect.Tests;

public sealed partial class ThatNullableDateTime
{
	public sealed class IsBefore
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				DateTime? subject = CurrentTime();
				DateTime? expected = null;

				async Task Act()
					=> await That(subject).IsBefore(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              be before <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
			{
				DateTime? subject = DateTime.MaxValue;
				DateTime? expected = DateTime.MaxValue;

				async Task Act()
					=> await That(subject).IsBefore(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             be before 9999-12-31T23:59:59.9999999,
					             but it was 9999-12-31T23:59:59.9999999
					             """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
			{
				DateTime? subject = DateTime.MinValue;
				DateTime? expected = DateTime.MinValue;

				async Task Act()
					=> await That(subject).IsBefore(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             be before 0001-01-01T00:00:00.0000000,
					             but it was 0001-01-01T00:00:00.0000000
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsLater_ShouldFail()
			{
				DateTime? subject = LaterTime();
				DateTime? expected = CurrentTime();

				async Task Act()
					=> await That(subject).IsBefore(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              be before {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldFail()
			{
				DateTime? subject = CurrentTime();
				DateTime? expected = subject;

				async Task Act()
					=> await That(subject).IsBefore(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              be before {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectsIsEarlier_ShouldSucceed()
			{
				DateTime? subject = EarlierTime();
				DateTime? expected = CurrentTime();

				async Task Act()
					=> await That(subject).IsBefore(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task Within_WhenExpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				DateTime? subject = CurrentTime();
				DateTime expected = LaterTime(-3);

				async Task Act()
					=> await That(subject).IsBefore(expected)
						.Within(3.Seconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              be before {Formatter.Format(expected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				DateTime? subject = LaterTime(3);
				DateTime? expected = CurrentTime();

				async Task Act()
					=> await That(subject).IsBefore(expected)
						.Within(3.Seconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              be before {Formatter.Format(expected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				DateTime? subject = LaterTime(2);
				DateTime? expected = CurrentTime();

				async Task Act()
					=> await That(subject).IsBefore(expected)
						.Within(3.Seconds());

				await That(Act).DoesNotThrow();
			}
		}
	}
}
