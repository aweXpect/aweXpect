#if NET8_0_OR_GREATER
using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatDateOnly
{
	public sealed partial class Nullable
	{
		public sealed class IsOneOf
		{
			public sealed class Tests
			{
				[Fact]
				public async Task WhenExpectedOnlyContainsNull_ShouldFail()
				{
					DateOnly? subject = CurrentTime();
					IEnumerable<DateOnly?> expected = [null,];

					async Task Act()
						=> await That(subject).IsOneOf(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is one of [<null>],
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenSubjectIsContained_ShouldSucceed()
				{
					DateOnly? subject = CurrentTime();
					IEnumerable<DateOnly?> expected = [LaterTime(), subject, EarlierTime(),];

					async Task Act()
						=> await That(subject).IsOneOf(expected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsDifferent_ShouldFail()
				{
					DateOnly? subject = CurrentTime();
					DateOnly[] expected = [LaterTime()!.Value, EarlierTime()!.Value,];

					async Task Act()
						=> await That(subject).IsOneOf(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is one of {Formatter.Format(expected)},
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					DateOnly? subject = null;
					IEnumerable<DateOnly?> expected = [CurrentTime(), LaterTime(),];

					async Task Act()
						=> await That(subject).IsOneOf(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is one of {Formatter.Format(expected)},
						              but it was <null>
						              """);
				}

				[Fact]
				public async Task WhenSubjectIsNullAndExpectedContainsNull_ShouldSucceed()
				{
					DateOnly? subject = null;
					IEnumerable<DateOnly?> expected = [CurrentTime(), null,];

					async Task Act()
						=> await That(subject).IsOneOf(expected);

					await That(Act).DoesNotThrow();
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
					DateOnly?[] expected = [CurrentTime(), LaterTime(),];

					async Task Act()
						=> await That(subject).IsOneOf(expected)
							.Within(tolerance.Days())
							.Because("we want to test the failure");

					await That(Act).Throws<XunitException>()
						.OnlyIf(expectToThrow)
						.WithMessage($"""
						              Expected that subject
						              is one of {Formatter.Format(expected)} ± {tolerance} days, because we want to test the failure,
						              but it was {Formatter.Format(subject)}
						              """);
				}
			}
		}
	}
}
#endif
