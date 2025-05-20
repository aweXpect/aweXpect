﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatTimeOnly
{
	public sealed class IsBetween
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenMaximumIsNull_ShouldFail()
			{
				TimeOnly subject = CurrentTime();
				TimeOnly? minimum = subject;
				TimeOnly? maximum = null;

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
				TimeOnly subject = CurrentTime();
				TimeOnly? minimum = null;
				TimeOnly? maximum = subject;

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
				TimeOnly subject = TimeOnly.MaxValue;
				TimeOnly minimum = CurrentTime();
				TimeOnly maximum = TimeOnly.MaxValue;

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndMinimumAreMinValue_ShouldSucceed()
			{
				TimeOnly subject = TimeOnly.MinValue;
				TimeOnly minimum = TimeOnly.MinValue;
				TimeOnly maximum = CurrentTime();

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsBetweenMinimumAndMaximum_ShouldSucceed()
			{
				TimeOnly subject = CurrentTime();
				TimeOnly minimum = EarlierTime();
				TimeOnly maximum = LaterTime();

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsEarlierThanMinimum_ShouldFail()
			{
				TimeOnly subject = EarlierTime();
				TimeOnly minimum = CurrentTime();
				TimeOnly maximum = LaterTime();

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
				TimeOnly subject = LaterTime();
				TimeOnly minimum = EarlierTime();
				TimeOnly maximum = CurrentTime();

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
				TimeOnly subject = CurrentTime();
				TimeOnly minimum = EarlierTime();
				TimeOnly maximum = subject;

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsSameAsMinimum_ShouldSucceed()
			{
				TimeOnly subject = CurrentTime();
				TimeOnly minimum = subject;
				TimeOnly maximum = LaterTime();

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
				TimeOnly subject = LaterTime(4);
				TimeOnly minimum = TimeOnly.MinValue;
				TimeOnly maximum = CurrentTime();

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
				TimeOnly subject = EarlierTime(4);
				TimeOnly minimum = CurrentTime();
				TimeOnly maximum = TimeOnly.MaxValue;

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
				TimeOnly subject = CurrentTime();
				TimeOnly minimum = TimeOnly.MinValue;
				TimeOnly? maximum = LaterTime(-4);

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
				TimeOnly subject = CurrentTime();
				TimeOnly? minimum = EarlierTime(-4);
				TimeOnly maximum = TimeOnly.MaxValue;

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
				TimeOnly subject = LaterTime(3);
				TimeOnly minimum = TimeOnly.MinValue;
				TimeOnly maximum = CurrentTime();

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum)
						.Within(3.Seconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenValueIsWithinTheMinimumTolerance_ShouldSucceed()
			{
				TimeOnly subject = EarlierTime(3);
				TimeOnly minimum = CurrentTime();
				TimeOnly maximum = TimeOnly.MaxValue;

				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum)
						.Within(3.Seconds());

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
