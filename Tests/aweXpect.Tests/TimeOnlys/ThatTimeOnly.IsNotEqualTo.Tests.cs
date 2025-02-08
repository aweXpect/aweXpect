#if NET8_0_OR_GREATER
namespace aweXpect.Tests;

public sealed partial class ThatTimeOnly
{
	public sealed class IsNotEqualTo
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsDifferent_ShouldSucceed()
			{
				TimeOnly subject = CurrentTime();
				TimeOnly unexpected = LaterTime();

				async Task Act()
					=> await That(subject).IsNotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsTheSame_ShouldFail()
			{
				TimeOnly subject = CurrentTime();
				TimeOnly unexpected = subject;

				async Task Act()
					=> await That(subject).IsNotEqualTo(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenUnexpectedIsNull_ShouldSucceed()
			{
				TimeOnly subject = CurrentTime();
				TimeOnly? unexpected = null;

				async Task Act()
					=> await That(subject).IsNotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(3, 2, false)]
			[InlineData(5, 3, false)]
			[InlineData(2, 2, true)]
			[InlineData(0, 2, true)]
			public async Task Within_WhenValuesAreInsideTheTolerance_ShouldFail(
				int actualDifference, int toleranceSeconds, bool expectToThrow)
			{
				TimeSpan tolerance = toleranceSeconds.Seconds();
				TimeOnly subject = EarlierTime(actualDifference);
				TimeOnly unexpected = CurrentTime();

				async Task Act()
					=> await That(subject).IsNotEqualTo(unexpected)
						.Within(tolerance)
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.OnlyIf(expectToThrow)
					.WithMessage($"""
					              Expected that subject
					              is not {Formatter.Format(unexpected)} ± {Formatter.Format(tolerance)}, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
#endif
