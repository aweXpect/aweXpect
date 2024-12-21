#if NET8_0_OR_GREATER
namespace aweXpect.Tests.TimeOnlys;

public sealed partial class TimeOnlyShould
{
	public sealed class Be
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				TimeOnly subject = CurrentTime();
				TimeOnly? expected = null;

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
			public async Task WhenSubjectIsDifferent_ShouldFail()
			{
				TimeOnly subject = CurrentTime();
				TimeOnly expected = LaterTime();

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
				TimeOnly subject = CurrentTime();
				TimeOnly expected = subject;

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
				int actualDifference, int toleranceSeconds, bool expectToThrow)
			{
				TimeSpan tolerance = TimeSpan.FromSeconds(toleranceSeconds);
				TimeOnly subject = EarlierTime(actualDifference);
				TimeOnly expected = CurrentTime();

				async Task Act()
					=> await That(subject).Should().Be(expected)
						.Within(tolerance)
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.OnlyIf(expectToThrow)
					.WithMessage($"""
					              Expected subject to
					              be {Formatter.Format(expected)} ± {Formatter.Format(tolerance)}, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
#endif
