﻿namespace aweXpect.Tests.DateTimeOffsets;

public sealed partial class DateTimeOffsetShould
{
	public sealed class NotBeOnOrAfter
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectAndExpectedAreMaxValue_ShouldFail()
			{
				DateTimeOffset subject = DateTimeOffset.MaxValue;
				DateTimeOffset unexpected = DateTimeOffset.MaxValue;

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be on or after 9999-12-31T23:59:59.9999999+00:00,
					             but it was 9999-12-31T23:59:59.9999999+00:00
					             """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedAreMinValue_ShouldFail()
			{
				DateTimeOffset subject = DateTimeOffset.MinValue;
				DateTimeOffset unexpected = DateTimeOffset.MinValue;

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be on or after 0001-01-01T00:00:00.0000000+00:00,
					             but it was 0001-01-01T00:00:00.0000000+00:00
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsLater_ShouldFail()
			{
				DateTimeOffset subject = LaterTime();
				DateTimeOffset unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or after {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSame_ShouldFail()
			{
				DateTimeOffset subject = CurrentTime();
				DateTimeOffset unexpected = subject;

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or after {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectsIsEarlier_ShouldSucceed()
			{
				DateTimeOffset subject = EarlierTime();
				DateTimeOffset unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldFail()
			{
				DateTimeOffset subject = CurrentTime();
				DateTimeOffset? unexpected = null;

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected)
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or after <null>, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenNullableUnexpectedValueIsOutsideTheTolerance_ShouldFail()
			{
				DateTimeOffset subject = CurrentTime();
				DateTimeOffset? unexpected = EarlierTime(3);

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected)
						.Within(TimeSpan.FromSeconds(3))
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or after {Formatter.Format(unexpected)} ± 0:03, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail()
			{
				DateTimeOffset subject = LaterTime(3);
				DateTimeOffset unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected)
						.Within(TimeSpan.FromSeconds(3));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be on or after {Formatter.Format(unexpected)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task Within_WhenValuesAreWithinTheTolerance_ShouldSucceed()
			{
				DateTimeOffset subject = LaterTime(2);
				DateTimeOffset unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().NotBeOnOrAfter(unexpected)
						.Within(TimeSpan.FromSeconds(3));

				await That(Act).Should().NotThrow();
			}
		}
	}
}
