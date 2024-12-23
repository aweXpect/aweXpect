﻿#if NET8_0_OR_GREATER
namespace aweXpect.Tests.DateOnlys;

public sealed partial class NullableDateOnlyShould
{
	public sealed class Be
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenOnlyExpectedIsNull_ShouldFail()
			{
				DateOnly? subject = CurrentTime();
				DateOnly? expected = null;

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenOnlySubjectIsNull_ShouldFail()
			{
				DateOnly? subject = null;
				DateOnly? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be {Formatter.Format(expected)},
					              but it was <null>
					              """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedIsNull_ShouldSucceed()
			{
				DateOnly? subject = null;
				DateOnly? expected = null;

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsDifferent_ShouldFail()
			{
				DateOnly? subject = CurrentTime();
				DateOnly? expected = LaterTime();

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsTheSame_ShouldSucceed()
			{
				DateOnly? subject = CurrentTime();
				DateOnly? expected = subject;

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[InlineData(3, 2, true)]
			[InlineData(5, 3, true)]
			[InlineData(2, 2, false)]
			[InlineData(0, 2, false)]
			public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail(
				int actualDifference, int tolerance, bool expectToThrow)
			{
				DateOnly? subject = EarlierTime(actualDifference);
				DateOnly? expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().Be(expected)
						.Within(TimeSpan.FromDays(tolerance))
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.OnlyIf(expectToThrow)
					.WithMessage($"""
					              Expected subject to
					              be {Formatter.Format(expected)} ± {tolerance} days, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
#endif