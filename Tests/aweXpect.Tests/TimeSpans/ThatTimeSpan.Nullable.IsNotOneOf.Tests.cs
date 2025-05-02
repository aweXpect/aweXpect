using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatTimeSpan
{
	public sealed partial class Nullable
	{
		public sealed class IsNotOneOf
		{
			public sealed class Tests
			{
				[Fact]
				public async Task WhenExpectedOnlyContainsNull_ShouldSucceed()
				{
					TimeSpan? subject = CurrentTime();
					IEnumerable<TimeSpan?> expected = [null,];

					async Task Act()
						=> await That(subject).IsNotOneOf(expected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsContained_ShouldFail()
				{
					TimeSpan? subject = CurrentTime();
					IEnumerable<TimeSpan?> expected = [LaterTime(), subject, EarlierTime(),];

					async Task Act()
						=> await That(subject).IsNotOneOf(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not one of {Formatter.Format(expected)},
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenSubjectIsDifferent_ShouldSucceed()
				{
					TimeSpan? subject = CurrentTime();
					TimeSpan[] expected = [LaterTime()!.Value, EarlierTime()!.Value,];

					async Task Act()
						=> await That(subject).IsNotOneOf(expected);

					await That(Act).DoesNotThrow();
				}

				[Theory]
				[InlineData(3, 2, false)]
				[InlineData(5, 3, false)]
				[InlineData(2, 2, true)]
				[InlineData(0, 2, true)]
				public async Task Within_WhenValuesAreOutsideTheTolerance_ShouldFail(
					int actualDifference, int tolerance, bool expectToThrow)
				{
					TimeSpan? subject = EarlierTime(actualDifference);
					TimeSpan?[] expected = [CurrentTime(), LaterTime(),];

					async Task Act()
						=> await That(subject).IsNotOneOf(expected)
							.Within(tolerance.Seconds())
							.Because("we want to test the failure");

					await That(Act).Throws<XunitException>()
						.OnlyIf(expectToThrow)
						.WithMessage($"""
						              Expected that subject
						              is not one of {Formatter.Format(expected)} ± 0:0{tolerance}, because we want to test the failure,
						              but it was {Formatter.Format(subject)}
						              """);
				}
			}
		}
	}
}
