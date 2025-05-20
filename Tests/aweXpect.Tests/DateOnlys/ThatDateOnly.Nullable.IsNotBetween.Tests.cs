﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatDateOnly
{
	public sealed partial class Nullable
	{
		public sealed class IsNotBetween
		{
			public sealed class Tests
			{
				[Fact]
				public async Task WhenMaximumIsNull_ShouldFail()
				{
					DateOnly? subject = CurrentTime();
					DateOnly? minimum = subject;
					DateOnly? maximum = null;

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
					DateOnly? subject = CurrentTime();
					DateOnly? minimum = null;
					DateOnly? maximum = subject;

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
					DateOnly? subject = DateOnly.MaxValue;
					DateOnly? minimum = CurrentTime();
					DateOnly maximum = DateOnly.MaxValue;

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
					DateOnly? subject = DateOnly.MinValue;
					DateOnly minimum = DateOnly.MinValue;
					DateOnly? maximum = CurrentTime();

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
					DateOnly? subject = EarlierTime();
					DateOnly? minimum = CurrentTime();
					DateOnly? maximum = LaterTime();

					async Task Act()
						=> await That(subject).IsNotBetween(minimum).And(maximum);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsLaterThanMaximum_ShouldSucceed()
				{
					DateOnly? subject = LaterTime();
					DateOnly? minimum = EarlierTime();
					DateOnly? maximum = CurrentTime();

					async Task Act()
						=> await That(subject).IsNotBetween(minimum).And(maximum);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNotBetweenMinimumAndMaximum_ShouldFail()
				{
					DateOnly? subject = CurrentTime();
					DateOnly? minimum = EarlierTime();
					DateOnly? maximum = LaterTime();

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
				public async Task WhenSubjectIsSameAsMaximum_ShouldFail()
				{
					DateOnly? subject = CurrentTime();
					DateOnly? minimum = EarlierTime();
					DateOnly? maximum = subject;

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
					DateOnly? subject = CurrentTime();
					DateOnly? minimum = subject;
					DateOnly? maximum = LaterTime();

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
					DateOnly? subject = CurrentTime();
					DateOnly minimum = DateOnly.MinValue;
					DateOnly? maximum = LaterTime(2);

					async Task Act()
						=> await That(subject).IsNotBetween(minimum).And(maximum)
							.Within(3.Days());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenMinimumValueIsOutsideTheTolerance_ShouldSucceed()
				{
					DateOnly? subject = CurrentTime();
					DateOnly? minimum = EarlierTime(2);
					DateOnly maximum = DateOnly.MaxValue;

					async Task Act()
						=> await That(subject).IsNotBetween(minimum).And(maximum)
							.Within(3.Days());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenNullableMaximumValueIsOutsideTheTolerance_ShouldSucceed()
				{
					DateOnly? subject = CurrentTime();
					DateOnly minimum = DateOnly.MinValue;
					DateOnly? maximum = LaterTime(2);

					async Task Act()
						=> await That(subject).IsNotBetween(minimum).And(maximum)
							.Within(3.Days());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenNullableMinimumValueIsOutsideTheTolerance_ShouldSucceed()
				{
					DateOnly? subject = CurrentTime();
					DateOnly? minimum = EarlierTime(2);
					DateOnly maximum = DateOnly.MaxValue;

					async Task Act()
						=> await That(subject).IsNotBetween(minimum).And(maximum)
							.Within(3.Days());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenValueIsWithinTheMaximumTolerance_ShouldFail()
				{
					DateOnly? subject = EarlierTime(3);
					DateOnly minimum = DateOnly.MinValue;
					DateOnly? maximum = CurrentTime();

					async Task Act()
						=> await That(subject).IsNotBetween(minimum).And(maximum)
							.Within(3.Days());

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not between {Formatter.Format(minimum)} and {Formatter.Format(maximum)} ± 3 days,
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenValueIsWithinTheMinimumTolerance_ShouldFail()
				{
					DateOnly? subject = LaterTime(3);
					DateOnly? minimum = CurrentTime();
					DateOnly maximum = DateOnly.MaxValue;

					async Task Act()
						=> await That(subject).IsNotBetween(minimum).And(maximum)
							.Within(3.Days());

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not between {Formatter.Format(minimum)} and {Formatter.Format(maximum)} ± 3 days,
						              but it was {Formatter.Format(subject)}
						              """);
				}
			}
		}
	}
}
#endif
