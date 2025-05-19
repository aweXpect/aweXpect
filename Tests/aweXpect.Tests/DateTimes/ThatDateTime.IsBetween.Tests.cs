﻿namespace aweXpect.Tests;

public sealed partial class ThatDateTime
{
	public sealed class IsBetween
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenMaximumIsNull_ShouldFail()
			{
				DateTime subject = CurrentTime();
				DateTime? minimum = subject;
				DateTime? maximum = null;

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is between {Formatter.Format(minimum)} and <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMinimumIsNull_ShouldFail()
			{
				DateTime subject = CurrentTime();
				DateTime? minimum = null;
				DateTime? maximum = subject;

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is between <null> and {Formatter.Format(maximum)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndMaximumAreMaxValue_ShouldSucceed()
			{
				DateTime subject = DateTime.MaxValue;
				DateTime minimum = CurrentTime();
				DateTime maximum = DateTime.MaxValue;

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndMinimumAreMinValue_ShouldSucceed()
			{
				DateTime subject = DateTime.MinValue;
				DateTime minimum = DateTime.MinValue;
				DateTime maximum = CurrentTime();

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsBetweenMinimumAndMaximum_ShouldSucceed()
			{
				DateTime subject = CurrentTime();
				DateTime minimum = EarlierTime();
				DateTime maximum = LaterTime();

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsEarlierThanMinimum_ShouldFail()
			{
				DateTime subject = EarlierTime();
				DateTime minimum = CurrentTime();
				DateTime maximum = LaterTime();

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is between {Formatter.Format(minimum)} and {Formatter.Format(maximum)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsLaterThanMaximum_ShouldFail()
			{
				DateTime subject = LaterTime();
				DateTime minimum = EarlierTime();
				DateTime maximum = CurrentTime();

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is between {Formatter.Format(minimum)} and {Formatter.Format(maximum)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsSameAsMaximum_ShouldSucceed()
			{
				DateTime subject = CurrentTime();
				DateTime minimum = EarlierTime();
				DateTime maximum = subject;

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsSameAsMinimum_ShouldSucceed()
			{
				DateTime subject = CurrentTime();
				DateTime minimum = subject;
				DateTime maximum = LaterTime();

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class WithinTests
		{
			[Fact]
			public async Task WhenMaximumValueIsOutsideTheTolerance_ShouldFail()
			{
				DateTime subject = LaterTime(4);
				DateTime minimum = DateTime.MinValue;
				DateTime maximum = CurrentTime();

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum)
						.Within(3.Seconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is between {Formatter.Format(minimum)} and {Formatter.Format(maximum)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenMinimumValueIsOutsideTheTolerance_ShouldFail()
			{
				DateTime subject = EarlierTime(4);
				DateTime minimum = CurrentTime();
				DateTime maximum = DateTime.MaxValue;

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum)
						.Within(3.Seconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is between {Formatter.Format(minimum)} and {Formatter.Format(maximum)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenNullableMaximumValueIsOutsideTheTolerance_ShouldFail()
			{
				DateTime subject = CurrentTime();
				DateTime minimum = DateTime.MinValue;
				DateTime? maximum = LaterTime(-4);

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum)
						.Within(3.Seconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is between {Formatter.Format(minimum)} and {Formatter.Format(maximum)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenNullableMinimumValueIsOutsideTheTolerance_ShouldFail()
			{
				DateTime subject = CurrentTime();
				DateTime? minimum = EarlierTime(-4);
				DateTime maximum = DateTime.MaxValue;

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum)
						.Within(3.Seconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is between {Formatter.Format(minimum)} and {Formatter.Format(maximum)} ± 0:03,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenValueIsWithinTheMaximumTolerance_ShouldSucceed()
			{
				DateTime subject = LaterTime(3);
				DateTime minimum = DateTime.MinValue;
				DateTime maximum = CurrentTime();

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum)
						.Within(3.Seconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenValueIsWithinTheMinimumTolerance_ShouldSucceed()
			{
				DateTime subject = EarlierTime(3);
				DateTime minimum = CurrentTime();
				DateTime maximum = DateTime.MaxValue;

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum)
						.Within(3.Seconds());

				await That(Act).DoesNotThrow();
			}
		}
	}
}
