﻿namespace aweXpect.Tests;

public sealed partial class ThatDateTimeOffset
{
	public sealed class IsNotBetween
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenMaximumIsNull_ShouldFail()
			{
				DateTimeOffset subject = CurrentTime();
				DateTimeOffset? minimum = subject;
				DateTimeOffset? maximum = null;

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not between {Formatter.Format(minimum)} and <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMinimumIsNull_ShouldFail()
			{
				DateTimeOffset subject = CurrentTime();
				DateTimeOffset? minimum = null;
				DateTimeOffset? maximum = subject;

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not between <null> and {Formatter.Format(maximum)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndMaximumAreMaxValue_ShouldFail()
			{
				DateTimeOffset subject = DateTimeOffset.MaxValue;
				DateTimeOffset minimum = CurrentTime();
				DateTimeOffset maximum = DateTimeOffset.MaxValue;

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not between {Formatter.Format(minimum)} and {Formatter.Format(maximum)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndMinimumAreMinValue_ShouldFail()
			{
				DateTimeOffset subject = DateTimeOffset.MinValue;
				DateTimeOffset minimum = DateTimeOffset.MinValue;
				DateTimeOffset maximum = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not between {Formatter.Format(minimum)} and {Formatter.Format(maximum)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNotBetweenMinimumAndMaximum_ShouldFail()
			{
				DateTimeOffset subject = CurrentTime();
				DateTimeOffset minimum = EarlierTime();
				DateTimeOffset maximum = LaterTime();

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not between {Formatter.Format(minimum)} and {Formatter.Format(maximum)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsEarlierThanMinimum_ShouldSucceed()
			{
				DateTimeOffset subject = EarlierTime();
				DateTimeOffset minimum = CurrentTime();
				DateTimeOffset maximum = LaterTime();

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsLaterThanMaximum_ShouldSucceed()
			{
				DateTimeOffset subject = LaterTime();
				DateTimeOffset minimum = EarlierTime();
				DateTimeOffset maximum = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsSameAsMaximum_ShouldFail()
			{
				DateTimeOffset subject = CurrentTime();
				DateTimeOffset minimum = EarlierTime();
				DateTimeOffset maximum = subject;

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not between {Formatter.Format(minimum)} and {Formatter.Format(maximum)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSameAsMinimum_ShouldFail()
			{
				DateTimeOffset subject = CurrentTime();
				DateTimeOffset minimum = subject;
				DateTimeOffset maximum = LaterTime();

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not between {Formatter.Format(minimum)} and {Formatter.Format(maximum)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}

		public sealed class WithinTests
		{
			[Fact]
			public async Task WhenMaximumValueIsOutsideTheTolerance_ShouldSucceed()
			{
				DateTimeOffset subject = CurrentTime();
				DateTimeOffset minimum = DateTimeOffset.MinValue;
				DateTimeOffset maximum = LaterTime(2);

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum)
						.Within(3.Seconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMinimumValueIsOutsideTheTolerance_ShouldSucceed()
			{
				DateTimeOffset subject = CurrentTime();
				DateTimeOffset minimum = EarlierTime(2);
				DateTimeOffset maximum = DateTimeOffset.MaxValue;

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum)
						.Within(3.Seconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenNullableMaximumValueIsOutsideTheTolerance_ShouldSucceed()
			{
				DateTimeOffset subject = CurrentTime();
				DateTimeOffset minimum = DateTimeOffset.MinValue;
				DateTimeOffset? maximum = LaterTime(2);

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum)
						.Within(3.Seconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenNullableMinimumValueIsOutsideTheTolerance_ShouldSucceed()
			{
				DateTimeOffset subject = CurrentTime();
				DateTimeOffset? minimum = EarlierTime(2);
				DateTimeOffset maximum = DateTimeOffset.MaxValue;

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum)
						.Within(3.Seconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenValueIsWithinTheMaximumTolerance_ShouldFail()
			{
				DateTimeOffset subject = EarlierTime(3);
				DateTimeOffset minimum = DateTimeOffset.MinValue;
				DateTimeOffset maximum = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum)
						.Within(3.Seconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not between {Formatter.Format(minimum)} and {Formatter.Format(maximum)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenValueIsWithinTheMinimumTolerance_ShouldFail()
			{
				DateTimeOffset subject = LaterTime(3);
				DateTimeOffset minimum = CurrentTime();
				DateTimeOffset maximum = DateTimeOffset.MaxValue;

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum)
						.Within(3.Seconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not between {Formatter.Format(minimum)} and {Formatter.Format(maximum)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
